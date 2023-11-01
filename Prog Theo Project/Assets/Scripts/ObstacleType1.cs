using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleType1 : ObstacleBase
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 40;
    }

    public override void MoveObstacle(){
        base.MoveObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();
        DestroyIfOffScreen();
    }

    
}
