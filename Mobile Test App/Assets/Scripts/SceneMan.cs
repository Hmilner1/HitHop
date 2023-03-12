using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public void OnClickMainMenuPlay()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
        SceneManager.LoadScene("MainGameScreen");
    }

    public void OnClickSettings()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
        SceneManager.LoadScene("Settings Menu", LoadSceneMode.Additive);
        AudioManager.instance.musicBus.setPaused(true);
        Time.timeScale = 0f;
    }

    public void OnClickUnloadSettings()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
        SceneManager.UnloadSceneAsync("Settings Menu");
        AudioManager.instance.musicBus.setPaused(false);
        Time.timeScale = 1f;
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
