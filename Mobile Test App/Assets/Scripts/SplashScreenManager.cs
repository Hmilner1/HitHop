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
    //[SerializeField]
   // private GameObject m_SaveManager;
    public void OnClickStart()
    {
        SceneManager.LoadScene("StartMenu");
        GameObject newAudioManager =  Instantiate(m_AudioManager);
        GameObject newSongManager =  Instantiate(m_SongManager);
        //GameObject newSaveManager = Instantiate(m_SaveManager);
        newAudioManager.name = newAudioManager.name.Replace("(Clone)","").Trim();
        newSongManager.name = newSongManager.name.Replace("(Clone)", "").Trim();
        //newSaveManager.name = newSaveManager.name.Replace("(Clone)", "").Trim();
        AudioSettings settings = SaveManager.LoadAudioSettings();
        AudioManager.instance.m_MasterVolume = settings.Master;
        AudioManager.instance.m_Music = settings.Music;
        AudioManager.instance.m_SFX = settings.Sfx;
    }
}
