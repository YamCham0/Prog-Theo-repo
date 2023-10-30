using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatTrack : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z / 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Infinte Track scrolling
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
