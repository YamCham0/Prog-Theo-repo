using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleBase : MonoBehaviour
{

    [SerializeField] public float speed = 30;

    // Custom method to move the obstacle
    public virtual void MoveObstacle()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

    // Custom method to destroy off-screen obstacles
    public void DestroyIfOffScreen()
    {
        if (transform.position.z < -10.1f)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        MoveObstacle();
        DestroyIfOffScreen();
    }
}
