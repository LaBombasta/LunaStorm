using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float verticalOffset = 10;
    public float smoothSpeed = 1; // Adjust the smoothness of the movement

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target Z position
            float targetZ = target.position.z + verticalOffset;

            // Follow the player vertically with interpolation (lerp), but prevent backwards motion
            if (targetZ > transform.position.z)
            {
                Vector3 newPosition = transform.position;
                newPosition.z = Mathf.Lerp(transform.position.z, targetZ, smoothSpeed * Time.deltaTime);
                transform.position = newPosition;
            }
        }
    }
}
