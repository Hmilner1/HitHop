using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class PlayerInfoCloud
{
    [FirestoreProperty]
    public float TotalXP { get; set; }
    [FirestoreProperty]
    public float Level { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }
}
