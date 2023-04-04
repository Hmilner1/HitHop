using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPurcahseButton : MonoBehaviour
{
    public List<int> AllUnlockedSkins;
    public int CurrentSkin;
    [SerializeField]
    private int SkinToBuy;
    [SerializeField]
    private int ItemCost;

    public delegate void ItemPurchased(int CostAmount, int SkinNum);
    public static event ItemPurchased OnItemPurchased;

    private void OnEnable()
    {
        EquipSkin.OnSetSkin += SetActiveSkin;
        CurrencyManager.OnCurrencyCheck += CompletePurchase;
    }

    private void OnDisable()
    {
        EquipSkin.OnSetSkin -= SetActiveSkin;
        CurrencyManager.OnCurrencyCheck -= CompletePurchase;
    }

    private void Awake()
    {
        PlayerSkin info = SaveManager.LoadPlayerSkin();
        if (info != null)
        {
            CurrentSkin = info.CurrentSkin;
        }
        else
        {
            SaveManager.SavePlayerSkin(this);
        }
    }

    public void OnClickBuy()
    {
        OnItemPurchased?.Invoke(ItemCost, SkinToBuy);
    }

    public void CompletePurchase(int SkinNum)
    {
        PlayerSkin info = SaveManager.LoadPlayerSkin();
        AllUnlockedSkins = info.AllOwnedSkins;
        AllUnlockedSkins.Add(SkinNum);
        SaveManager.SavePlayerSkin(this);
    }

    public void SetActiveSkin(int Skin)
    {
        PlayerSkin info = SaveManager.LoadPlayerSkin();
        AllUnlockedSkins = info.AllOwnedSkins;
        CurrentSkin = Skin;
        SaveManager.SavePlayerSkin(this);
    }
}
