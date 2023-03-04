using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawn : MonoBehaviour
{
    private SelectedSong m_SelectedSong;
    private SongSO m_Song;
    public float m_BPM;
    public float m_BeatsNeeded;
    private AudioSource audioSource;
    public GameObject m_Beat;
    public GameObject m_HighSpawn;
    public GameObject m_LowSpawn;
    public float TimerTime;
    public float timer = 10f;
    private float m_SongLength;

    private void Start()
    {
        m_SelectedSong = GameObject.Find("SongManger").GetComponent<SelectedSong>();
        m_Song = m_SelectedSong.m_SongSO;
        audioSource = GetComponent<AudioSource>();
        // audioSource.clip = m_Song.Song;
        AudioManager.instance.InitMusic(m_Song.Song);
        m_BPM = m_Song.BPM;
        timer = TimerTime;
        timer = Mathf.FloorToInt(timer % 60);
        m_SongLength = m_Song.Length;
        m_BeatsNeeded = m_BPM* (m_SongLength/60);
        audioSource.Play();
    }

   private void Update()
   {
        timer = timer - 1f * Time.deltaTime;
        if (timer < 0f)
        {
            SpawnBeat();
            timer = TimerTime;
            timer = Mathf.FloorToInt(timer % 60);
        }

        if (m_BeatsNeeded <= 0)
        {
            Debug.Log("GameOver");
        }
   }
    private void SpawnBeat()
    {
        if (m_BeatsNeeded > 0)
        {
            float spawnAmount = m_BPM / 60;
            for (int i = 0; i < spawnAmount; i++)
            {
                float spawnpoint = Random.Range(1, 3);
                if (spawnpoint == 1)
                {
                    
                    Instantiate(m_Beat, new Vector3(m_HighSpawn.transform.position.x , m_HighSpawn.transform.position.y, m_HighSpawn.transform.position.z), Quaternion.identity);
                    m_BeatsNeeded--;
                }
                else if (spawnpoint == 2)
                {
                    
                    Instantiate(m_Beat, new Vector3(m_LowSpawn.transform.position.x , m_LowSpawn.transform.position.y, m_LowSpawn.transform.position.z), Quaternion.identity);
                    m_BeatsNeeded--;
                }
            }
        }
    }
}
