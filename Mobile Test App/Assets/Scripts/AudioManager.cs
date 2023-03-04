using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;

    private List<EventInstance> eventInstances;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Audio Manager Found!");
        }
        instance = this;
        eventInstances = new List<EventInstance>();
    }
    private void Start()
    {
        
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