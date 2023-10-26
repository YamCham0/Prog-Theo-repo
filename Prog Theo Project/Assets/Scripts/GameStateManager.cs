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
        // Do not transition if the state is GameOver
        if (newState != GameState.GameOver)
        {
            // Load the LoadingScene first
            SceneManager.LoadScene("LoadingScene");

            // Wait for a second (or however long you want the transition to be)
            yield return new WaitForSeconds(1);
        }

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
                // Handle GameOver state here without switching scene.
                // Could invoke a UnityEvent or set a flag that other scripts can listen to.
                // OnGameOver.Invoke();
                break;
        }

        CurrentState = newState;
    }



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            Transform root = GameObject.FindGameObjectWithTag("CanvasRoot").transform; // Assuming you've tagged your Canvas root
            Transform textTransform = root.Find("GameOverText"); // Direct child
            if (textTransform != null)
            {
                gameOverText = textTransform.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("GameOverText not found");
            }
        }
    }


    public void GameOver(int finalScore, string player)
    {
        isGameOver = true;

        // Update the high score
        highScoreManager.UpdateHighScore(finalScore, player);

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverText is not set");
        }

        SetState(GameState.GameOver);
    }

}
