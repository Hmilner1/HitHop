using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
