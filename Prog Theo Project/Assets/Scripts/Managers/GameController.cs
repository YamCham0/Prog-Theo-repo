using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnSelectedCar();
    }

    // Encapsulates the car spawning logic.
    private void SpawnSelectedCar()
    {
        string selectedCarName = GameStateManager.Instance.selectedCarName;
        GameObject carPrefab = LoadCarPrefab(selectedCarName);
        
        if (carPrefab != null)
        {
            // Instantiate the car at the desired position.
            Instantiate(carPrefab, new Vector3(0, 0.4f, -8), Quaternion.identity);
        }
    }

    // Abstracts the details of how car prefabs are loaded.
    private GameObject LoadCarPrefab(string carName)
    {
        GameObject carPrefab = Resources.Load<GameObject>("Prefabs/" + carName);
        
        // Warn if the car prefab could not be loaded, this will only show in the editor.
        if (carPrefab == null)
        {
            // Debug.LogWarning("No car found with the name: " + carName);
        }

        return carPrefab;
    }
}
