using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Assign these in the Inspector
    [SerializeField] private Texture2D defaultCursor;   // Default cursor image
    [SerializeField] private Texture2D hoverCursor;     // Cursor when hovering over UI or specific objects
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto; // Cursor mode
    [SerializeField] private Vector2 hotSpot = new Vector2(0.5f, 0.5f); // Center of the cursor image

    private void Start()
    {
        // Set the default cursor when the game starts
        SetCursor(defaultCursor);
    }

    // Method to set the cursor
    public void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    // Call this method to change to hover cursor
    public void ChangeToHoverCursor()
    {
        SetCursor(hoverCursor);
    }

    // Call this method to reset to default cursor
    public void ResetCursor()
    {
        SetCursor(defaultCursor);
    }

    private void OnDestroy()
    {
        // Reset cursor to default when the object is destroyed
        ResetCursor();
    }
}
