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
        // ClearHighScore();
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


    public void UpdateHighScore(int newScore, string newPlayer)
    {
        if (newScore > HighScoreManager.BestScore)
        {
            HighScoreManager.BestScore = newScore;
            HighScoreManager.BestPlayer = newPlayer;
            HighScoreManager.SaveGameRank();

            SetBestPlayer();
        }
    }

    private void SetBestPlayer()
    {
        if (BestPlayerName == null)
        {
            Debug.LogError("BestPlayerName is not initialized.");
            return;
        }

        if (BestPlayer == null && BestScore == 0)
        {
            BestPlayerName.text = "";
        }
        else
        {
            BestPlayerName.text = $"HighScore - {BestPlayer}: {BestScore}";
        }
    }


    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    }

    // public void ClearHighScore()
    // {
    //     string path = Application.persistentDataPath + "/savefile.json";

    //     if (File.Exists(path))
    //     {
    //         try
    //         {
    //             File.Delete(path);
    //             Debug.Log("High score data deleted.");
    //         }
    //         catch (System.Exception e)
    //         {
    //             Debug.LogError("Could not delete high score data: " + e.Message);
    //         }
    //     }

    //     // Reset the current static high score values
    //     BestScore = 0;
    //     BestPlayer = "";

    //     // Update the BestPlayerName label to reflect the cleared high score
    //     SetBestPlayer();
    // }
}
