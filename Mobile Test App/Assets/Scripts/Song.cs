using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Song : MonoBehaviour
{
    [SerializeField]
    private SongSO[] m_SongList;
    [SerializeField]
    private Text m_SongName;

    private SongSO m_CurrentSong;
    //private TMP_Text m_SongName;
    private SelectedSong m_SelectedSong;
    int m_SelectedIndex;
    private void Awake()
    {
        m_SelectedIndex= 0;
        m_CurrentSong = m_SongList[m_SelectedIndex];
        m_SongName.text = m_CurrentSong.SongName;
        m_SelectedSong = GameObject.Find("SongManger").GetComponent<SelectedSong>();
    }

    private void Update()
    {
        m_SongName.text = m_CurrentSong.SongName;
        m_CurrentSong = m_SongList[m_SelectedIndex];
        m_SelectedSong.m_SongSO = m_CurrentSong;
    }

    public void OnClickForward()
    {
        if (m_SelectedIndex < m_SongList.Length -1)
        {
            m_SelectedIndex++;
        }
    }

    public void OnClickBack()
    {
        if (m_SelectedIndex > 0)
        {
            m_SelectedIndex--;
        }
    }
}
