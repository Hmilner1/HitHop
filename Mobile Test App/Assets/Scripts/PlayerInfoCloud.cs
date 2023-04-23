using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class PlayerInfoCloud
{
    [FirestoreProperty]
    public float CTotalXP { get; set; }
    [FirestoreProperty]
    public float CLevel { get; set; }
    [FirestoreProperty]
    public string CName { get; set; }
}
