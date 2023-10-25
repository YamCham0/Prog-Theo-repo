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
        distanceTraveled += Time.deltaTime; // Assuming 1 unit of distance per second
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        int score = Mathf.FloorToInt(distanceTraveled * scoreMultiplier);
        scoreText.text = "Score: " + score;
    }
}