using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class EndScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_ScoreText;

    private void Start()
    {
        m_ScoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        PlayerActions.OnGetAccuracy += UpdateAccuracy;
    }

    private void OnDisable()
    {
        PlayerActions.OnGetAccuracy -= UpdateAccuracy;
    }

    private void UpdateAccuracy(float Accuracy)
    {
        m_ScoreText.text = "Score: " + Accuracy.ToString();
    }
}
