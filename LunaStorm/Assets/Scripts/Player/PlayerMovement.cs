using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Camera mainCamera;

    private Rigidbody rb;
    private CameraMovement cameraMovement; // Reference to the CameraMovement script

    private float minX, maxX, minZ, maxZ;
    private float backwardOffset = 20.0f;
    private float forwardOffset = 20.0f;
    private float leftOffset = 40.0f;
    private float rightOffset = 40.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get the CameraMovement component attached to the camera
        cameraMovement = mainCamera.GetComponent<CameraMovement>();

        CalculateBounds();
    }

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        // Clamp player's position
        Vector3 clampedPosition = transform.position;
        if (cameraMovement == null || !cameraMovement.enabled)
        {
            CalculateBounds();

            // If CameraMovement script is disabled, clamp Z-axis
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
            // If CameraMovement script is disabled, clamp X-axis
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        }
        else
        {
            // If CameraMovement script is enabled, clamp X-axis
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        }
        transform.position = clampedPosition;
    }

    // Calculate bounds of the camera view
    void CalculateBounds()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Get the orthographic size of the camera (half of the vertical size of the view)
        float vertExtent = mainCamera.orthographicSize;

        // Calculate the horizontal size of the view based on the orthographic size and screen aspect ratio
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculate the min and max X positions, considering the left and right offsets
        minX = mainCamera.transform.position.x - horzExtent - leftOffset;
        maxX = mainCamera.transform.position.x + horzExtent + rightOffset;

        // Calculate the min and max Z positions, considering the backward and forward offsets
        minZ = mainCamera.transform.position.z - vertExtent - backwardOffset;
        maxZ = mainCamera.transform.position.z + vertExtent + forwardOffset;
    }
}
