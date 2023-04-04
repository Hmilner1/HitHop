using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelText : MonoBehaviour
{
    private TMP_Text m_LevelText;

    private void Start()
    {
        m_LevelText = GameObject.Find("Level text").GetComponent<TMP_Text>();

        PlayerInfo info = SaveManager.LoadPlayerInfo();
        m_LevelText.text = "Level: " + info.Level.ToString();
    }



}
