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
    public TextMeshProUGUI highScoreText;
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
        Debug.Log("GameStateManager Instance: " + GameStateManager.Instance);

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
        Transform canvasRoot = GameObject.FindGameObjectWithTag("CanvasRoot").transform;
        
        // For gameOverText
        Transform gameOverTextTransform = canvasRoot.Find("GameOverText"); 
        if (gameOverTextTransform != null)
        {
            gameOverText = gameOverTextTransform.GetComponent<TextMeshProUGUI>();
        }

        // For highScoreText
        Transform highScoreTextTransform = canvasRoot.Find("HighScoreText");
        if (highScoreTextTransform != null)
        {
            highScoreText = highScoreTextTransform.GetComponent<TextMeshProUGUI>();
        }
    }
}



    public void GameOver(int finalScore, string player)
{
    isGameOver = true;

    // Update the high score
    highScoreManager.UpdateHighScore(finalScore, player);

    if (gameOverText != null && highScoreText != null)
    {
        gameOverText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);

        // Set the text for the highScoreText here
        highScoreText.text = $"HighScore - {HighScoreManager.BestPlayer}: {HighScoreManager.BestScore}";
    }
    else
    {
        if (gameOverText == null)
        {
            Debug.LogError("GameOverText is not set");
        }
        if (highScoreText == null)
        {
            Debug.LogError("HighScoreText is not set");
        }
    }

    SetState(GameState.GameOver);
}



}
