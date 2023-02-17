using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Song : MonoBehaviour
{
    public SongSO m_CurrentSong;
    //private TMP_Text m_SongName;
    private Text m_SongName;
    private SelectedSong m_SelectedSong;
    private void Awake()
    {
        m_SongName = gameObject.GetComponentInChildren<Text>();
        m_SongName.text = m_CurrentSong.SongName;
        m_SelectedSong = GameObject.Find("SongManger").GetComponent<SelectedSong>();
    }

    public void OnButtonClicked()
    {
        m_SelectedSong.m_SongSO = m_CurrentSong;
    }
}
