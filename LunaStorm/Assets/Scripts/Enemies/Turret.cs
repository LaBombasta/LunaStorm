using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    protected enum TurretAIState { Idle, Attacking }
    [SerializeField] protected TurretAIState currentState;

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject turretAmmo;

    private float distanceToTarget;
    [SerializeField] private float maxRange;
    [SerializeField] private Transform ammoSpawnPoint_L;
    [SerializeField] private Transform ammoSpawnPoint_R;
    private bool weaponFired = false;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float timeToExplode;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget <= maxRange)
        {
            // set player location to temp location
            currentState = TurretAIState.Attacking;
        }
        else
        {

            currentState = TurretAIState.Idle;
        }

        switch (currentState)
        {
            case TurretAIState.Idle:

                break;

            case TurretAIState.Attacking:

                transform.LookAt(target.transform.position);
                if (weaponFired == false)
                {
                    weaponFired = true;
                    
                    GameObject turretRound_Left = Instantiate(turretAmmo, ammoSpawnPoint_L.position, ammoSpawnPoint_L.rotation);
                    Rigidbody  turretRound_L_RB = turretRound_Left.GetComponent<Rigidbody>();
                    GameObject turretRound_Right = Instantiate(turretAmmo, ammoSpawnPoint_R.position, ammoSpawnPoint_R.rotation);
                    Rigidbody  turretRound_R_RB = turretRound_Right.GetComponent<Rigidbody>();
                    Destroy(turretRound_Right, timeToExplode);
                    Destroy(turretRound_Left, timeToExplode);
                    /*
                    if (turretRound_L_RB != null || turretRound_R_RB != null)
                    {
                        turretRound_L_RB.velocity = transform.forward * projectileSpeed;
                        
                        turretRound_R_RB.velocity = transform.forward * projectileSpeed;
                        
                       
                    }
                    else
                    {
                        Debug.LogWarning("Rigidbody component not found on the instantiated bullet.");
                    }
                    */

                    Invoke(nameof(ResetWeaponFired), timeBetweenShots);

                }


                break;
        }

    }


    private void ResetWeaponFired()
    {
        weaponFired = false;


    }










}




