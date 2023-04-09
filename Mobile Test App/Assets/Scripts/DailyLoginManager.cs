using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyLoginManager : MonoBehaviour
{
    public DateTime m_CurrentTime;
    public DateTime m_LastLoginTime;
    public int m_DaysLoggedIn;

    private void Start()
    {
        GameTimeInfo info = SaveManager.LoadTime();
        if (info != null)
        {
            m_LastLoginTime = info.LastLogin;
            m_CurrentTime = info.Time;
            m_DaysLoggedIn = info.DaysLogged;
        }
        else if(info == null)
        {
            m_DaysLoggedIn= 1;
            m_CurrentTime = DateTime.Now;
            SaveManager.SaveTime(this);
        }
        Debug.Log(m_DaysLoggedIn);
        m_CurrentTime = DateTime.Now;
        Debug.Log(m_LastLoginTime);
        if (m_LastLoginTime.Year == 0001 )
        {
            SceneManager.LoadScene("DailyLogIn", LoadSceneMode.Additive);
            m_LastLoginTime = DateTime.Now;
            SaveManager.SaveTime(this);
        }
        else if (m_CurrentTime.Day > m_LastLoginTime.Day)
        {
            m_DaysLoggedIn = m_DaysLoggedIn + 1;
            m_LastLoginTime = DateTime.Now;
            SaveManager.SaveTime(this);
            SceneManager.LoadScene("DailyLogIn", LoadSceneMode.Additive);
        }
    }
}
