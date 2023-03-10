using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Song : MonoBehaviour
{
    [SerializeField]
    private SongSO[] m_SongList;
    [SerializeField]
    private TextMeshPro m_SongName;
    [SerializeField]
    private float m_WaitTime;

    private SongSO m_CurrentSong;
    //private TMP_Text m_SongName;
    private SelectedSong m_SelectedSong;
    int m_SelectedIndex;
    private Animator m_CassetAnimator;

    private void Awake()
    {
        m_SelectedIndex= 0;
        m_CurrentSong = m_SongList[m_SelectedIndex];
        m_SongName.text = m_CurrentSong.SongName;
        m_SelectedSong = GameObject.Find("SongManger").GetComponent<SelectedSong>();
        m_CassetAnimator = GameObject.Find("Casset").GetComponent<Animator>();
    }

    private void Update()
    {
        m_SongName.text = m_CurrentSong.SongName;
        m_CurrentSong = m_SongList[m_SelectedIndex];
        m_SelectedSong.m_SongSO = m_CurrentSong;
    }

    public void OnClickPlay()
    {
        m_CassetAnimator.SetTrigger("Play");
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        while (true)
        {

            yield return new WaitForSeconds(m_WaitTime);
            SceneManager.LoadScene("MainGameScreen");
            StopAllCoroutines();
        }
    }

    public void OnClickForward()
    {
        if (m_SelectedIndex < m_SongList.Length -1)
        {
            m_CassetAnimator.SetTrigger("Out");
            StartCoroutine(Forward());
        }
    }

    IEnumerator Forward()
    { 
        while (true)
        {
            yield return new WaitForSeconds(m_WaitTime);
            m_SelectedIndex++;
            m_CassetAnimator.SetTrigger("In");
            StopAllCoroutines();
        }
    }

    public void OnClickBack()
    {
        if (m_SelectedIndex > 0)
        {
            //m_SelectedIndex--;
            m_CassetAnimator.SetTrigger("Out");
            StartCoroutine(Back());
        }
    }

    IEnumerator Back()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_WaitTime);
            m_SelectedIndex--;
            m_CassetAnimator.SetTrigger("In");
            StopAllCoroutines();
        }
    }
}
