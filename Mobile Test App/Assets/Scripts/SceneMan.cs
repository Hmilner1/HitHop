using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public void OnClickMainMenuPlay()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.LoadScene("MainGameScreen");
    }

    public void OnClickSettings()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.LoadScene("Settings Menu", LoadSceneMode.Additive);
        AudioManager.instance.musicBus.setPaused(true);
        Time.timeScale = 0f;
    }

    public void OnClickUnloadSettings()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.UnloadSceneAsync("Settings Menu");
        AudioManager.instance.musicBus.setPaused(false);
        Time.timeScale = 1f;
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1f;
        AudioManager.instance.musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        AudioManager.instance.musicBus.setPaused(false);
        SceneManager.LoadScene("StartMenu");
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        AudioManager.instance.musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        AudioManager.instance.musicBus.setPaused(false);
        //SceneManager.LoadScene("StartMenu");
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void OnClickShop()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.LoadScene("ShopMenu", LoadSceneMode.Additive);
    }
    public void OnClickProgression()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.LoadScene("ProgressionPath", LoadSceneMode.Additive);
    }

    public void UnloadShop()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.UnloadSceneAsync("ShopMenu");
    }

    public void UnloadProgression()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.UnloadSceneAsync("ProgressionPath");
    }

    public void OnClickSkinMenu()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.LoadScene("SkinMenu", LoadSceneMode.Additive);
    }

    public void UnloadSkinMenu()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.MenuSound, this.transform.position);
        SceneManager.UnloadSceneAsync("SkinMenu");
    }
}
