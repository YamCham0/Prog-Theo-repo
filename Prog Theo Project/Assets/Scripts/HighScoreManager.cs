using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI BestPlayerName;
    public static int BestScore;
    public static string BestPlayer;

    private void Awake()
    {
        LoadGameRank();
    }

    public static void SaveGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        SaveData data = new SaveData();
        data.TheBestPlayer = BestPlayer;
        data.HighiestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(path, json);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not save game rank: " + e.Message);
        }
    }

    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                BestPlayer = data.TheBestPlayer;
                BestScore = data.HighiestScore;
                SetBestPlayer();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not load game rank: " + e.Message);
        }
    }

    private void SetBestPlayer()
    {
        if (BestPlayer == null && BestScore == 0)
        {
            BestPlayerName.text = "";
        }
        else
        {
            BestPlayerName.text = $"Best Score - {BestPlayer}: {BestScore}";
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    }
}
