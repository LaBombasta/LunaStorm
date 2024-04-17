using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Color lineColor = Color.green; // Default color is green

    void Update()
    {
        // Get the position of the GameObject this script is attached to
        Vector3 startPos = transform.position;

        // Calculate the end position straight down from the start position
        Vector3 endPos = startPos - Vector3.up * 200f; // Adjust 200f to change the length of the line

        // Draw the line from start position to end position
        Debug.DrawLine(startPos, endPos, lineColor);
    }
}
