using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    [SerializeField]
    private Button[] m_UnlockButtons;

    private void Update()
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
