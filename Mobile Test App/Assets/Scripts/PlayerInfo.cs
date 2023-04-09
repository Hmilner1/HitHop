using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo 
{
    public float TotalXP;
    public float Level;
    public string Name;
    //public GameObject CurrentSkin;

    public PlayerInfo(EndScreenController player)
    {
        TotalXP = player.TotalXP;
        Level = player.Level;
        Name = player.Name;
        //CurrentSkin = player.Skin;
    }

}
