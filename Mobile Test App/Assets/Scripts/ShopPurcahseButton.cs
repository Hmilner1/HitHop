using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPurcahseButton : MonoBehaviour
{
    public int CurrentSkin;
    [SerializeField]
    private int SkinToBuy;

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
        CurrentSkin = SkinToBuy;
        SaveManager.SavePlayerSkin(this);
    }
}
