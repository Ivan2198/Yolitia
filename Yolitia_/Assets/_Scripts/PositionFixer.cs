using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFixer : MonoBehaviour
{
    public Transform centerObject;     // The central object (cylinder) around which the object will be placed
    [Range(1f, 10f)]
    public float radius = 5f;          // Radius of the circle path
    public int divisions = 12;         // Number of positions to divide the circumference (like clock hours)

    // New variables for selecting the specific division and height
    [Range(1, 12)]
    public int selectedDivision = 4;   // The division to place the object (1-12, representing hours on a clock)
    [Range(0f, 30f)]
    public float selectedHeight = 6f;   // The Y position (height) for the object

    private void Start()
    {
        PlaceObjectAtSelectedPosition();
    }

    void PlaceObjectAtSelectedPosition()
    {
        if (!centerObject)
        {
            Debug.LogError("Center object is not assigned!");
            return;
        }

        if (selectedDivision < 1 || selectedDivision > divisions)
        {
            Debug.LogError("Selected division is out of range!");
            return;
        }

        // Calculate the angle for the selected division
        float angle = (selectedDivision - 1) * (360f / divisions) * Mathf.Deg2Rad; // Convert to radians

        // Calculate the x and z positions on the circle at the given radius
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Set the Y position (height) to the specified value
        float y = selectedHeight;

        // Calculate the final position for the object
        Vector3 position = new Vector3(x + centerObject.position.x, y, z + centerObject.position.z);

        // Set the object's position and rotate to face center
        transform.position = position;
        RotateObjectTowardsCenter();
    }

    void RotateObjectTowardsCenter()
    {
        Vector3 directionToCenter = centerObject.position - transform.position;
        float yRotation = Mathf.Atan2(directionToCenter.x, directionToCenter.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // Draw Gizmos to visualize positions
    private void OnDrawGizmos()
    {
        if (centerObject)
        {
            // Draw the center object
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(centerObject.position, 0.2f);

            // Draw the circular path and positions
            Gizmos.color = Color.green;
            for (int i = 0; i < divisions; i++)
            {
                float angle = i * (360f / divisions) * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                Vector3 position = new Vector3(x + centerObject.position.x, selectedHeight, z + centerObject.position.z);
                Gizmos.DrawSphere(position, 0.1f); // Draw small spheres at each division
            }

            // Highlight the selected position
            if (selectedDivision >= 1 && selectedDivision <= divisions)
            {
                float selectedAngle = (selectedDivision - 1) * (360f / divisions) * Mathf.Deg2Rad;
                float x = Mathf.Cos(selectedAngle) * radius;
                float z = Mathf.Sin(selectedAngle) * radius;
                Vector3 selectedPosition = new Vector3(x + centerObject.position.x, selectedHeight, z + centerObject.position.z);
                Gizmos.color = Color.yellow; // Highlight color for selected position
                Gizmos.DrawSphere(selectedPosition, 0.15f); // Draw a larger sphere for the selected position
            }
        }
    }

    // Button to reposition in the inspector
    [ContextMenu("Reposition Object")]
    private void RepositionObject()
    {
        PlaceObjectAtSelectedPosition();
    }
}