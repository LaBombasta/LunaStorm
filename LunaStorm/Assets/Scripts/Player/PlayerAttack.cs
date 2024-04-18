using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Reference to the bullet prefab
    public GameObject bulletPrefab;

    // Position offset for spawning bullets relative to the main weapon's position
    public Vector3 bulletSpawnOffset = new Vector3(0f, 0f, 1f);

    // Speed at which the bullet moves forward
    public float bulletSpeed = 10f;

    // Interval between consecutive bullet instantiations
    public float fireRate = 0.2f;

    // Timer to control the rate of fire
    private float fireTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Continuously fire bullets while holding down the spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            // Increment the fire timer
            fireTimer += Time.deltaTime;

            // If the fire timer exceeds the fire rate, instantiate a bullet and reset the timer
            if (fireTimer >= fireRate)
            {
                FireBullet();
                fireTimer = 0f;
            }
        }
    }

    // Method to instantiate a bullet
    void FireBullet()
    {
        // Find all game objects with the "MainWeapon" tag
        GameObject[] mainWeapons = GameObject.FindGameObjectsWithTag("MainWeapon");

        // Check if any main weapon is found
        if (mainWeapons.Length > 0)
        {
            // Iterate through each main weapon found
            foreach (GameObject mainWeapon in mainWeapons)
            {
                // Instantiate the bullet prefab at the main weapon's position and rotation
                Quaternion rotation = mainWeapon.transform.rotation;
                rotation *= Quaternion.Euler(90, 0, 0); // Rotate by 90 degrees around the x-axis
                GameObject bullet = Instantiate(bulletPrefab, mainWeapon.transform.position + mainWeapon.transform.forward * bulletSpawnOffset.z, rotation);

                // Get the bullet's rigidbody component
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

                // Check if bulletRigidbody is found
                if (bulletRigidbody != null)
                {
                    // Apply velocity to move the bullet forward
                    bulletRigidbody.velocity = mainWeapon.transform.forward * bulletSpeed;

                    // Destroy the bullet after 2 seconds
                    Destroy(bullet, 5f);
                }
                else
                {
                    Debug.LogWarning("Rigidbody component not found on the instantiated bullet.");
                }
            }
        }
        else
        {
            Debug.LogWarning("No game objects found with the 'MainWeapon' tag.");
        }
    }

}
