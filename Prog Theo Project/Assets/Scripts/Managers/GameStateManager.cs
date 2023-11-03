using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Title,
    Tutorial,
    Main,
    Selection,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public GameState CurrentState;
    public UnityEvent OnGameOver;
    public string selectedCarName;
    public bool isGameOver = false;
    [SerializeField] private TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highScoreText;
    public Button backButton;
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
            yield return new WaitForSeconds(2);
        }

        // Load the actual scene
        switch (newState)
        {
            case GameState.Title:
                SceneManager.LoadScene("TitleScene");
                isGameOver = false;
                break;
            case GameState.Tutorial:
                SceneManager.LoadScene("TutorialScene");
                break;
            case GameState.Main:
                SceneManager.LoadScene("MainScene");
                break;
            case GameState.Selection:
                SceneManager.LoadScene("SelectionScene");
                break;
            case GameState.GameOver:
                break;
        }

        CurrentState = newState;
        UpdateSoundtrack();
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
            // For Back button
            Transform backButtonTransform = canvasRoot.Find("BackButton");
            if (backButtonTransform != null)
            {
                backButton = backButtonTransform.GetComponent<Button>();
            }
        }
    }


    private void UpdateSoundtrack()
    {
        GameObject menuSoundManager = GameObject.Find("MenuSoundManager");
        GameObject gameSoundManager = GameObject.Find("GameSoundManager");
        AudioSource menuAudioSource = menuSoundManager.GetComponent<AudioSource>();
        AudioSource gameAudioSource = gameSoundManager.GetComponent<AudioSource>();

        switch (CurrentState)
        {
            case GameState.Title:
            case GameState.Selection:
            case GameState.Tutorial:
                if (menuAudioSource != null && !menuAudioSource.isPlaying)
                {
                    menuAudioSource.Play();
                }
                if (gameAudioSource != null)
                {
                    gameAudioSource.Stop();
                }
                break;

            case GameState.Main:
                if (menuAudioSource != null)
                {
                    menuAudioSource.Stop();
                }
                if (gameAudioSource != null && !gameAudioSource.isPlaying)
                {
                    gameAudioSource.Play();
                }
                break;
        }
    }




    public void GameOver(int finalScore, string player)
    {
        isGameOver = true;

        // Update the high score
        highScoreManager.UpdateHighScore(finalScore, player);

        if (gameOverText != null && highScoreText != null && backButton != null)
        {
            gameOverText.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);


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
