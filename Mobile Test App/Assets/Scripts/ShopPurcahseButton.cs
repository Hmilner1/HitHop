using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPurcahseButton : MonoBehaviour
{
    public List<int> AllUnlockedSkins;
    public int CurrentSkin;
    [SerializeField]
    private int SkinToBuy;

    private void OnEnable()
    {
        EquipSkin.OnSetSkin += SetActiveSkin;
    }

    private void OnDisable()
    {
        EquipSkin.OnSetSkin -= SetActiveSkin;
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
        PlayerSkin info = SaveManager.LoadPlayerSkin();
        AllUnlockedSkins = info.AllOwnedSkins;
        AllUnlockedSkins.Add(SkinToBuy);
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
