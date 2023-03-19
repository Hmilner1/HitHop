using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSwapBeatMovement : MonoBehaviour
{
    private enum Position
    { 
        HIGH,
        LOW
    }
    public GameObject m_HighSpawn;
    public GameObject m_LowSpawn;

    private Position m_Position;

    [SerializeField]
    private float m_LerpAmount;

    private int m_Pos;
    private int m_HitNum;

    public delegate void HitBeat();
    public static event HitBeat OnBeatHit;

    private void Awake()
    {
        m_HighSpawn = GameObject.Find("HighSpawnPos");
        m_LowSpawn = GameObject.Find("LowSpawnPos");

        if (transform.position.y == m_HighSpawn.transform.position.y)
        {
            m_Position = Position.HIGH;
        }
        else if (transform.position.y == m_LowSpawn.transform.position.y)
        {
            m_Position = Position.LOW;
        }
        else
        {
            Debug.Log("Error Getting LaneSwapBeat Position");
        }
        m_HitNum = 0;
    }

    private void Update()
    {
        
    }

    public void FirstHit()
    {
        m_HitNum++;
        if (m_Position == Position.HIGH)
        {
            this.transform.position = new Vector2(this.transform.position.x, m_LowSpawn.transform.position.y);
            AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
        }
        if (m_Position == Position.LOW)
        {
            this.transform.position = new Vector2(this.transform.position.x, m_HighSpawn.transform.position.y);
            AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
        }

        if (m_HitNum == 2)
        {
            SecondHit();
        }
    }

    private void SecondHit()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
        Destroy(gameObject);
    }

}
