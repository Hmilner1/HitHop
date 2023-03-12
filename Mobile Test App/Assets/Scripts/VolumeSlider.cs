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

    private void Awake()
    {

    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                m_VolumeSlider.value = AudioManager.instance.m_MasterVolume;
                break;

            case VolumeType.MUSIC:
                m_VolumeSlider.value = AudioManager.instance.m_Music;
                break;

            case VolumeType.SFX:
                m_VolumeSlider.value = AudioManager.instance.m_SFX;
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
                AudioManager.instance.m_MasterVolume = m_VolumeSlider.value;
                break;

            case VolumeType.MUSIC:
                AudioManager.instance.m_Music = m_VolumeSlider.value;
                break;

            case VolumeType.SFX:
                AudioManager.instance.m_SFX = m_VolumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume error unknown source");
                break;
        }

    }

}
