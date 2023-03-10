using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public void OnClickMainMenuPlay()
    {
        SceneManager.LoadScene("MainGameScreen");
    }

    public void OnClickSettings()
    {
        SceneManager.LoadScene("Settings Menu", LoadSceneMode.Additive);
    }

    public void OnClickUnloadSettings()
    {
        SceneManager.UnloadSceneAsync("Settings Menu");
    }
}
