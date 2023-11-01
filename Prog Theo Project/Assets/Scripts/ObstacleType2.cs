using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleType2 : ObstacleBase
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 30;
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
