using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public Transform pointA; // First point (lower position)
    public Transform pointB; // Second point (upper position)
    public float speed = 2f; // Speed of the movement

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {

        pointA.SetParent(null);
        pointB.SetParent(null);

        // Initialize positions
        if (pointA != null && pointB != null)
        {
            startPosition = pointA.position;
            endPosition = pointB.position;
        }
        else
        {
            Debug.LogError("Please assign both pointA and pointB in the inspector.");
        }
    }

    void Update()
    {
        // Check if pointA and pointB are assigned
        if (pointA != null && pointB != null)
        {
            // Move the platform between pointA and pointB using PingPong
            float time = Mathf.PingPong(Time.time * speed, 1f);
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
        }
    }
}

