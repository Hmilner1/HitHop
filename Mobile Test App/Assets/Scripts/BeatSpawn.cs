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
                float spawnpoint = Random.Range(1, 6);
                if (spawnpoint == 1 || spawnpoint == 4)
                {

                    Instantiate(m_Beat, new Vector3(m_HighSpawn.transform.position.x, m_HighSpawn.transform.position.y, m_HighSpawn.transform.position.z), Quaternion.identity);
                    m_BeatsNeeded--;
                }
                else if (spawnpoint == 2 || spawnpoint == 5)
                {

                    Instantiate(m_Beat, new Vector3(m_LowSpawn.transform.position.x, m_LowSpawn.transform.position.y, m_LowSpawn.transform.position.z), Quaternion.identity);
                    m_BeatsNeeded--;
                }
                else if (spawnpoint == 3)
                {
                    float spawnpointDontHit = Random.Range(1, 3);

                    if (spawnpointDontHit == 1)
                    {

                        Instantiate(m_DontHitBeat, new Vector3(m_HighSpawn.transform.position.x, m_HighSpawn.transform.position.y, m_HighSpawn.transform.position.z), Quaternion.identity);
                        m_BeatsNeeded--;
                    }
                    else if (spawnpointDontHit == 2)
                    {

                        Instantiate(m_DontHitBeat, new Vector3(m_LowSpawn.transform.position.x, m_LowSpawn.transform.position.y, m_LowSpawn.transform.position.z), Quaternion.identity);
                        m_BeatsNeeded--;
                    }
                }
                else if (spawnpoint == 6)
                {
                    float spawnpointDontHit = Random.Range(1, 3);

                    if (spawnpointDontHit == 1)
                    {

                        Instantiate(m_LaneSwapBeat, new Vector3(m_HighSpawn.transform.position.x, m_HighSpawn.transform.position.y, m_HighSpawn.transform.position.z), Quaternion.identity);
                        m_BeatsNeeded--;
                    }
                    else if (spawnpointDontHit == 2)
                    {

                        Instantiate(m_LaneSwapBeat, new Vector3(m_LowSpawn.transform.position.x, m_LowSpawn.transform.position.y, m_LowSpawn.transform.position.z), Quaternion.identity);
                        m_BeatsNeeded--;
                    }
                }
            }
        }
    }
}
