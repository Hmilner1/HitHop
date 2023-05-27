using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxSettings : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        AudioSettings info = SaveManager.LoadAudioSettings();
        if (info != null)
        {
            if (info.MToggleState == false)
            {
                start = true;
            }
            else
            {
                start = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
        }
    }
}
