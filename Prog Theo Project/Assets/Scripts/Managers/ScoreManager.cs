using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the Score Text UI (TextMeshPro)
    private float distanceTraveled; // To store the distance traveled
    public float scoreMultiplier = 1.0f; // Multiplier for score calculations
    public static event Action<int> OnScoreUpdated;  // Declare the event

    void Update()
    {
        if (GameStateManager.Instance.isGameOver == false)
        {
            distanceTraveled = TrackMoveBack.totalDistanceMoved;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        int score = Mathf.FloorToInt(distanceTraveled * scoreMultiplier);
        PlayerDataHandler.Instance.PlayerScore = score;
        scoreText.text = "Score: " + score;

        OnScoreUpdated?.Invoke(score);  // Trigger the event
    }
}