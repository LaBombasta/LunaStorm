using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Teleport : MonoBehaviour
{
    // Coordinates to teleport to
    public Vector3 teleportLocation = new Vector3(0f, 4.19f, -11.84f);

    // This method gets called when the collider collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the player character
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("i hit it.");
            // Teleport the player character to the desired position
            collision.transform.position = teleportLocation;
        }
    }
}
