using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSettings 
{
    public float Master;
    public float Music;
    public float Sfx;

    public AudioSettings(VolumeSlider volume)
    {
        Master = volume.Master;
        Music = volume.Music;
        Sfx = volume.Sfx;
    }
}
