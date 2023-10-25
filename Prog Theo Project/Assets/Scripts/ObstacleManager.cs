using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private float spawnRate = 1.0f;

    void Start()
    {
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
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, Quaternion.identity);
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
        return new Vector3(lanePositions[Random.Range(0, lanePositions.Length)], 0.4f, 5);
    }
}
