using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType1 : CarBase
{
     void Start()
    {

    }

    void Update()
    {
        // Call custom methods to check for inputs
        Move();
        Jump();
    }

  
    

}
