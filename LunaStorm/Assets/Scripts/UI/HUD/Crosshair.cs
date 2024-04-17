using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Adjust this value to set the distance of the crosshair from the camera
    private float distanceFromCamera = 50f;

    void Update()
    {
        // Get the position of the mouse cursor in screen coordinates
        Vector3 mousePosition = Input.mousePosition;
        // Set the distance of the mouse cursor from the camera
        mousePosition.z = distanceFromCamera;

        // Convert the screen position of the mouse cursor to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Update the position of the crosshair to follow the mouse cursor
        transform.position = worldPosition;
    }
}
