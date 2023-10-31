using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType2 : CarBase
{
    void Start()
    {
        base.Start();
        // Initialize the properties for CarType1
        carName = "Subuwu";
    }

    void Update()
    {
        // Call custom methods to check for inputs
        Move();
        Jump();
    }

  
    

}
