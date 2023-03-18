using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnText : MonoBehaviour
{
    [SerializeField]
    private GameObject MissText;
    [SerializeField]
    private GameObject HitText;
    [SerializeField]
    private Canvas TextCanvas;


    private void OnEnable()
    {
        BeatMovement.OnBeatMiss += SpawnMissText;
        PlayerActions.OnBeatHit += SpawnHitText;
    }

    private void OnDisable()
    {
        BeatMovement.OnBeatMiss -= SpawnMissText;
        PlayerActions.OnBeatHit -= SpawnHitText;
    }

    private void SpawnMissText(string Type)
    {
        if (Type == "Beat")
        {
            Instantiate(MissText, TextCanvas.transform);
        }
    }

    private void SpawnHitText()
    {
        Instantiate(HitText, TextCanvas.transform);
    }
}
