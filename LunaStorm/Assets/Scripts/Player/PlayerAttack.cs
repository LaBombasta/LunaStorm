using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Vector3 bulletSpawnOffset = new Vector3(0f, 0f, 1f);
    public float bulletSpeed = 10f;

    HomingMissile hMissile;

    public float fireRate = 0.2f;
    private float fireTimer = 0f;

    public int upgradeCount = 0;

    // Reference to the original bullet spawn points
    public Transform bulletSpawnCenter;
    public Transform bulletSpawnLWing;
    public Transform bulletSpawnRWing;

    private void Start()
    {
        hMissile = GetComponent<HomingMissile>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                FireBullet();
                fireTimer = 0f;
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                hMissile.FireMissile();
                fireTimer = 0f;
            }
        }

       /* // Handle upgrades
        if (upgradeCount == 1)
        {
            if (bulletSpawnCenter != null)
            {
                // Move the existing bulletSpawnCenter GameObject
                bulletSpawnCenter.localPosition += new Vector3(0.25f, 0f, 0f);

                // Instantiate a new bulletSpawnCenter GameObject as a child of the player character
                GameObject newBulletSpawnCenter = Instantiate(bulletSpawnCenter.gameObject, transform.position, Quaternion.identity, transform);

                // Set the position of the new bulletSpawnCenter
                newBulletSpawnCenter.transform.localPosition = new Vector3(0.5f, bulletSpawnCenter.localPosition.y, bulletSpawnCenter.localPosition.z);
            }
            else
            {
                Debug.LogError("BulletSpawn Center not assigned.");
            }
        }


        else if (upgradeCount == 2)
        {
            if (bulletSpawnLWing != null && bulletSpawnRWing != null)
            {
                bulletSpawnLWing.localRotation = Quaternion.Euler(0f, 20f, 0f);
                bulletSpawnRWing.localRotation = Quaternion.Euler(0f, -20f, 0f);
            }
            else
            {
                Debug.LogError("BulletSpawn LWing or BulletSpawn RWing not assigned.");
            }
        }*/
    }

    void FireBullet()
    {
        GameObject[] mainWeapons = GameObject.FindGameObjectsWithTag("MainWeapon");
        if (mainWeapons.Length > 0)
        {
            foreach (GameObject mainWeapon in mainWeapons)
            {
                Quaternion rotation = mainWeapon.transform.rotation;
                rotation *= Quaternion.Euler(90, 0, 0);
                GameObject bullet = Instantiate(bulletPrefab, mainWeapon.transform.position + mainWeapon.transform.forward * bulletSpawnOffset.z, rotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = mainWeapon.transform.forward * bulletSpeed;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Upgrade"))
        {
            Destroy(collision.gameObject);

            upgradeCount++;
        }
    }



}
