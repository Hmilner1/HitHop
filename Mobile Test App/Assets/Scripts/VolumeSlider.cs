using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType { 
        MASTER,
        MUSIC,
        SFX
    }
    [SerializeField]
    private VolumeType volumeType;
    [SerializeField]
    private Slider m_VolumeSlider;

    public float Master = 1;
    public float Music = 1;
    public float Sfx = 1;

    public bool m_BatteryToggleState;
    public bool m_MotionToggleState;

    private void OnEnable()
    {
        SettingsContol.OnBatteryToggle += OnBatteryToggle;
        SettingsContol.OnMotionToggle += OnMotionToggle;
    }

    private void OnDisable()
    {
        SettingsContol.OnBatteryToggle -= OnBatteryToggle;
        SettingsContol.OnMotionToggle -= OnMotionToggle;
    }

    private void Awake()
    {
        AudioSettings settings =  SaveManager.LoadAudioSettings();
        if (settings != null)
        {
            Master = settings.Master;
            Music = settings.Music;
            Sfx = settings.Sfx;
            m_BatteryToggleState = settings.BToggleState;
            m_MotionToggleState = settings.MToggleState;
        }
        else
        {
            SaveManager.SaveAudioSettings(this);
        }

    }

    private void Update()
    {
        AudioSettings settings = SaveManager.LoadAudioSettings();
        Master = settings.Master;
        Music = settings.Music;
        Sfx = settings.Sfx;

        switch (volumeType)
        {
            case VolumeType.MASTER:
                m_VolumeSlider.value = Master;
                break;

            case VolumeType.MUSIC:
                m_VolumeSlider.value = Music;
                break;

            case VolumeType.SFX:
                m_VolumeSlider.value = Sfx;
                break;
                default:
                Debug.LogWarning("Volume error unknown source");
                break;
        }
    }

    public void OnSliderChange()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                Master = m_VolumeSlider.value;
                AudioManager.instance.m_MasterVolume = Master;
                break;

            case VolumeType.MUSIC:
                Music = m_VolumeSlider.value;
                AudioManager.instance.m_Music = Music;
                break;

            case VolumeType.SFX:
                Sfx = m_VolumeSlider.value;
                AudioManager.instance.m_SFX = Sfx;
                break;
            default:
                Debug.LogWarning("Volume error unknown source");
                break;
        }
        SaveManager.SaveAudioSettings(this);
    }

    public void OnBatteryToggle(bool isOn)
    {
        if (isOn == true)
        {
            m_BatteryToggleState = true;
        }
        else if (isOn == false)
        {
            m_BatteryToggleState = false;
        }
        SaveManager.SaveAudioSettings(this);
    }

    public void OnMotionToggle(bool isOn)
    {
        if (isOn == true)
        {
            m_MotionToggleState = true;
        }
        else if (isOn == false)
        {
            m_MotionToggleState = false;
        }
        SaveManager.SaveAudioSettings(this);
    }

}
