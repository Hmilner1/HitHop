using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class EndScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_ScoreText;
    [SerializeField]
    private Slider m_XpBar;
    private float XpToGive;
    private float ScoreAchived;

    private float m_CurrentLevel = 1;

    private void Start()
    {
        m_ScoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        m_XpBar.maxValue = Convert.ToSingle(CalculateXP());
        XpToGive = 204.1f;
    }

    private void OnEnable()
    {
        PlayerActions.OnGetAccuracy += UpdateAccuracy;
    }

    private void OnDisable()
    {
        PlayerActions.OnGetAccuracy -= UpdateAccuracy;
    }

    private void Update()
    {
        if (XpToGive >= m_XpBar.value)
        {
            m_XpBar.value = m_XpBar.value + 50 * Time.deltaTime;
        }
    }

    private void UpdateAccuracy(float Accuracy)
    {
        ScoreAchived= Accuracy;
        XpToGive = Convert.ToSingle(CalculateXPToGive());
        m_ScoreText.text = "Score: " + Accuracy.ToString();
    }

    private double CalculateXP()
    {
        double XpToLevel;

        XpToLevel = m_CurrentLevel / 0.07f;
        XpToLevel = Math.Pow(XpToLevel, 2);

        return XpToLevel;
    }

    private double CalculateXPToGive()
    {
        double XpOut;

        XpOut = ScoreAchived / 10;

        return XpOut;
    }
}
