using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "SO/Song")]
public class SongSO : ScriptableObject
{
    [Header("Song Name")]
    public string SongName;

    [Header("Audio Clip")]
    public EventReference Song;

    [Header("BPM")]
    public float BPM;

    [Header("Song Length")]
    public float Length;

    [Header("Spawn % 0= Fade, 1= LaneSwap, 2= Dont Hit, 3= Beat")]
    public int[] BeatSpawnChance;
    
}
