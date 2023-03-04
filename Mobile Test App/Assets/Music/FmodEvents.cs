using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    [field: Header("Beat Sfx")]
    [field: SerializeField] public EventReference beatDestroySound { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference Track7 { get; private set; }
    [field: SerializeField] public EventReference Track6 { get; private set; }

    public static FmodEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Audio Manager Found!");
        }
        instance = this;
    }
}
