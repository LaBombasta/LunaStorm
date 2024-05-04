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

    //separate out the homing missle code into its own script

   
}
