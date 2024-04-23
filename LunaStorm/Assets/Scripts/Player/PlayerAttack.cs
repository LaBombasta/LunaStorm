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


    // Reference to the bullet prefab
    public GameObject missilePrefab;

    // Position offset for spawning bullets relative to the main weapon's position
    public Vector3 missileSpawnOffset = new Vector3(0f, 0f, 1f);

    // Speed at which the bullet moves forward
    public float missileSpeed = 20f;

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
        //Fire homing missiles
        if(Input.GetKey(KeyCode.F))
        {
            // Increment the fire timer
            fireTimer += Time.deltaTime;

            // If the fire timer exceeds the fire rate, instantiate a bullet and reset the timer
            if (fireTimer >= fireRate)
            {
                HomingMissile();
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

                    // Destroy the bullet after 5 seconds
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

    void HomingMissile()
    {
        // Find all game objects with the "Turret" tag
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");

        // If there are turrets, target them first
        if (turrets.Length > 0)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
            missile.transform.LookAt(turrets[0].transform);

            StartCoroutine(Homing(missile));
        }
        // If there are no turrets, target other enemies
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length > 0)
            {
                GameObject missile = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
                missile.transform.LookAt(enemies[0].transform);

                StartCoroutine(Homing(missile));
            }
            else
            {
                Debug.LogWarning("No game objects found with the 'Enemy' tag.");
            }
        }
    }

    public IEnumerator Homing(GameObject missile)
    {
        while (Vector3.Distance(.transform.position, missile.transform.position) > 0.3f)
        {
            missile.transform.position += (.transform.position - missile.transform.position).normalized * missileSpeed * Time.deltaTime;
            missile.transform.LookAt(.transform);
            
            yield return null;
        }

        Destroy(missile);
    }
}