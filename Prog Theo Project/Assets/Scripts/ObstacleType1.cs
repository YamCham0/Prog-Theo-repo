using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleType1 : ObstacleBase
{
    // Inheritance: ObstacleType1 inherits from ObstacleBase
    // Polymorphism: Speed is being customized for this particular Obstacle Type
    void Start()
    {
        speed = 40; // This accesses the speed variable from the base class and sets a new value
    }
}