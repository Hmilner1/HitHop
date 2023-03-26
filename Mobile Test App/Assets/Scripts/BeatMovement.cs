using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMovement : MonoBehaviour
{
    BeatSpawn beatSpawner;
    private float m_BPM;

    public delegate void BeatMissed(string type);
    public static event BeatMissed OnBeatMiss;

    private void Start()
    {
        beatSpawner = GameObject.Find("BeatSpawner").GetComponent<BeatSpawn>();
        m_BPM = beatSpawner.m_BPM;
        m_BPM = m_BPM / 60f;
    }

    private void Update()
    {
        transform.position -= new Vector3(m_BPM * Time.deltaTime, 0f);

        if (transform.position.x < -2.7)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
            if (gameObject.tag == "Beat")
            {
                OnBeatMiss?.Invoke("Beat");
            }
            else if (gameObject.tag == "LaneSwapBeat")
            {
                OnBeatMiss?.Invoke("Beat");
            }
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Beat")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}
