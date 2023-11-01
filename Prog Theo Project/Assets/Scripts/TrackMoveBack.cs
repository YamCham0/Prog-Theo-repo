using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMoveBack : MonoBehaviour
{

    [SerializeField] private float speed = 30;
    public static float totalDistanceMoved = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.isGameOver == false)
        {
            float distanceThisFrame = Time.deltaTime * speed;
            transform.Translate(Vector3.back * distanceThisFrame);
            totalDistanceMoved += distanceThisFrame;
        }
    }
}
