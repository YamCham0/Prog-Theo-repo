using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType1 : CarBase
{
    void Start()
    {
        // Initialize the properties for CarType1
        carName = "Totoya";
    }

    void Update()
    {
        // Call custom methods to check for inputs
        Move();
        Jump();
    }

  
    

}
