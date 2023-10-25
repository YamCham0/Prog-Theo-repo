using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleType1 : ObstacleBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle(speed);
        DestroyIfOffScreen();
    }

    
}
