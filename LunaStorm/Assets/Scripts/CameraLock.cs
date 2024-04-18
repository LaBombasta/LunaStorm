using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    // Reference to the CameraMovement script
    public CameraMovement cameraMovementScript;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger has been entered by the player
        if (other.CompareTag("Player"))
        {
            // Disable the CameraMovement script
            if (cameraMovementScript != null)
            {
                cameraMovementScript.enabled = false;
            }
        }
    }
}
