using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private void OnEnable() => PlayerActions.OnAddPoint += AddPoints;
    private void OnDisable() => PlayerActions.OnAddPoint -= AddPoints;

    private TMP_Text M_ScoreText;
    private Canvas m_GameCanvas;

    private int m_Score = 0;

    private void Start()
    {
        m_GameCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        M_ScoreText = m_GameCanvas.GetComponentInChildren<TMP_Text>();
    }

    private void AddPoints(int score)
    {
        m_Score = score;
        M_ScoreText.text = score.ToString();
    }
}
