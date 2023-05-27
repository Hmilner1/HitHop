using Firebase.Auth;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using UnityEngine.Assertions;

public class UnlockManager : MonoBehaviour
{
    [SerializeField]
    private Button[] m_UnlockButtons;

    private void Start()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            UnlockFireBaseRead();
        }
        else
        {
            UnlockLocalRead();
        }
    }

    private void UnlockFireBaseRead()
    {
        string playerInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/PlayerData";
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Document(playerInfoPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);

            var playerInfo = task.Result.ConvertTo<PlayerInfoCloud>();

            for (int i = 0; i < m_UnlockButtons.Length; i++)
            {
                if (i + 1 > playerInfo.CLevel)
                {
                    m_UnlockButtons[i].GetComponent<Image>().color = new Color(m_UnlockButtons[i].GetComponent<Image>().color.r, m_UnlockButtons[i].GetComponent<Image>().color.g, m_UnlockButtons[i].GetComponent<Image>().color.b, 0.5f);
                }
            }
        });
    }

    private void UnlockLocalRead()
    {
        PlayerInfo info = SaveManager.LoadPlayerInfo();

        if (info != null)
        {
            for (int i = 0; i < m_UnlockButtons.Length; i++)
            {
                if (i + 1 > info.Level)
                {
                    m_UnlockButtons[i].GetComponent<Image>().color = new Color(m_UnlockButtons[i].GetComponent<Image>().color.r, m_UnlockButtons[i].GetComponent<Image>().color.g, m_UnlockButtons[i].GetComponent<Image>().color.b, 0.5f);
                }
            }

        }
        else
        {
            for (int i = 0; i < m_UnlockButtons.Length; i++)
            {
                if (i > 0)
                {
                    m_UnlockButtons[i].GetComponent<Image>().color = new Color(m_UnlockButtons[i].GetComponent<Image>().color.r, m_UnlockButtons[i].GetComponent<Image>().color.g, m_UnlockButtons[i].GetComponent<Image>().color.b, 0.5f);
                }
            }
        }
    }

}
