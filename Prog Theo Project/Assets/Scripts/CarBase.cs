using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstraction: Abstract Car class that serves as the base for all car types
public abstract class CarBase : MonoBehaviour
{
    public string carName;
    public int hitPoints;
    public float mobility;
    public float laneSwitchSpeed = 2.0f;
    private int currentLaneIndex = 1;
    [SerializeField] protected float jumpForce = 5f;
    private bool isJumping = false;

    // Abstract function for moving the car, to be implemented by derived classes
    public abstract void Move();

    public virtual void Jump()
    {
        if (!isJumping)  // No double jumps
        {
            // Perform the jump
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Set the flag
            isJumping = true;
        }
    }

    // OnCollisionEnter to detect landing
    void OnCollisionEnter(Collision collision)
    {
        // Check if the car landed back on the track
        if (collision.gameObject.CompareTag("Track"))
        {
            isJumping = false;
        }
    }

    // Encapsulation: Public method to deal damage to the car, hiding internal variables
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            // TODO: Implement logic for when the car is destroyed
            Debug.Log("Car is destroyed!");
        }
    }

    // Function to switch lanes left or right
    // Initialize to 1 for the middle lane
      

    public virtual void SwitchLane(bool moveRight)
    {
        Vector3 targetPosition = transform.position;
    
        // Define lane positions
        float[] lanePositions = {-1.3f, 0f, 1.3f};

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
}
