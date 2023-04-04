using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.NetworkInformation;
using System;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text CurrencyText;

    public int CurrencyAmount = 0;

    public delegate void CurrencyCheck(int SkinNum);
    public static event CurrencyCheck OnCurrencyCheck;

    private void OnEnable()
    {
        ShopPurcahseButton.OnItemPurchased += OnPurchase;
        PurchaseCurrency.OnPurchaseDisc += CurrencyAdded;
    }

    private void OnDisable()
    {
        ShopPurcahseButton.OnItemPurchased -= OnPurchase;
        PurchaseCurrency.OnPurchaseDisc -= CurrencyAdded;
    }

    private void Start()
    {
        TokenSave info = SaveManager.LoadToken();
        if (info != null)
        {
            CurrencyAmount = info.OwnedCurrencyAmount;
        }
        else
        {
            SaveManager.SaveToken(this);
        }
        //CurrencyAmount = Int32.Parse(CurrencyText.text.ToString());
        CurrencyText.text = CurrencyAmount.ToString();
        //Debug.Log(CurrencyAmount);
    }

    public void OnPurchase(int CostAmount, int SkinNum)
    {
        if (CurrencyAmount >= CostAmount)
        {
            CurrencyAmount = CurrencyAmount - CostAmount;
            SaveManager.SaveToken(this);
            CurrencyText.text = CurrencyAmount.ToString();
            OnCurrencyCheck?.Invoke(SkinNum);
        }
    }

    public void CurrencyAdded(int Amount)
    { 
        CurrencyAmount = CurrencyAmount + Amount;
        SaveManager.SaveToken(this);
        CurrencyText.text = CurrencyAmount.ToString();
    }


}
