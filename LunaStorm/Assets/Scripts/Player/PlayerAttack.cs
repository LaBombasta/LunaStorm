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
    public float missileRate = 1.0f;
    private float fireTimer = 0f;
    

    public int upgradeCount = 0;

    // Reference to the original bullet spawn points
    public Transform bulletSpawnCenter;
    public Transform bulletSpawnLWing;
    public Transform bulletSpawnRWing;

    [SerializeField] public AudioManager audioManager;

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
        else if (Input.GetKey(KeyCode.F))
        {
            fireTimer += Time.deltaTime;
            //Debug.Log(fireTimer);
            if (fireTimer >= missileRate)
            {
                missileRate = .5F;
                hMissile.FireMissile();
                fireTimer = 0f;
            }
        }
    }

    void FireBullet()
    {
        Quaternion rotationC = bulletSpawnCenter.transform.rotation * Quaternion.Euler(0, 0, 0);
        Quaternion rotationL = bulletSpawnLWing.transform.rotation * Quaternion.Euler(0, 0, 0);
        Quaternion rotationR = bulletSpawnRWing.transform.rotation * Quaternion.Euler(0, 0, 0);

        Vector3 offsetCenter = bulletSpawnCenter.transform.forward * bulletSpawnOffset.z;
        Vector3 offsetLWing = bulletSpawnLWing.transform.forward * bulletSpawnOffset.z;
        Vector3 offsetRWing = bulletSpawnRWing.transform.forward * bulletSpawnOffset.z;

        GameObject bulletC, bulletL, bulletR, bulletC1 = null, bulletL1 = null, bulletR1 = null;

        switch (upgradeCount)
        {
            case 0:
                bulletC = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position + offsetCenter, rotationC);
                bulletL = InstantiateBullet(bulletPrefab, bulletSpawnLWing.transform.position + offsetLWing, rotationL);
                bulletR = InstantiateBullet(bulletPrefab, bulletSpawnRWing.transform.position + offsetRWing, rotationR);
                break;

            case 1:
                bulletC = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position - new Vector3(0.25f, 0, 0) + offsetCenter, rotationC);
                bulletC1 = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position + new Vector3(0.25f, 0, 0) + offsetCenter, rotationC);
                bulletL = InstantiateBullet(bulletPrefab, bulletSpawnLWing.transform.position + offsetLWing, rotationL);
                bulletR = InstantiateBullet(bulletPrefab, bulletSpawnRWing.transform.position + offsetRWing, rotationR);
                break;

            case 2:
                bulletC = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position - new Vector3(0.25f, 0, 0) + offsetCenter, rotationC);
                bulletC1 = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position + new Vector3(0.25f, 0, 0) + offsetCenter, rotationC);
                bulletL = InstantiateBullet(bulletPrefab, bulletSpawnLWing.transform.position - new Vector3(0.25f, 0, 0) + offsetLWing, rotationL);
                bulletL1 = InstantiateBullet(bulletPrefab, bulletSpawnLWing.transform.position + new Vector3(0.25f, 0, 0) + offsetLWing, rotationL);
                bulletR = InstantiateBullet(bulletPrefab, bulletSpawnRWing.transform.position - new Vector3(0.25f, 0, 0) + offsetRWing, rotationR);
                bulletR1 = InstantiateBullet(bulletPrefab, bulletSpawnRWing.transform.position + new Vector3(0.25f, 0, 0) + offsetRWing, rotationR);
                break;

            default:
                Debug.LogWarning("Invalid upgrade count.");
                return;
        }

        ApplyBulletVelocityAndDestroy(bulletC, bulletSpeed);
        ApplyBulletVelocityAndDestroy(bulletL, bulletSpeed);
        ApplyBulletVelocityAndDestroy(bulletR, bulletSpeed);
        ApplyBulletVelocityAndDestroy(bulletC1, bulletSpeed);
        ApplyBulletVelocityAndDestroy(bulletL1, bulletSpeed);
        ApplyBulletVelocityAndDestroy(bulletR1, bulletSpeed);
    }

    GameObject InstantiateBullet(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(prefab, position, rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        audioManager.PlaySoundEffects(audioManager.PlayerGunfire);

        if (bulletRigidbody != null)
        {
            // Calculate the velocity based on the bullet's forward direction and speed
            Vector3 velocity = bullet.transform.forward * bulletSpeed;

            Debug.Log("Bullet forward direction: " + bullet.transform.forward);


            // Apply the velocity to the Rigidbody component
            bulletRigidbody.velocity = velocity;

            // Set up a callback to destroy the bullet after a delay
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on the instantiated bullet.");
        }

        // Add debug log
        if (bulletRigidbody == null)
        {
            Debug.Log("Rigidbody component not found on the bullet GameObject: " + bullet.name);
        }

        return bullet;
    }


    void ApplyBulletVelocityAndDestroy(GameObject bullet, float speed)
    {
        if (bullet != null)
        {
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = bullet.transform.forward * speed;
                Destroy(bullet, 1.2f);
            }
            else
            {
                Debug.LogWarning("Rigidbody component not found on the instantiated bullet.");
            }
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
