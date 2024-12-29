using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform centerPoint; // The center point of the circle
    public float radius = 5f;     // Radius of the circle
    public float speed = 2f;      // Speed of movement along the circle
    public float verticalSpeed = 2f; // Speed of vertical movement

    private float angle;          // Angle around the circle (in radians)

    void Update()
    {
        // Calculate the current angle based on speed
        angle += speed * Time.deltaTime;

        // Calculate the new x and z positions using trigonometry
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Set the new position, keeping the y the same (assuming a flat circle)
        Vector3 newPosition = new Vector3(x + centerPoint.position.x, transform.position.y, z + centerPoint.position.z);

        // Check for vertical movement input
        if (Input.GetKey(KeyCode.I))
        {
            newPosition.y += verticalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            newPosition.y -= verticalSpeed * Time.deltaTime;
        }

        // Apply the new position
        transform.position = newPosition;

        // Optionally rotate the player to face the direction of movement
        Vector3 direction = new Vector3(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
        transform.forward = direction;
    }
}
