using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Firebase.Auth;
using Firebase.Firestore;

public static class SaveManager
{
    public static void SaveAudioSettings(VolumeSlider Slider)
    {
        BinaryFormatter AudioFormatter = new BinaryFormatter();
        string AudioPath = Application.persistentDataPath + "/AudioSettings.data";
        FileStream fileStream = new FileStream(AudioPath, FileMode.Create);

        AudioSettings settings = new AudioSettings(Slider);


        AudioFormatter.Serialize(fileStream, settings);
        fileStream.Close();
    }

    public static AudioSettings LoadAudioSettings()
    {
        string AudioPath = Application.persistentDataPath + "/AudioSettings.data";
        if (File.Exists(AudioPath))
        {
            BinaryFormatter AudioFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(AudioPath, FileMode.Open);

            AudioSettings Settings = AudioFormatter.Deserialize(fileStream) as AudioSettings;
            fileStream.Close();
            return Settings;
        }
        else
        {
           // Debug.Log("Audio save not found");
            return null;
        }
    }

    public static void SavePlayerInfo(EndScreenController playerstats)
    {
        BinaryFormatter PlayerFormatter = new BinaryFormatter();
        string PlayerPath = Application.persistentDataPath + "/Player.data";
        FileStream fileStream = new FileStream(PlayerPath, FileMode.Create);

        PlayerInfo settings = new PlayerInfo(playerstats);


        PlayerFormatter.Serialize(fileStream, settings);
        fileStream.Close();
    }

    public static PlayerInfo LoadPlayerInfo()
    {
        string PlayerPath = Application.persistentDataPath + "/Player.data";
        if (File.Exists(PlayerPath))
        {
            BinaryFormatter PlayerFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(PlayerPath, FileMode.Open);

            PlayerInfo Settings = PlayerFormatter.Deserialize(fileStream) as PlayerInfo;
            fileStream.Close();
            return Settings;
        }
        else
        {
            //Debug.Log("Audio save not found");
            return null;
        }
    }


    public static void SavePlayerSkin(ShopPurcahseButton NewPlayerSkin)
    {
        BinaryFormatter PlayerFormatter = new BinaryFormatter();
        string PlayerPath = Application.persistentDataPath + "/PlayerSkin.data";
        FileStream fileStream = new FileStream(PlayerPath, FileMode.Create);

        PlayerSkin settings = new PlayerSkin(NewPlayerSkin);


        PlayerFormatter.Serialize(fileStream, settings);
        fileStream.Close();
    }

    public static PlayerSkin LoadPlayerSkin()
    {
        string PlayerPath = Application.persistentDataPath + "/PlayerSkin.data";
        if (File.Exists(PlayerPath))
        {
            BinaryFormatter PlayerFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(PlayerPath, FileMode.Open);

            PlayerSkin Settings = PlayerFormatter.Deserialize(fileStream) as PlayerSkin;
            fileStream.Close();
            return Settings;
        }
        else
        {
            //Debug.Log("Audio save not found");
            return null;
        }
    }


    public static void SaveToken(CurrencyManager TokenMan)
    {
        BinaryFormatter PlayerFormatter = new BinaryFormatter();
        string PlayerPath = Application.persistentDataPath + "/Token.data";
        FileStream fileStream = new FileStream(PlayerPath, FileMode.Create);

        TokenSave settings = new TokenSave(TokenMan);


        PlayerFormatter.Serialize(fileStream, settings);
        fileStream.Close();
    }

    public static TokenSave LoadToken()
    {
        string PlayerPath = Application.persistentDataPath + "/Token.data";
        if (File.Exists(PlayerPath))
        {
            BinaryFormatter PlayerFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(PlayerPath, FileMode.Open);

            TokenSave Settings = PlayerFormatter.Deserialize(fileStream) as TokenSave;
            fileStream.Close();
            return Settings;
        }
        else
        {
           // Debug.Log("Audio save not found");
            return null;
        }
    }

    public static void SaveTime(DailyLoginManager DailyLogin)
    {
        BinaryFormatter PlayerFormatter = new BinaryFormatter();
        string PlayerPath = Application.persistentDataPath + "/Time.data";
        FileStream fileStream = new FileStream(PlayerPath, FileMode.Create);

        GameTimeInfo settings = new GameTimeInfo(DailyLogin);


        PlayerFormatter.Serialize(fileStream, settings);
        fileStream.Close();
    }

    public static GameTimeInfo LoadTime()
    {
        string PlayerPath = Application.persistentDataPath + "/Time.data";
        if (File.Exists(PlayerPath))
        {
            BinaryFormatter PlayerFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(PlayerPath, FileMode.Open);

            GameTimeInfo Settings = PlayerFormatter.Deserialize(fileStream) as GameTimeInfo;
            fileStream.Close();
            return Settings;
        }
        else
        {
            //Debug.Log("Audio save not found");
            return null;
        }
    }


}
