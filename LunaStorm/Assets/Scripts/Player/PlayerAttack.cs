using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Reference to the bomb prefab
    public GameObject bombPrefab;

    // Array to hold multiple spawn points
    public Transform[] bombSpawnPoints;

    // Position offset for spawning bombs relative to the main weapon's position
    private Vector3 bombSpawnOffset = new Vector3(0f, 0f, 1f);

    // Speed at which the bomb moves forward
    public float bombSpeed = 10f;

    // Interval between consecutive bomb instantiations
    public float fireRate = 0.2f;

    // Timer to control the rate of fire
    private float fireTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Continuously drop bombs while holding down the spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            // Increment the fire timer
            fireTimer += Time.deltaTime;

            // If the fire timer exceeds the fire rate, drop a bomb and reset the timer
            if (fireTimer >= fireRate)
            {
                DropBomb();
                fireTimer = 0f;
            }
        }
    }

    // Method to drop a bomb
    void DropBomb()
    {
        foreach (Transform spawnPoint in bombSpawnPoints)
        {
            // Instantiate a bomb at the current spawn point position with the specified rotation
            GameObject bomb = Instantiate(bombPrefab, spawnPoint.position + bombSpawnOffset, Quaternion.Euler(90f, 0f, 0f));

            // Set bomb speed
            Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
            bombRigidbody.velocity = bomb.transform.forward * bombSpeed;
        }
    }

    // Collision detection for the bomb
    void OnCollisionEnter(Collision collision)
    {
        // Destroy the bomb upon collision with any object
        Destroy(gameObject);
    }
}
