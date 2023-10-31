using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    public static MenuSoundManager Instance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
