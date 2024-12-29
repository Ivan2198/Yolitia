using UnityEngine;

public class PlatformAttachment : MonoBehaviour
{
    private Transform originalParent; // Store the player's original parent

    // This method is called when the player collides with the platform
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Store the player's original parent transform
            originalParent = collision.transform.parent;

            // Set the player’s parent to the platform (this object)
            collision.transform.SetParent(transform);
        }
    }

    // This method is called when the player stops colliding with the platform
    private void OnCollisionExit(Collision collision)
    {
        // Check if the player is leaving the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restore the player's original parent transform
            collision.transform.SetParent(originalParent);
        }
    }
}
