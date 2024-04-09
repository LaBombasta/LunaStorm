using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    public Transform cameraTransform; // Reference to the camera's transform
    public float rectHeight = 100f; // Height of the follow rectangle
    public float cameraMoveSpeed = 1.25f; // Camera move speed

    private Vector3 lastPosition; // Store the player's last position

    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        float verticalInput = Input.GetAxis("Vertical");

        // Clamp vertical movement
        verticalInput = Mathf.Clamp(verticalInput, -100f, 100f);

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(0f, 0f, verticalInput).normalized;

        // Move the player
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Check if the player moved forward
        if (Vector3.Dot(transform.position - lastPosition, transform.forward) > 0)
        {
            // If the player moved forward, move the camera with the player
            cameraTransform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        // Check if the player leaves the top of the rectangle
        if (cameraTransform.position.y > rectHeight)
        {
            // Move the camera forward
            cameraTransform.position += Vector3.forward * cameraMoveSpeed * Time.deltaTime;
        }

        // Update the last position of the player
        lastPosition = transform.position;
    }
}
