using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuUIHandler : MonoBehaviour
{
    // Start Game
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
// How to play Scene
    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }

    // Exit Game
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
