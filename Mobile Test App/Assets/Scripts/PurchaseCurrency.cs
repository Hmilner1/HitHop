using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

public class PurchaseCurrency : MonoBehaviour
{
    public delegate void PurchaseDisc(int Amount);
    public static event PurchaseDisc OnPurchaseDisc;

    [SerializeField]
    GameObject m_FailPanel;

    [SerializeField]
    private int ButtonAmount;

    public void OnClickPurchaseDisc(Product Purchase)
    {
        OnPurchaseDisc?.Invoke(Convert.ToInt32(Purchase.definition.payout.quantity));
    }

    public void DisableButton()
    { 
        Button currentButton = this.GetComponent<Button>();
        currentButton.interactable= false;
    }

    public void FailedPurchase(Product Purchase, PurchaseFailureReason reasonForFailure)
    {
        TMP_Text m_FailMessege;
        m_FailMessege = m_FailPanel.GetComponentInChildren<TMP_Text>();
        m_FailMessege.text = "Purchase Failed Due To " + reasonForFailure.ToString();
        m_FailPanel.SetActive(true);
        
    }
   
}
