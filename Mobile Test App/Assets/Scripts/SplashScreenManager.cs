using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_AudioManager;
    [SerializeField]
    private GameObject m_SongManager;
    public void OnClickStart()
    {
        SceneManager.LoadScene("StartMenu");
        GameObject newAudioManager =  Instantiate(m_AudioManager);
        GameObject newSongManager =  Instantiate(m_SongManager);
        newAudioManager.name = newAudioManager.name.Replace("(Clone)","").Trim();
        newSongManager.name = newSongManager.name.Replace("(Clone)", "").Trim();
    }
}
