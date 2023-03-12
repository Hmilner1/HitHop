using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [Header("Volume")]
    [Range(0, 1)]
    public float m_MasterVolume = 1;
    [Range(0, 1)]
    public float m_Music = 1;
    [Range(0, 1)]
    public float m_SFX = 1;

    private Bus masterBus;

    public Bus musicBus;
    private Bus sfxBus;

    private EventInstance musicEventInstance;

    private List<EventInstance> eventInstances;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance != null)
        {
            Debug.LogError("More than one Audio Manager Found!");
        }
        instance = this;
        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Update()
    {
        masterBus.setVolume(m_MasterVolume);
        musicBus.setVolume(m_Music);
        sfxBus.setVolume(m_SFX);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void PlayOneShot(EventReference sound, Vector3 woldPos)
    {
        RuntimeManager.PlayOneShot(sound, woldPos);
    }

    public void InitMusic(EventReference musicEventReferance)
    {
        musicEventInstance = CreateInstance(musicEventReferance);
        musicEventInstance.start();
     
    }
}
