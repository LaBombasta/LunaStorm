using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Vector3 bulletSpawnOffset = new Vector3(0f, 0f, 1f);
    public float bulletSpeed = 10f;

    public GameObject missilePrefab;
    public Vector3 missileSpawnOffset = new Vector3(0f, 0f, 1f);
    public float missileSpeed = 20f;

    public float fireRate = 0.2f;
    private float fireTimer = 0f;

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
                HomingMissile();
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

    void HomingMissile()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        if (turrets.Length > 0)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
            missile.transform.LookAt(turrets[0].transform);
            StartCoroutine(Homing(missile, turrets[0].transform));
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                GameObject missile = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
                missile.transform.LookAt(enemies[0].transform);
                StartCoroutine(Homing(missile, enemies[0].transform));
            }
            else
            {
                Debug.LogWarning("No game objects found with the 'Enemy' tag.");
            }
        }
    }

    //add code to find the distance from the player to the enemies, and home towards the closest one
    public IEnumerator Homing(GameObject missile, Transform target)
    {
        while (Vector3.Distance(target.position, missile.transform.position) > 0.3f)
        {
            missile.transform.position += (target.position - missile.transform.position).normalized * missileSpeed * Time.deltaTime;
            missile.transform.LookAt(target);
            yield return null;
        }
        Destroy(missile);
    }
}
