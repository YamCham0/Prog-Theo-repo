using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private float spawnRate = 2.0f;
    private int lastThresholdCrossed = 0;
    private bool thresholdCrossed = false;

    void Start()
    {
        ScoreManager.OnScoreUpdated += UpdateSpawnRate; //subscribe to event
        // Custom method to start spawning
        StartSpawning();
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnObstacle", 0, spawnRate);
    }

    // Custom method to spawn obstacle
    public void SpawnObstacle()
    {
        int obstacleIndex = GetRandomObstacleIndex();
        Vector3 spawnPosition = GetRandomLanePosition();
        if (GameStateManager.Instance.isGameOver == false)
        {
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, Quaternion.identity);
        }
    }

    // Custom method to get random obstacle index
    public int GetRandomObstacleIndex()
    {
        return Random.Range(0, obstaclePrefabs.Length);
    }

    // Custom method to get random lane position
    public Vector3 GetRandomLanePosition()
    {
        float[] lanePositions = { -1.3f, 0f, 1.3f };
        return new Vector3(lanePositions[Random.Range(0, lanePositions.Length)], 0.4f, 25);
    }

    public void UpdateSpawnRate(int currentScore)
{
    int nextThreshold = lastThresholdCrossed + 200;

    if (currentScore >= nextThreshold && !thresholdCrossed) // Add the !thresholdCrossed check here
    {
        lastThresholdCrossed = nextThreshold;  // Update lastThresholdCrossed with nextThreshold
        spawnRate = Mathf.Max(0.2f, spawnRate - 0.2f);  // Decrease spawnRate by 0.2 to a minimum of 0.2f

        CancelInvoke("SpawnObstacle");
        InvokeRepeating("SpawnObstacle", 0, spawnRate);  // Reset with the new spawn rate
        thresholdCrossed = true; // Set the flag
    }
    else if (currentScore < nextThreshold)
    {
        thresholdCrossed = false; // Reset the flag
    }
}


    void OnDestroy()
    {
        ScoreManager.OnScoreUpdated -= UpdateSpawnRate;  // Unsubscribe from event
    }
}
