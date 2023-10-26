using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstraction: Abstract Car class that serves as the base for all car types
public abstract class CarBase : MonoBehaviour
{
    public string carName;
    public float laneSwitchSpeed = 2.0f;
    private int currentLaneIndex = 1;
    [SerializeField] protected float jumpForce = 5f;
    public bool isJumping = false;

    public virtual void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && GameStateManager.Instance.isGameOver == false)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    // OnCollisionEnter to detect landing
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Track"))
        {
            isJumping = false;
            Debug.Log("Landed back on: " + collision.gameObject.name);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision Detected with: " + collision.gameObject.name);
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        int finalScore = PlayerDataHandler.Instance.PlayerScore;
        string player = PlayerDataHandler.Instance.PlayerName;

        Debug.Log("TriggerGameOver - Final Score: " + finalScore + ", Player: " + player);

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.GameOver(finalScore, player);
        }
        else
        {
            Debug.LogError("GameStateManager instance is null");
        }
    }




    // Function to switch lanes left or right
    // Initialize to 1 for the middle lane
    public virtual void SwitchLane(bool moveRight)
    {
        Vector3 targetPosition = transform.position;

        // Define lane positions
        float[] lanePositions = { -1.3f, 0f, 1.3f };

        // Move to adjacent lane if possible
        if (moveRight && currentLaneIndex < lanePositions.Length - 1)
        {
            currentLaneIndex++;
        }
        else if (!moveRight && currentLaneIndex > 0)
        {
            currentLaneIndex--;
        }

        // Set the new x position
        targetPosition.x = lanePositions[currentLaneIndex];
        transform.position = targetPosition;
    }

    public virtual void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameStateManager.Instance.isGameOver == false)
        {
            SwitchLane(false);  // Move left
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && GameStateManager.Instance.isGameOver == false)
        {
            SwitchLane(true);  // Move right
        }
    }
}
