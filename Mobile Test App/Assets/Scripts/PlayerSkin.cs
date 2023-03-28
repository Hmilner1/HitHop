using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSkin
{
    public int CurrentSkin;

    public PlayerSkin(ShopPurcahseButton NewPlayerSkin)
    {
        CurrentSkin = NewPlayerSkin.CurrentSkin;
    }

}

