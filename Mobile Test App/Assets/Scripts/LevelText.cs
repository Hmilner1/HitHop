using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;

public class LevelText : MonoBehaviour
{
    private TMP_Text m_LevelText;

    private void Start()
    {
        m_LevelText = GameObject.Find("Level text").GetComponent<TMP_Text>();

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
                    m_LevelText.text = "Level: " + playerInfo.CLevel.ToString();
                });
            }
            else
            {
                m_LevelText.text = "Level: " + info.Level.ToString();
            }
        }
        else 
        {
            m_LevelText.text = "Level: " + 1.ToString();
        }
    }



}
