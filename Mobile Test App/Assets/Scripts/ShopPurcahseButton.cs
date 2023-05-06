using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

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
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string skinInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/SkinData";
            var firestore = FirebaseFirestore.DefaultInstance;
            if (firestore.Document(skinInfoPath) == null)
            {
                var skinInfo = new PlayerSkinCloud
                {
                    //OwnedCurrencyAmount = info.OwnedCurrencyAmount,
                    AllOwnedSkins = AllUnlockedSkins,
                    CurrentSkin = 0,
                };
                firestore.Document(skinInfoPath).SetAsync(skinInfo);
            }
            else
            {
                firestore.Document(skinInfoPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    Assert.IsNull(task.Exception);

                    var SkinInfo = task.Result.ConvertTo<PlayerSkinCloud>();
                    CurrentSkin = SkinInfo.CurrentSkin;

                });
            }

            if (info != null)
            {
                CurrentSkin = info.CurrentSkin;
            }
            else
            {
                CurrentSkin = 0;
                SaveManager.SavePlayerSkin(this);
            }
        }
        else
        {
            if (info != null)
            {
                CurrentSkin = info.CurrentSkin;
            }
            else
            {
                SaveManager.SavePlayerSkin(this);

                if (FirebaseAuth.DefaultInstance.CurrentUser != null)
                {
                    string skinInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/SkinData";
                    var skinInfo = new PlayerSkinCloud
                    {
                        //OwnedCurrencyAmount = info.OwnedCurrencyAmount,
                        AllOwnedSkins = info.AllOwnedSkins,
                        CurrentSkin = info.CurrentSkin,
                    };
                    var firestore = FirebaseFirestore.DefaultInstance;
                    firestore.Document(skinInfoPath).SetAsync(skinInfo);
                }
            }
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
        if (CheckSkinList(SkinNum) == false)
        {
            AllUnlockedSkins.Add(SkinNum);
        }
        SaveManager.SavePlayerSkin(this);

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string skinInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/SkinData";
            var skinInfo = new PlayerSkinCloud
            {
                AllOwnedSkins = AllUnlockedSkins,
                CurrentSkin = CurrentSkin,
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(skinInfoPath).SetAsync(skinInfo);
        }
    }

    private bool CheckSkinList(int Skin)
    {
        foreach (var sk in AllUnlockedSkins)
        {
            if (Skin == sk)
            { 
                return true;
            }
        }
        return false;
    }

    public void SetActiveSkin(int Skin)
    {
        PlayerSkin info = SaveManager.LoadPlayerSkin();
        AllUnlockedSkins = info.AllOwnedSkins;
        CurrentSkin = Skin;
        SaveManager.SavePlayerSkin(this);

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string skinInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/SkinData";
            var skinInfo = new PlayerSkinCloud
            {
                AllOwnedSkins = info.AllOwnedSkins,
                CurrentSkin= Skin,
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(skinInfoPath).SetAsync(skinInfo);
        }
    }
}
