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
    private GameObject m_HighSpawn;
    private GameObject m_LowSpawn;

    private Position m_Position;

    [SerializeField]
    private float m_LerpAmount;

    private int m_Pos;
    public int m_HitNum;
    private float m_RayCastRange = 10f;


    public delegate void HitBeat();
    public static event HitBeat OnBeatHit;

    [SerializeField]
    private Rigidbody2D m_Rigidbody;

    private int layerMask = 1 << 3;
    private Transform m_Transform;

    [SerializeField]
    private GameObject m_TestObject;

    private float timer = 0;

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
        timer = timer + 1f * Time.deltaTime;
        if (timer < 2f)
        {
            RaycastHit hit;
            RaycastHit hit2;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * m_RayCastRange, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * m_RayCastRange, Color.yellow);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, m_RayCastRange, layerMask))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
                Debug.Log("Did Hit");
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit2, m_RayCastRange, layerMask))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
                Debug.Log("Did Hit");
            }
        }
        m_Transform = m_TestObject.transform;
    }

    public void FirstHit()
    {
        m_HitNum++;
        if (m_Position == Position.HIGH)
        {
            m_Transform.position = new Vector3(m_Transform.position.x, m_LowSpawn.transform.position.y, 0);
            AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
        }
        if (m_Position == Position.LOW)
        {
            m_Transform.position = new Vector3(m_Transform.position.x, m_HighSpawn.transform.position.y, 0);
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
       // Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Miss")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }
}
