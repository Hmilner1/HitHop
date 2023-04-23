using Firebase.Auth;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using UnityEngine.Assertions;

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
    private int Combo;
    private float MaxBeats;
    [SerializeField]
    private float m_LerpAmount;
    //private float Accuracy;
    //private bool multiTouch;

    public delegate void AddPoint(int Score, int CurrentCombo);
    public static event AddPoint OnAddPoint;
    private Animator m_CharacterAnimator;

    public delegate void GetAccuracy(float Accuracy);
    public static event GetAccuracy OnGetAccuracy;

    public delegate void HitBeat();
    public static event HitBeat OnBeatHit;

    [SerializeField]
    private GameObject[] PlayerSkins;

    private void OnEnable()
    {
        PlayerInputs.OnStartTouch += Move;
        PlayerInputs.OnStartTouch += StartClick;
        PlayerInputs.OnEndTouch += StopClick;
        PlayerInputs.OnStartMulti += MultiTouch;
        PlayerInputs.OnEndMulti += MultiTouch;
        BeatSpawn.OnGameEnd += CalculateAccuracy;
        BeatMovement.OnBeatMiss += BeatMissed;
    }
    private void OnDisable()
    {
        PlayerInputs.OnStartTouch -= Move;
        PlayerInputs.OnStartTouch -= StartClick;
        PlayerInputs.OnEndTouch -= StopClick;
        PlayerInputs.OnStartMulti -= MultiTouch;
        PlayerInputs.OnEndMulti -= MultiTouch;
        BeatSpawn.OnGameEnd -= CalculateAccuracy;
        BeatMovement.OnBeatMiss -= BeatMissed;
    }

    private void Awake()
    {
        BeatDestroyed = false;
        m_Clicked = false;
        m_Jump = false;
        //multiTouch = false;
        m_PlayerInputs = new PlayerInputs();

        PlayerSkin info = SaveManager.LoadPlayerSkin();

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string skinInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/SkinData";
            var firestore = FirebaseFirestore.DefaultInstance;

            firestore.Document(skinInfoPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Assert.IsNull(task.Exception);

                var SkinInfo = task.Result.ConvertTo<PlayerSkinCloud>();

                for (int i = 0; i < PlayerSkins.Length; i++)
                {

                    if (i == SkinInfo.CurrentSkin)
                    {
                        PlayerSkins[i].SetActive(true);
                    }
                    else
                    {
                        Destroy(PlayerSkins[i]);
                    }
                }
                m_CharacterAnimator = m_Player.GetComponentInChildren<Animator>();

            });
        }
        else
        {
            for (int i = 0; i < PlayerSkins.Length; i++)
            {

                if (i == info.CurrentSkin)
                {
                    PlayerSkins[i].SetActive(true);
                }
                else
                {
                    Destroy(PlayerSkins[i]);
                }
                m_CharacterAnimator = m_Player.GetComponentInChildren<Animator>();
            }
        }
    }

    private void Start()
    {
        MaxBeats = m_beatspawner.m_BeatsNeeded; 
        BeatsHit = 0;
        m_Score = 0;
        m_Player = GameObject.Find("Player");
    }
    private void FixedUpdate()
    {
        AddAPoint();
        if (m_Jump)
        {
            if (m_CharacterAnimator != null)
            {
                m_CharacterAnimator.SetTrigger("Jump");
            }
            m_timer = m_timer - 1f * Time.deltaTime;
            m_Player.transform.position = Vector2.Lerp(m_Player.transform.position, m_MovePositionHigh.transform.position, m_LerpAmount * Time.deltaTime);
        }
        else if (!m_Jump)
        { 
            m_Player.transform.position = Vector2.Lerp(m_Player.transform.position, m_MovePositionLow.transform.position, m_LerpAmount * Time.deltaTime);
            m_Jump = false;
            m_timer = m_TimerTime;
            if (m_CharacterAnimator != null)
            {
                m_CharacterAnimator.SetTrigger("Running");
            }
        }
        if (m_timer <= 0f)
        {
            m_Player.transform.position = Vector2.Lerp(m_Player.transform.position, m_MovePositionLow.transform.position, m_LerpAmount * Time.deltaTime);
            m_Jump = false;
            m_timer = m_TimerTime;
            if (m_CharacterAnimator != null)
            {
                m_CharacterAnimator.SetTrigger("Running");
            }
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

    private void MultiTouch(bool multiPress)
    {
        if (multiPress == false)
        {
            return;
        }
        else if (multiPress == true)
        {
            SceneManager.LoadScene("LevelEndScreen", LoadSceneMode.Additive);
        }
    }

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
                }
            }
        }

        GameObject Fadebeat;
        if (collision.gameObject.tag == "FadeBeat")
        {
            Fadebeat = collision.gameObject;
            if (m_Jump)
            {
                AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
                Fadebeat.SetActive(false);
                if (!Fadebeat.activeSelf)
                {
                    Destroy(Fadebeat);
                    BeatDestroyed = true;
                }
            }
            else if (m_Clicked && !m_Jump)
            {
                Fadebeat.SetActive(false);
                if (!Fadebeat.activeSelf)
                {
                    AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
                    Destroy(Fadebeat);
                    BeatDestroyed = true;
                }
            }
        }

        GameObject LaneSwapBeat;
        if (collision.gameObject.tag == "LaneSwapBeat")
        {
            LaneSwapBeatMovement SwapScript;
            LaneSwapBeat = collision.gameObject;
            SwapScript = LaneSwapBeat.GetComponent<LaneSwapBeatMovement>();
            if (m_Jump)
            {
                LaneSwapBeatMovement m_SwapScrip = LaneSwapBeat.GetComponent<LaneSwapBeatMovement>();
                m_SwapScrip.FirstHit();
                if (SwapScript.m_HitNum == 2)
                {
                    Destroy(LaneSwapBeat);
                    BeatDestroyed = true;
                }
            }
            else if (m_Clicked && !m_Jump)
            {
                LaneSwapBeatMovement m_SwapScrip = LaneSwapBeat.GetComponent<LaneSwapBeatMovement>();
                m_SwapScrip.FirstHit();
                if (SwapScript.m_HitNum == 2)
                {
                    Destroy(LaneSwapBeat);
                    BeatDestroyed = true;
                }
            }
        }

        GameObject MissBeat;
        if (collision.gameObject.tag == "Miss")
        {
            MissBeat = collision.gameObject;
            if (m_Jump)
            {
                AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
                MissBeat.SetActive(false);
                if (!MissBeat.activeSelf)
                {
                    Destroy(MissBeat);
                    Combo = 0;
                    OnAddPoint?.Invoke(m_Score, Combo);
                }
            }
            else if (m_Clicked && !m_Jump)
            {
                MissBeat.SetActive(false);
                if (!MissBeat.activeSelf)
                {
                    AudioManager.instance.PlayOneShot(FmodEvents.instance.beatDestroySound, this.transform.position);
                    Destroy(MissBeat);
                    Combo = 0;
                    OnAddPoint?.Invoke(m_Score, Combo);
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
            Combo = Combo + 1;
            switch (Combo)
            {
                case < 10:
                    m_Score = m_Score + 10;
                    break;
                case > 30:
                    m_Score = m_Score + 40;
                    break;
                case > 20:
                    m_Score = m_Score + 30;
                    break;
                case > 10:
                    m_Score = m_Score + 20;
                    break;
            }

            OnAddPoint?.Invoke(m_Score, Combo);
            OnBeatHit?.Invoke();
            BeatDestroyed = false;

        }
    }

    private void BeatMissed(string Type)
    {
        if (Type == "Beat")
        {
            Combo = 0;
        }
    }

    private void CalculateAccuracy()
    {
        
        StartCoroutine(SendAccuracy());
    }

    IEnumerator SendAccuracy()
    {
        yield return new WaitForSeconds(0.1f);
        OnGetAccuracy?.Invoke(m_Score);
        StopAllCoroutines();
    }
}