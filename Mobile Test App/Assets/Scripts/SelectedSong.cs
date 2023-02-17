using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSong : MonoBehaviour
{
    public SongSO m_SongSO;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

}
