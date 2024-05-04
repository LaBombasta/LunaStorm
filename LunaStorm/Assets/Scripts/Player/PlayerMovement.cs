using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    private CameraMovement cameraMovement; // Reference to the CameraMovement script
    private CharacterController controller;

    private Health health;

    public float forwardSpeed = 7f;
    public float barrelRollSpeedMultiplier = 2.2f; // Multiplier for strafing speed

    private float minX, maxX, minZ, maxZ;
    private float backwardOffset = 16.0f;
    private float forwardOffset = 16.0f;
    private float leftOffset = 30.0f;
    private float rightOffset = 30.0f;
    public float smoothingFactor = 5f; // Adjust the smoothing factor for camera transition

    public float rollSpeed = 1000f;
    private bool canRoll = true;

    //change speed multiplier to take effect only during barrel roll

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Get the CameraMovement component attached to the camera
        cameraMovement = mainCamera.GetComponent<CameraMovement>();

        health = FindObjectOfType<Health>();

        CalculateBounds();
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Adjusting speed based on direction
        float adjustedSpeed = forwardSpeed;
        // Apply barrel roll speed multiplier if rolling
        if (!canRoll)
        {
            adjustedSpeed *= barrelRollSpeedMultiplier;
        }
        if(GameManager.instance.LockedInBattle())
        {
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            controller.Move(moveDirection * adjustedSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 moveDirection = new Vector3(horizontalInput, 0f,1).normalized;
            moveDirection.z /=3;
            controller.Move(moveDirection * adjustedSpeed * Time.deltaTime);
        }
        
        
        // Barrel Roll
        if (canRoll && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            health.enabled = false;

            // Determine the direction of the barrel roll based on horizontal input
            int rollDirection = horizontalInput < 0 ? 1 : -1; // If moving left, roll right; if moving right, roll left

            StartCoroutine(BarrelRoll(rollDirection));

            health.enabled = true;
        }

        //******************************** add lerp when the camera movement becomes enabled again so that it is a smooth transition back to the player************************
        // Clamp player's position
        Vector3 clampedPosition = transform.position;
        CalculateBounds();
        if (cameraMovement == null || !cameraMovement.enabled)
        {
            // If CameraMovement script is disabled, clamp X and Z axes
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        }
        else
        {
            // If CameraMovement script is enabled, clamp X-axis only
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);

            // Clamp backward position (negative Z-axis)
            if (clampedPosition.z < 0f)
            {
                clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, 0f);
            }
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

    IEnumerator BarrelRoll(int rollDirection)
    {
        canRoll = false; // Disable rolling until the cooldown is over

        float rollAmount = 360f; // Adjust the amount of rotation for the barrel roll
        float rollAngle = 0f;

        while (rollAngle < rollAmount)
        {
            float deltaAngle = rollSpeed * Time.deltaTime * rollDirection;
            transform.Rotate(Vector3.forward, deltaAngle);
            rollAngle += Mathf.Abs(deltaAngle);
            yield return null;
        }

        // Explicitly set rotation to ensure it ends at 0
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

        // Cooldown
        yield return new WaitForSeconds(0.0f); // 1 second cooldown
        canRoll = true; // Enable rolling again
    }
}
