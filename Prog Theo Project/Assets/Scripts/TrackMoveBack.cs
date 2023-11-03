using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMoveBack : MonoBehaviour
{
    [SerializeField] private float speed = 30f; // Encapsulation: Speed is adjustable in the Inspector but protected from outside access
    public static float totalDistanceMoved = 0f; // Static variable to track the total distance moved

    private void Update()
    {
        if (!GameStateManager.Instance.isGameOver) // Encapsulation: Access GameStateManager's state via its instance
        {
            MoveTrack();
        }
    }

    private void MoveTrack()
    {
        float distanceThisFrame = Time.deltaTime * speed;
        transform.Translate(Vector3.back * distanceThisFrame);
        totalDistanceMoved += distanceThisFrame;
    }
}