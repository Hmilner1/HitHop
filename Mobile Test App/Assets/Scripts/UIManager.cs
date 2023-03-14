using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private void OnEnable() => PlayerActions.OnAddPoint += AddPoints;
    private void OnDisable() => PlayerActions.OnAddPoint -= AddPoints;

    [SerializeField]
    private TMP_Text m_ScoreText;
    [SerializeField]
    private TMP_Text m_ComboText;

    private void Start()
    {

    }

    private void AddPoints(int score, int Combo)
    {
        m_ScoreText.text = score.ToString();

        m_ComboText.text = Combo.ToString() + "X";
    }
}
