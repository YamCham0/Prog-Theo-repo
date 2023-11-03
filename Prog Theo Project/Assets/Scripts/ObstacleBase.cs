using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleBase : MonoBehaviour
{
    // Encapsulation: Using SerializeField to allow setting in the inspector while keeping the field private
    [SerializeField] protected float speed = 30;

    // Abstraction: A method that defines behavior without specifying the details in the base class
    public virtual void MoveObstacle()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

    // Encapsulation: Keeping this logic within the object that it pertains to
    private void DestroyIfOffScreen()
    {
        if (transform.position.z < -10.1f)
        {
            Destroy(gameObject);
        }
    }

    // Abstraction: Update is handling the abstraction of two different behaviors
    void Update()
    {
        MoveObstacle();
        DestroyIfOffScreen();
    }
}