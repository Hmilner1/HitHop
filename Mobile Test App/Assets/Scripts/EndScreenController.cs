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
    //private float XpToGive;
    private float ScoreAchived;

    //Player Save Data
    public float TotalXP = 0;
    public float Level = 1;
    public string Name = "Name";
    //public GameObject Skin;

    private float TempXp;

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
        m_XpBar.maxValue = Convert.ToSingle(CalculateXP());
        TotalXP = TotalXP + Convert.ToSingle(CalculateXP());
        TempXp = TotalXP;
        SaveManager.SavePlayerInfo(this);
        //XpToGive = 450.1f;
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
        m_PlayerLevelText.text = Level.ToString();

        //if (XpToGive >= m_XpBar.maxValue)
        //{
        //    XpToGive = XpToGive - m_XpBar.maxValue;
        //    m_XpBar.value = 0;
        //    Level = Level + 1;
        //    m_XpBar.maxValue = Convert.ToSingle(CalculateXP());

        //}
        //else if (XpToGive >= 0)
        //{
        //    m_XpBar.value = m_XpBar.value + 50 * Time.deltaTime;
        //    XpToGive = XpToGive - 50 * Time.deltaTime;
        //}

        if (TempXp >= CalculateXP())
        {
            TempXp = TempXp - Convert.ToSingle(CalculateXP());
            Level = Level + 1;
            SaveManager.SavePlayerInfo(this);
        }
        else if (TempXp <= CalculateXP())
        {
            if (m_XpBar.value < TempXp)
            {
                m_XpBar.value = m_XpBar.value + 50 * Time.deltaTime;
            }
        }
    }

    private void UpdateAccuracy(float Accuracy)
    {
        ScoreAchived = Accuracy;
        //XpToGive = Convert.ToSingle(CalculateXPToGive());
        m_ScoreText.text = "Score: " + Accuracy.ToString();
    }

    private double CalculateXP()
    {
        double XpToLevel;

        XpToLevel = Level / 0.07f;
        XpToLevel = Math.Pow(XpToLevel, 2);

        return XpToLevel;
    }

    private double CalculateXPToGive()
    {
        double XpOut;

        XpOut = ScoreAchived / 10;

        TotalXP = TotalXP + Convert.ToSingle(XpOut);
        //SaveManager.SavePlayerInfo(this);


        return XpOut;
    }
}
