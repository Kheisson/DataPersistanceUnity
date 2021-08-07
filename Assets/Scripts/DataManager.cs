using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    private static string PlayerName;
    private static int BestScore;
    private static string BestScorePlayerName;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string GetPlayerName()
    {
        return PlayerName;
    }

    public void SetPlayerName(string playerName)
    {
        PlayerName = playerName;
    }

    public string GetBestScorePlayerName()
    {
        return BestScorePlayerName;
    }

    public void SetBestScorePlayerName(string playerName)
    {
        BestScorePlayerName = playerName;
    }

    public int GetBestScore()
    {
        return BestScore;
    }

    public void SetBestScore(int newScore)
    {
        if(newScore > BestScore)
        {
            BestScore = newScore;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public int BestScore;
    }

    public void SaveScore()
    {
        SaveData save = new SaveData();
        save.PlayerName = PlayerName;
        save.BestScore = BestScore;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/unitydata/data.json", json);
    }

    public void LoadScore()
    {
        string path = File.ReadAllText(Application.persistentDataPath + "/unitydata/data.json");
        SaveData load = JsonUtility.FromJson<SaveData>(path);
        BestScorePlayerName = load.PlayerName;
        BestScore = load.BestScore;
    }
}
