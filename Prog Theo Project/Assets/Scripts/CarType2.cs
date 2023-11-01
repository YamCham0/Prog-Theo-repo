using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType2 : CarBase
{
    private int collisionCount = 0;
    new void Start()
    {
        Debug.Log("CarType2 Start method called.");
        base.Start();
        collisionCount = 0;
        carName = "Subuwu";
    }

    public override void Move()
    {
        // Prevent moving while in the air
        if (isJumping)
        {
            return;
        }

        base.Move();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CarType2 OnCollisionEnter called");

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collisionCount++;

            PlayCrashSound(); // Play crash sound for all collisions

            if (collisionCount == 1)
            {
                StartCoroutine(FlashCar()); // Flash on the first collision
            }

            Debug.Log("Collision with Obstacle detected. Current collision count: " + collisionCount);

            if (collisionCount >= 2)
            {
                Debug.Log("Collision count >= 2. Triggering GameOver.");
                TriggerGameOverEffects(); // Play the particle effect on game over
                TriggerGameOver();  // Trigger Game Over
            }
        }
        else
        {
            Debug.Log("Collision with non-Obstacle detected. Calling base OnCollisionEnter.");
            base.OnCollisionEnter(collision);  // Handles other collisions like landing
        }
    }

    void Update()
    {
        // Call custom methods to check for inputs
        Move();
        Jump();
    }




}
