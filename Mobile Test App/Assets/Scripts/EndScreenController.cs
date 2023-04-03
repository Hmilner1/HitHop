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
    private TMP_Text m_PlayerName;
    [SerializeField]
    private TMP_Text m_PlayerLevelText;
    [SerializeField]
    private Slider m_XpBar;
 
    private float ScoreAchived;

    //Player Save Data
    public float TotalXP = 0;
    public float Level = 1;
    public string Name = "Name";

    private float TempXp;

    private void OnEnable()
    {
        PlayerActions.OnGetAccuracy += UpdateAccuracy;
    }

    private void OnDisable()
    {
        PlayerActions.OnGetAccuracy -= UpdateAccuracy;
    }

    private void Start()
    {
        PlayerInfo info = SaveManager.LoadPlayerInfo();
        if (info != null)
        {
            TotalXP = info.TotalXP;
            Level = info.Level;
            Name = info.Name;  
        }
        else
        {
            SaveManager.SavePlayerInfo(this);
        }
        m_PlayerName.text = Name;  
        m_ScoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();

        Level = 1;
        m_XpBar.maxValue = Convert.ToSingle(CalculateXP());
    }

    private void Update()
    {
        m_PlayerLevelText.text = Level.ToString();
        if (TempXp >= Convert.ToSingle(CalculateXP()))
        {
            TempXp = TempXp - Convert.ToSingle(CalculateXP());
            Level = Level + 1;
            SaveManager.SavePlayerInfo(this);
        }
        else if (TempXp <= CalculateXP())
        {
            if (m_XpBar.value <= TempXp)
            {
                m_XpBar.maxValue = Convert.ToSingle(CalculateXP());
                m_XpBar.value = m_XpBar.value + 50 * Time.deltaTime;
            }
        }
    }

    private void UpdateAccuracy(float Accuracy)
    {
        ScoreAchived = Accuracy;
        m_ScoreText.text = "Score: " + Accuracy.ToString();

        TotalXP = TotalXP + CalculateXPToGive();
        TempXp = TotalXP;
        SaveManager.SavePlayerInfo(this);
    }

    private double CalculateXP()
    {
        double XpToLevel;

        XpToLevel = Level / 0.07f;
        XpToLevel = Math.Pow(XpToLevel, 2);
        return XpToLevel;
    }

    private float CalculateXPToGive()
    {
        float XpOut;
        XpOut = ScoreAchived / 10;
        return XpOut;
    }
}
