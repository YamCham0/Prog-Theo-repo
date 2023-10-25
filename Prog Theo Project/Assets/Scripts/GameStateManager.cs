using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        // Handle each game state
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
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
    }
}
