using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        string selectedCarName = GameStateManager.Instance.selectedCarName;
        
        // Load the car prefab from the Resources folder
        GameObject carPrefab = Resources.Load<GameObject>("Prefabs/" + selectedCarName);

        // Instantiate the car at the desired position
        if (carPrefab != null)
        {
            Instantiate(carPrefab, new Vector3(0, 0.4f, -8), Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No car found with the name: " + selectedCarName);
        }
    }
}
