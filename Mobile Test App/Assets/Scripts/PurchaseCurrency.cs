using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseCurrency : MonoBehaviour
{
    public delegate void PurchaseDisc(int Amount);
    public static event PurchaseDisc OnPurchaseDisc;

    [SerializeField]
    private int ButtonAmount;

    public void OnClickPurchaseDisc()
    {
        OnPurchaseDisc?.Invoke(ButtonAmount);
    }

    public void DisableButton()
    { 
        Button currentButton = this.GetComponent<Button>();
        currentButton.interactable= false;
    }
   
}
