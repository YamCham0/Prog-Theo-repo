using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    public static MenuSoundManager Instance;

    // Encapsulation: Access to the instance is controlled through a property.
    public static MenuSoundManager SharedInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<MenuSoundManager>();
                if (Instance == null)
                {
                    GameObject menuManager = new GameObject("MenuSoundManager");
                    Instance = menuManager.AddComponent<MenuSoundManager>();
                    DontDestroyOnLoad(menuManager);
                }
            }
            return Instance;
        }
    }

    // Singleton pattern is used here to ensure there is only one instance of the MenuSoundManager in the game.
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