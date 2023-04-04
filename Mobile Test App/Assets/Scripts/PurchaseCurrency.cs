using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
