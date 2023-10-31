using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] public TMP_InputField PlayerNameInput;

    public void StartGameCarType1()
    {
        GameStateManager.Instance.selectedCarName = "Totoya";
        GameStateManager.Instance.SetState(GameState.Main);
    }
    public void StartGameCarType2()
    {
        GameStateManager.Instance.selectedCarName = "Subuwu";
        GameStateManager.Instance.SetState(GameState.Main);
    }

    public void SelectionScreen()
    {
        SetPlayerName();
        GameStateManager.Instance.SetState(GameState.Selection);
        
    }

    public void Tutorial()
    {
        GameStateManager.Instance.SetState(GameState.Tutorial);
    }

    public void GoBack()
    {
        // SceneManager.LoadScene("TitleScene");
        GameStateManager.Instance.isGameOver = false;
        GameStateManager.Instance.SetState(GameState.Title);
    }

    public void SetPlayerName()
    {
        PlayerDataHandler.Instance.PlayerName = PlayerNameInput.text;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

