using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public static GameSoundManager Instance;

    // Encapsulation: Access to the instance is controlled through a property.
    public static GameSoundManager SharedInstance
    {
        get
        {
            if (Instance == null)
            {
                // Lazy initialization (only when needed)
                Instance = FindObjectOfType<GameSoundManager>();
                if (Instance == null)
                {
                    GameObject soundManager = new GameObject("GameSoundManager");
                    Instance = soundManager.AddComponent<GameSoundManager>();
                    DontDestroyOnLoad(soundManager);
                }
            }
            return Instance;
        }
    }

    // The Singleton pattern ensures only one instance of GameSoundManager exists.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
