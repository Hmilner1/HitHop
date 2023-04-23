using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class PlayerSkinCloud
{
    [FirestoreProperty]
    public int CurrentSkin { get; set; }
    [FirestoreProperty]
    public List<int> AllOwnedSkins { get; set; }
}

