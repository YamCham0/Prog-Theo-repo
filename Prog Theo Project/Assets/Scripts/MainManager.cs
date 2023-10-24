using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainManager : MonoBehaviour
{

    public Text ScoreText;

    //Fields for display the player info
    public Text CurrentPlayerName;
    public Text BestPlayerNameAndScore;

    public GameObject GameOverText;

    private void Awake()
    {
        LoadGameRank();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    // private void CheckBestPlayer()
    // {
    //     int CurrentScore = PlayerDataHandler.Instance.Score;

    //     if (CurrentScore > BestScore)
    //     {
    //         BestPlayer = PlayerDataHandler.Instance.PlayerName;
    //         BestScore = CurrentScore;

    //         BestPlayerNameAndScore.text = $"Best Score - {BestPlayer}: {BestScore}";

    //         SaveGameRank(BestPlayer, BestScore);
    //     }
    // }

        // private void SetBestPlayer()
        // {
        //     if (BestPlayer == null && BestScore == 0)
        //     {
        //         BestPlayerNameAndScore.text = "";
        //     }
        //     else
        //     {
        //         BestPlayerNameAndScore.text = $"Best Score - {BestPlayer}: {BestScore}";
        //     }

        // }

        public void SaveGameRank(string bestPlaterName, int bestPlayerScore)
        {
            SaveData data = new SaveData();

            data.TheBestPlayer = bestPlaterName;
            data.HighiestScore = bestPlayerScore;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        public void LoadGameRank()
        {

        }

        [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    }
}
