using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{

    [SerializeField] private float speed = 30;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.isGameOver == false)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
    }
}
