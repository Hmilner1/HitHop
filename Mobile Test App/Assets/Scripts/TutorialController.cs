using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_LeftText;
    [SerializeField]
    private TMP_Text m_RightText;
    [SerializeField]
    private GameObject m_leftImage;
    [SerializeField]
    private GameObject m_rightImage;
    [SerializeField]
    private GameObject m_ScreenSplit;
    [SerializeField]
    private GameObject m_BeatTutOverlay;
    [SerializeField]
    private GameObject m_Beat;
    [SerializeField]
    private GameObject m_MissBeat;
    [SerializeField]
    private GameObject m_Score;
    [SerializeField]
    private GameObject m_MissBeatTut;
    [SerializeField]
    private GameObject m_SwapBeatTut;
    [SerializeField]
    private GameObject m_EndTutScreen;

    public GameObject m_HighSpawn;
    public GameObject m_LowSpawn;


    private bool LeftTap;
    private bool RightTap;
    private bool TapTutFinished;
    private bool BeatTutFinished;
    private bool MissBeatTutFinished;

    private void OnEnable()
    {
        PlayerInputs.OnStartTouch += InputGiven;
    }

    private void OnDisable()
    {
        PlayerInputs.OnStartTouch -= InputGiven;
    }

    private void Start()
    {
        m_BeatTutOverlay.SetActive(false);
        m_MissBeatTut.SetActive(false);
        m_SwapBeatTut.SetActive(false);
        m_EndTutScreen.SetActive(false);
        LeftTap = false;
        RightTap = false;
        TapTutFinished = false;
        BeatTutFinished = false;
        MissBeatTutFinished = false;
    }

    public void InputGiven(Vector2 Pos)
    {
        if (Pos == new Vector2(0, 0))
        {
            return;
        }
        if (Pos.x > Screen.width / 2 && RightTap == false)
        {
            m_RightText.text = "NICE!";
            RightTap = true;
        }
        else if (Pos.x < Screen.width / 2 && LeftTap == false)
        {
            m_LeftText.text = "NICE!";
            LeftTap = true;
        }

        if (LeftTap == true && RightTap == true && TapTutFinished == false)
        {
            StartCoroutine(RemoveText());
            TapTutFinished= true;
        }
    }

    public void OnClickBeatTut()
    {
        m_Score.SetActive(true);
        m_BeatTutOverlay.SetActive(false);
        Instantiate(m_Beat, m_HighSpawn.transform.position, Quaternion.identity);
        if (BeatTutFinished == false)
        {
            StartCoroutine(CheckScore());
        }
    }

    IEnumerator CheckScore()
    {
        yield return new WaitForSeconds(3f);
        TMP_Text scoreText;
        scoreText = m_Score.GetComponent<TMP_Text>();
        if (scoreText.text == "10")
        {
            MissBeatTut();
            BeatTutFinished = true;
        }
        else
        {
            OnClickBeatTut();
        }
    }

    public void OnClickMissTut()
    {
        m_MissBeatTut.SetActive(false);
        m_SwapBeatTut.SetActive(true);
    }

    public void OnClickLaneSwapTut()
    {
        m_SwapBeatTut.SetActive(false);
        Instantiate(m_MissBeat, m_LowSpawn.transform.position, Quaternion.identity);
        if (MissBeatTutFinished == false)
        {
            StartCoroutine(CheckMissScore());
        }
    }

    IEnumerator CheckMissScore()
    {
        yield return new WaitForSeconds(4f);
        TMP_Text scoreText;
        scoreText = m_Score.GetComponent<TMP_Text>();
        if (scoreText.text == "20")
        {
            //MissBeatTut();
            EndTut();
            MissBeatTutFinished = true;
        }
        else
        {
            OnClickMissTut();
        }
    }

    public void EndTut()
    {
        m_EndTutScreen.SetActive(true);
    }

    public void MissBeatTut()
    {
        m_MissBeatTut.SetActive(true);
    }


    IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(1f);
        m_LeftText.text = "";
        m_RightText.text = "";
        m_leftImage.SetActive(false);
        m_rightImage.SetActive(false);
        m_ScreenSplit.SetActive(false);
        m_BeatTutOverlay.SetActive(true);
        StopAllCoroutines();
    }

}
