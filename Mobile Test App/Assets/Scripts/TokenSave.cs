using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TokenSave
{
    public int OwnedCurrencyAmount;

    public TokenSave(CurrencyManager TokenMan)
    {
        OwnedCurrencyAmount = TokenMan.CurrencyAmount;
    }
}
