using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;

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
            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                string playerInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/PlayerData";
                var firestore = FirebaseFirestore.DefaultInstance;

                firestore.Document(playerInfoPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    Assert.IsNull(task.Exception);

                    var playerInfo = task.Result.ConvertTo<PlayerInfoCloud>();
                    TotalXP = playerInfo.CTotalXP;
                    Level = playerInfo.CLevel;
                    Name = playerInfo.CName;

                });
            }
            else
            {
                TotalXP = info.TotalXP;
                Level = info.Level;
                Name = info.Name;
            }
        }
        else
        {
            SaveManager.SavePlayerInfo(this);

            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                string playerInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/PlayerData";
                var playerInfo = new PlayerInfoCloud
                {
                    CTotalXP = info.TotalXP,
                    CLevel = info.Level,
                    CName = info.Name,

                };
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Document(playerInfoPath).SetAsync(playerInfo);
            }
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

            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                string playerInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/PlayerData";
                var playerInfo = new PlayerInfoCloud
                {
                    CLevel = Level
                };
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Document(playerInfoPath).SetAsync(playerInfo);
            }
        }
        else if (TempXp <= CalculateXP())
        {
            if (m_XpBar.value <= TempXp)
            {
                m_XpBar.maxValue = Convert.ToSingle(CalculateXP());
                m_XpBar.value = Mathf.Lerp(m_XpBar.value, TempXp, Time.deltaTime*1);
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

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string playerInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/PlayerData";
            var playerInfo = new PlayerInfoCloud
            {
                CTotalXP = TotalXP
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(playerInfoPath).SetAsync(playerInfo);
        }
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
