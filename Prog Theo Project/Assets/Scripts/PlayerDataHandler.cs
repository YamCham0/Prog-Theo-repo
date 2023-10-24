using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    //Static Class for save the current player data;

    public static PlayerDataHandler Instance;

    public string PlayerName;

    public int Score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}