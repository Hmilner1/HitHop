using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[Serializable]
public class GameTimeInfo 
{
    public DateTime Time;
    public DateTime LastLogin;
    public int DaysLogged;

    public GameTimeInfo(DailyLoginManager DailyLogin)
    {
        Time = DailyLogin.m_CurrentTime;
        LastLogin = DailyLogin.m_LastLoginTime;
        DaysLogged = DailyLogin.m_DaysLoggedIn;
    }
}
