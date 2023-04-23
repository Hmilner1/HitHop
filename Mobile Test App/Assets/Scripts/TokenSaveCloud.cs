using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class TokenSaveCloud
{
    [FirestoreProperty]
    public int OwnedCurrencyAmount { get; set; }

}
