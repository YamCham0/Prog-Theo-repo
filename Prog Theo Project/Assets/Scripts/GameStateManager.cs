using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public enum GameState
{
    Title,
    Tutorial,
    Main,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public GameState CurrentState;
    public UnityEvent OnGameOver;
    public bool isGameOver = false;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private HighScoreManager highScoreManager;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            // Subscribe from sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }

        highScoreManager = GetComponent<HighScoreManager>();
    }

    private void OnDestroy()
    {
        // Unsubscribe from sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void SetState(GameState newState)
    {
        StartCoroutine(LoadSceneWithTransition(newState));
    }


    IEnumerator LoadSceneWithTransition(GameState newState)
    {
        // Load the LoadingScene first
        SceneManager.LoadScene("LoadingScene");

        // Wait for a second (or however long you want the transition to be)
        yield return new WaitForSeconds(1);

        // Load the actual scene
        switch (newState)
        {
            case GameState.Title:
                SceneManager.LoadScene("TitleScene");
                break;
            case GameState.Tutorial:
                SceneManager.LoadScene("TutorialScene");
                break;
            case GameState.Main:
                SceneManager.LoadScene("MainScene");
                break;
            case GameState.GameOver:
                OnGameOver.Invoke();
                break;
        }

        CurrentState = newState;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            gameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        }
    }


public void UpdateHighScore(int newScore, string newPlayer)
{
    if (newScore > HighScoreManager.BestScore)
    {
        HighScoreManager.BestScore = newScore;
        HighScoreManager.BestPlayer = newPlayer;
        HighScoreManager.SaveGameRank();
    }
}




    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        SetState(GameState.GameOver);
    }
}
