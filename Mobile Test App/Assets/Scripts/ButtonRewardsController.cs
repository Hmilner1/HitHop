using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRewardsController : MonoBehaviour
{
    private Button[] buttonRewards;

    private void Awake()
    {
        buttonRewards = this.GetComponentsInChildren<Button>();

        GameTimeInfo Info = SaveManager.LoadTime();

        for (int i = 0; i < 31; i++)
        {
            buttonRewards[i].GetComponent<Image>().color = new Color(buttonRewards[i].GetComponent<Image>().color.r, buttonRewards[i].GetComponent<Image>().color.g, buttonRewards[i].GetComponent<Image>().color.b, 0.5f);
            if (i < Info.DaysLogged)
            {
                buttonRewards[i].GetComponent<Image>().color = new Color(buttonRewards[i].GetComponent<Image>().color.r, buttonRewards[i].GetComponent<Image>().color.g, buttonRewards[i].GetComponent<Image>().color.b, 1f);
            }
        }

        
    }
}
