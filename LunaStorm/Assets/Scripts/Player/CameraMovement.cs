using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float verticalOffset = 10;

    void LateUpdate()
    {
        if (target != null)
        {
            // Follow the player vertically
            Vector3 newPosition = transform.position;
            newPosition.z = target.position.z + verticalOffset;
            transform.position = newPosition;
        }
    }
}

//Lock backward motion