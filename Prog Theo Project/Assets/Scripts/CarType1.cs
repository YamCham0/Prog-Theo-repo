using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType1 : CarBase
{
    void Start()
    {
        // Initialize the properties for CarType1
        carName = "CarType1";
        hitPoints = 20;
        mobility = 4.0f;
    }

    void Update()
    {
        // Call custom methods to check for inputs
        CheckForLaneSwitch();
        CheckForJump();
    }

    // Custom method to check for lane-switching input
    private void CheckForLaneSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchLane(false);  // Move left
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchLane(true);  // Move right
        }
    }

    // Custom method to check for jump input
    private void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();  // Call the Jump method (from CarBase)
        }
    }

    // Implementing the abstract Move method from CarBase
    public override void Move()
    {
        // Your implementation here
    }

    public override void HandleObstacleCollision(GameObject obstacle)
{
    // CarType1-specific collision handling
    Debug.Log("CarType1 collided with obstacle: " + obstacle.name);
    TakeDamage(5);  // Example: take only 5 points of damage
}

    // Implementing the abstract SwitchLane method from CarBase
    public override void SwitchLane(bool moveRight)
    {
        base.SwitchLane(moveRight); // Call parent class's SwitchLane method
    }
}
