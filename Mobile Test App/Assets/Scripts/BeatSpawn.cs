using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatSpawn : MonoBehaviour
{
    private SelectedSong m_SelectedSong;
    private SongSO m_Song;
    public float m_BPM;
    public float m_BeatsNeeded;

    public GameObject m_Beat;
    public GameObject m_LaneSwapBeat;
    public GameObject m_DontHitBeat;
    public GameObject m_FadeBeat;

    [SerializeField]
    private GameObject[] m_BeatList;

    public GameObject m_HighSpawn;
    public GameObject m_LowSpawn;
    public float TimerTime;
    public float timer = 10f;
    private float m_SongLength;
    private bool m_LoadedEndScreen;

    public delegate void GetAccuracy();
    public static event GetAccuracy OnGameEnd;

    private void Start()
    {
        m_SelectedSong = GameObject.Find("SongManger").GetComponent<SelectedSong>();
        m_Song = m_SelectedSong.m_SongSO;
        AudioManager.instance.InitMusic(m_Song.Song);
        m_BPM = m_Song.BPM;
        timer = TimerTime;
        timer = Mathf.FloorToInt(timer % 60);
        m_SongLength = m_Song.Length;
        m_BeatsNeeded = m_BPM* (m_SongLength/60);
        m_LoadedEndScreen = false;
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
            if (m_LoadedEndScreen == false)
            {
                StartCoroutine(EndGame());
            }

        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("LevelEndScreen", LoadSceneMode.Additive);
        m_LoadedEndScreen=true;
        OnGameEnd?.Invoke();
        StopAllCoroutines();

    }

    private void SpawnBeat()
    {
        if (m_BeatsNeeded > 0)
        {
            float spawnAmount = m_BPM / 60;

            for (int i = 0; i < spawnAmount; i++)
            {
                Instantiate(m_BeatList[ChoseBeat()], ChoseSpawnPos(), Quaternion.identity);
                m_BeatsNeeded--;
            }
        }
    }

    private int ChoseBeat()
    {
        float RandomBeatNum = Random.Range(1, 100);
        if (RandomBeatNum <= m_Song.BeatSpawnChance[0])
        {
            return 3;
        }
        else if (RandomBeatNum <= m_Song.BeatSpawnChance[1])
        {
            return 2;
        }
        else if (RandomBeatNum <= m_Song.BeatSpawnChance[2])
        {
            return 1;
        }
        else if (RandomBeatNum <= m_Song.BeatSpawnChance[3])
        {
            return 0;
        }
        return 0;
    }

    private Vector3 ChoseSpawnPos()
    {
        float RandomBeatSpawn = Random.Range(1, 100);
        if (RandomBeatSpawn <= 50)
        {
            return new Vector3(m_LowSpawn.transform.position.x, m_LowSpawn.transform.position.y, m_LowSpawn.transform.position.z);
        }
        else if (RandomBeatSpawn <= 100)
        {
           return new Vector3(m_HighSpawn.transform.position.x, m_HighSpawn.transform.position.y, m_HighSpawn.transform.position.z);
        }
        return new Vector3(0,0,0);
    }
}
