using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleType2 : ObstacleBase
{
    // Inheritance: ObstacleType2 inherits from ObstacleBase
    void Start()
    {
        speed = 30; // This accesses the speed variable from the base class and sets a new value, even though it's the same as the default
    }
}