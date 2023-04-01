using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static PlayerActions;

public class EquipSkin : MonoBehaviour
{
    private ShopPurcahseButton m_PurchaseButton;
    [SerializeField]
    private int SkinIndex;

    public delegate void SetSkin(int skinNum);
    public static event SetSkin OnSetSkin;

    public void OnClickEquip()
    {
        OnSetSkin?.Invoke(SkinIndex);
    }
}
