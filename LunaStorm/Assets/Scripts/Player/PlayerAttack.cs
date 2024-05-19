using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject gunFlashPrefab;
    public Vector3 bulletSpawnOffset = new Vector3(0f, 0f, 1f);
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;
    public float missileRate = 1.0f;
    private float fireTimer = 0f;
    public int upgradeCount = 0;
    [SerializeField]
    private int maxUpgradeCount = 2;
    public Transform bulletSpawnCenter;
    public Transform bulletSpawnLWing;
    public Transform bulletSpawnRWing;
    private HomingMissile hMissile;

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
                AudioManager.instance.PlaySoundEffects(AudioManager.instance.PlayerGunfire);
                FireBullet();
                fireTimer = 0f;
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            fireTimer += Time.deltaTime;
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
        AudioManager.instance.PlaySoundEffects(AudioManager.instance.PlayerGunfire);
        GameObject gunFlash;

        Quaternion rotationC = bulletSpawnCenter.transform.rotation;
        Quaternion rotationL = bulletSpawnLWing.transform.rotation;
        Quaternion rotationR = bulletSpawnRWing.transform.rotation;

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
                bulletC = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position - new Vector3(0.5f, 0, 0) + offsetCenter, rotationC);
                bulletC1 = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position + new Vector3(0.5f, 0, 0) + offsetCenter, rotationC);
                bulletL = InstantiateBullet(bulletPrefab, bulletSpawnLWing.transform.position + offsetLWing, rotationL);
                bulletR = InstantiateBullet(bulletPrefab, bulletSpawnRWing.transform.position + offsetRWing, rotationR);
                break;

            case 2:
                bulletC = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position - new Vector3(0.75f, 0, 0) + offsetCenter, rotationC);
                bulletC1 = InstantiateBullet(bulletPrefab, bulletSpawnCenter.transform.position + new Vector3(0.75f, 0, 0) + offsetCenter, rotationC);

                rotationL = rotationL * Quaternion.Euler(0, -20.0f, 0);
                rotationR = rotationR * Quaternion.Euler(0, 20.0f, 0);
                Quaternion rotationL1 = rotationL * Quaternion.Euler(0, -20.0f, 0);
                Quaternion rotationR1 = rotationR * Quaternion.Euler(0, 20.0f, 0);

                bulletL = InstantiateBullet(bulletPrefab, bulletSpawnLWing.transform.position - new Vector3(0.25f, 0, 0) + offsetLWing, rotationL);
                bulletL1 = InstantiateBullet(bulletPrefab, bulletSpawnLWing.transform.position + new Vector3(0.25f, 0, 0) + offsetLWing, rotationL1);
                bulletR = InstantiateBullet(bulletPrefab, bulletSpawnRWing.transform.position - new Vector3(0.25f, 0, 0) + offsetRWing, rotationR);
                bulletR1 = InstantiateBullet(bulletPrefab, bulletSpawnRWing.transform.position + new Vector3(0.25f, 0, 0) + offsetRWing, rotationR1);
                break;

            default:
                Debug.LogWarning("Invalid upgrade count.");
                return;
        }

        gunFlash = Instantiate(gunFlashPrefab, bulletSpawnCenter.transform.position, bulletSpawnCenter.transform.rotation);
        Destroy(gunFlash, gunFlash.GetComponent<ParticleSystem>().main.duration);

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

        if (bulletRigidbody != null)
        {
            Vector3 velocity = bullet.transform.forward * bulletSpeed;
            bulletRigidbody.velocity = velocity;
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on the instantiated bullet.");
        }

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
            //Debug.Log("PoweredUpdudeeee");
            IncreaseUpgradeCount();
        }
    }

    public void IncreaseUpgradeCount()
    {
        upgradeCount++;
        if(upgradeCount>maxUpgradeCount)
        {
            upgradeCount = maxUpgradeCount;
        }else
        {
            AudioManager.instance.PlaySoundEffects(AudioManager.instance.PowerUp);
        }
    }

    public void DecreaseUpgradeCount()
    {
        if(upgradeCount>0)
        {
            upgradeCount--;
            AudioManager.instance.PlaySoundEffects(AudioManager.instance.PowerDown);
        }
    }
    public void ResetUpgradeCount()
    {
        upgradeCount = 0;
    }
}
