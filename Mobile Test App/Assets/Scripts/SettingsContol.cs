using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsContol : MonoBehaviour
{
    private Button m_HomeButton;

    private void Awake()
    {
        m_HomeButton = GameObject.Find("Home Button").GetComponent<Button>();
        m_HomeButton.gameObject.SetActive(false);

        Scene[] scenes = SceneManager.GetAllScenes();
        foreach (Scene scene in scenes)
        {
            if (scene.name == "MainGameScreen")
            {
                m_HomeButton.gameObject.SetActive(true);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
