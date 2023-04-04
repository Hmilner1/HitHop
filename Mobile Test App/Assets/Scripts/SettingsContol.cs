using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;

public class SettingsContol : MonoBehaviour
{
    private Button m_HomeButton;
    [SerializeField]
    private Toggle m_BatteryToggle;
    [SerializeField]
    private Toggle m_ReducemotionToggle;

    public delegate void BatteryToggleOn(bool isOn);
    public static event BatteryToggleOn OnBatteryToggle;

    public delegate void MotionToggleOn(bool isOn);
    public static event MotionToggleOn OnMotionToggle;


    private void Awake()
    {
        AudioSettings settings = SaveManager.LoadAudioSettings();
        if (settings != null)
        {
            m_BatteryToggle.isOn = settings.BToggleState;
            m_ReducemotionToggle.isOn = settings.MToggleState; 
        }

        m_HomeButton = GameObject.Find("Home Button").GetComponent<Button>();
        m_HomeButton.gameObject.SetActive(false);

        Scene[] scenes = SceneManager.GetAllScenes();
        foreach (Scene scene in scenes)
        {
            if (scene.name == "MainGameScreen")
            {
                m_HomeButton.gameObject.SetActive(true);
            }
            else if (scene.name == "TutorialLevel")
            {
                m_HomeButton.gameObject.SetActive(true);
            }
        }
    }

    public void OnClickBatterySaver(bool OnValue)
    {
        if (OnValue == true)
        {
            OnBatteryToggle?.Invoke(OnValue);
            Application.targetFrameRate = 30;
        }
        else if (OnValue == false)
        {
            OnBatteryToggle?.Invoke(OnValue);
            Application.targetFrameRate = 120;
        }
    }

    public void OnClickReduceMotion(bool OnValue)
    {
        OnMotionToggle?.Invoke(OnValue);
    }
}
