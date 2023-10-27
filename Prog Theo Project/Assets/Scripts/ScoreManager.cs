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

    void Update()
    {
        if (GameStateManager.Instance.isGameOver == false)
        {
            distanceTraveled += Time.deltaTime; // Assuming 1 unit of distance per second
            Debug.Log("distanceTraveled" + distanceTraveled);
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        int score = Mathf.FloorToInt(distanceTraveled * scoreMultiplier);
        PlayerDataHandler.Instance.PlayerScore = score;
        scoreText.text = "Score: " + score;
        Debug.Log("Score: " + score);
    }
}