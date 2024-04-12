using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public enum GunType
    {
        Normal,   //Shoots with the parents transform.
        Straight, //This will always point to its orginal rotation, can be reset with the second override.
        Spin,     //Spins 
        Wavy,
        Burst,
        LockedOn,
        ResetGun,
    } //this will dictate the pattern

    [Header("Bullet Attributes")]
    public GameObject bullet; //
    public float bulletLifetime = 1f;
    public float speed = 1f;

    [Header("Gun Attibutes")]
    [SerializeField] private GunType gunType;
    [SerializeField] private float fireRate = 1f;
    //[SerializeField] private float firingAngle;
    [SerializeField] private bool isFiring;
    [SerializeField] private Vector3 rotSpeed = new Vector3(0, 1, 0);



    private Vector3 startPos;
    private Vector3 spinDirection;
    private Quaternion startRotation;
    private GameObject activeBullet;
    private GameObject target;
    private float timer = 0f;

    private void Start()
    {
        startPos = this.transform.localPosition;
        startRotation = this.transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            timer += Time.deltaTime;
            switch (gunType)
            {
                case GunType.Normal:
                    Normal();
                    break;
                case GunType.Straight:
                    // this is always point towards its original orientation
                    Straighten();
                    break;
                case GunType.Spin:
                    Spin();
                    break;
                case GunType.Wavy:
                    Wavy();
                    break;
                case GunType.Burst:
                    break;
                case GunType.LockedOn:
                    LockedOn();
                    break;
                case GunType.ResetGun:
                    ResetGun();
                    break;
                default:
                    break;
            }
            if (timer >= fireRate)
            {
                Fire();
                timer = 0;
            }
        }
    }

    //Gun Functions

    // a basic firing function to create our bullets.
    private void Fire()
    {

        if (bullet)
        {
            activeBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            activeBullet.GetComponent<Bullet>().speed = speed;
            activeBullet.GetComponent<Bullet>().bulletLifeTime = bulletLifetime;
            activeBullet.transform.rotation = transform.rotation;
        }
    }
    public void StopFiring()
    {
        isFiring = false;
    }
    public void StartFiring()
    {
        ResetGun();
        isFiring = true;
    }
    //Resets to its original position and its original rotation as well as setting the shotting interval
    public void ResetGun()
    {
        this.transform.SetLocalPositionAndRotation(startPos, Quaternion.identity);
        timer = 0f;
    }
    //Set it to shoot straight forward 
    private void Normal()
    {
        this.transform.SetLocalPositionAndRotation(startPos, Quaternion.identity);
    }
    private void Straighten()
    {
        this.transform.rotation = startRotation;
    }
    public void Straighten(Vector3 newDir)
    {

        startRotation = Quaternion.Euler(newDir.x, newDir.y, newDir.z);
        Straighten();
    }
    private void Spin()
    {
        transform.Rotate(rotSpeed);
    }

    private void Wavy()
    {
        transform.Rotate(new Vector3(1, 1, 0));
    }

    private void LockedOn()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (target == null)
        {
            Debug.Log("cannot detect");
            gunType = GunType.Normal;
            return;
        }
        if (target)
        {
            Transform locked = target.transform;
            transform.LookAt(locked);
            transform.rotation = transform.rotation * Quaternion.Euler(0, -90, 0);

        }
    }
   
}
