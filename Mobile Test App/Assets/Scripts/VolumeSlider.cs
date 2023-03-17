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

    private void Awake()
    {
        AudioSettings settings =  SaveManager.LoadAudioSettings();
        if (settings != null)
        {
            Master = settings.Master;
            Music = settings.Music;
            Sfx = settings.Sfx;
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

}
