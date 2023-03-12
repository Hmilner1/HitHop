using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerInputs m_PlayerInputs;
    [SerializeField]
    private BeatSpawn m_beatspawner;

    public float m_TimerTime;
    public float m_timer = 10f;
    private bool m_Clicked;
    private bool m_Jump;
    private int m_Score;
    private GameObject m_Player;
    public GameObject m_MovePositionHigh;
    public GameObject m_MovePositionLow;
    public bool BeatDestroyed;
    private float BeatsHit;
    private float MaxBeats;
    [SerializeField]
    private float m_LerpAmount;

    public delegate void AddPoint(int Score);
    public static event AddPoint OnAddPoint;
    private Animator m_CharacterAnimator;

    private void OnEnable()
    {
        PlayerInputs.OnStartTouch += Move;
        PlayerInputs.OnStartTouch += StartClick;
        //PlayerInputs.OnEndTouch += MoveBack;
        PlayerInputs.OnEndTouch += StopClick;

    }
    private void OnDisable()
    {
        PlayerInputs.OnStartTouch -= Move;
        PlayerInputs.OnStartTouch -= StartClick;
        //PlayerInputs.OnEndTouch -= MoveBack;
        PlayerInputs.OnEndTouch -= StopClick;
    }

    private void Awake()
    {
        BeatDestroyed = false;
        m_Clicked = false;
        m_Jump = false;
        m_PlayerInputs = new PlayerInputs();
    }

    private void Start()
    {
        MaxBeats = m_beatspawner.m_BeatsNeeded; 
        BeatsHit = 0;
        m_Score = 0;
        m_Player = GameObject.Find("Player");
        m_CharacterAnimator = m_Player.GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        AddAPoint();
        if (m_Jump)
        {
            m_timer = m_timer - 1f * Time.deltaTime;
            m_Player.transform.position = Vector2.Lerp(m_Player.transform.position, m_MovePositionHigh.transform.position, m_LerpAmount * Time.deltaTime);
            m_CharacterAnimator.SetTrigger("Jump");
        }
        else if (!m_Jump)
        { 
            m_Player.transform.position = Vector2.Lerp(m_Player.transform.position, m_MovePositionLow.transform.position, m_LerpAmount * Time.deltaTime);
            m_Jump = false;
            m_timer = m_TimerTime;
            m_CharacterAnimator.SetTrigger("Running");
        }
        if (m_timer <= 0f)
        {
            m_Player.transform.position = Vector2.Lerp(m_Player.transform.position, m_MovePositionLow.transform.position, m_LerpAmount * Time.deltaTime);
            m_Jump = false;
            m_timer = m_TimerTime;
            m_CharacterAnimator.SetTrigger("Running");
        }
    }

    private void Move(Vector2 position)
    {
        if (position.x > Screen.width / 2)
        {
            m_Jump = true;
        }
        else if (position.x < Screen.width / 2)
        {
            m_Jump = false;
        }
    }

    //private void MoveBack(Vector2 position)
    //{
    // //  m_Player.transform.position = m_MovePositionLow.transform.position;
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject beat;
        if (collision.gameObject.tag == "Beat")
        {
            beat = collision.gameObject;
            if (m_Jump)
            {
                AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
                beat.SetActive(false);
                if (!beat.activeSelf)
                {
                    Destroy(beat);
                    BeatDestroyed = true;
                    Debug.Log("Nice");
                }
            }
            else if (m_Clicked && !m_Jump)
            {
                beat.SetActive(false);
                if (!beat.activeSelf)
                {
                    AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
                    Destroy(beat);
                    BeatDestroyed = true;
                    Debug.Log("Nice");
                }
            }
        }
    }

    private void StartClick(Vector2 position)
    {
        if (position.x < Screen.width / 2)
        {
            m_Clicked = true;
        }
    }


    private void StopClick(Vector2 position)
    {
        m_Clicked = false;
    }

    private void AddAPoint()
    {
        if (BeatDestroyed)
        {
            BeatsHit = BeatsHit + 1;
            m_Score = m_Score + 1;
            OnAddPoint?.Invoke(m_Score);
            BeatDestroyed = false;
        }
    }

    private void CalculateAccuracy()
    {
        float Accuracy;

        Accuracy = BeatsHit / MaxBeats;
        Accuracy = Accuracy * 100;
    }
}