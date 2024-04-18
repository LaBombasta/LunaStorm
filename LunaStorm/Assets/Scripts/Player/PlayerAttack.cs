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

    // Reference to the bomb prefab
    public GameObject bulletPrefab;

    // Array to hold multiple spawn points
    public Transform[] bulletSpawnPoints;

    // Position offset for spawning bombs relative to the main weapon's position
    private Vector3 bulletSpawnOffset = new Vector3(0f, 0f, 1f);

    // Speed at which the bomb moves forward
    public float bulletSpeed = 10f;

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

        // Fire bullets on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Increment the fire timer
            fireTimer += Time.deltaTime;

            // If the fire timer exceeds the fire rate, fire bullets and reset the timer
            if (fireTimer >= fireRate)
            {
                FireBullet();
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

            // Destroy the bomb after 5 seconds
            Destroy(bomb, 5f);
        }
    }
    
    void FireBullet()
    {
        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            // Instantiate a bullet at the current spawn point position with the specified rotation
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position + bulletSpawnOffset, Quaternion.Euler(90f, 0f, 0f));

            // Calculate the direction the bullet should move (forward from the spawn point)
            Vector3 direction = spawnPoint.forward;

            // Move the bullet in the calculated direction at the specified speed
            bullet.transform.position += direction * bulletSpeed * Time.deltaTime;

            // Destroy the bomb after 5 seconds
            Destroy(bullet, 5f);
        }
    }


    // Collision detection for the bomb
    void OnCollisionEnter(Collision collision)
    {
        // Destroy the bomb upon collision with any object
        Destroy(gameObject);
    }
}
