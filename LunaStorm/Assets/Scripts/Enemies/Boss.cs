using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // reference to wave spawner 

    [Header("Gun Attibutes")]
    [SerializeField] private float attackTimer;
    [SerializeField] private float normalFR;
    [SerializeField] private float straightFR;
    [SerializeField] private float spinFR;
    [SerializeField] private float wavyFR;
    [SerializeField] private float burstFR;
    [SerializeField] private float lockedOnFR;

    [SerializeField] private BulletSpawner[] attachedGuns;
    [SerializeField] private GameObject[] droppedItem;
    [SerializeField] private GameObject shieldLocation;
    [SerializeField] private GameObject shield;
    [SerializeField] private WaveSpawner myMinions;

    private bool phase1 = true;
    private bool phase2;

    private void Start()
    {
        attachedGuns = gameObject.GetComponentsInChildren<BulletSpawner>(true);
        //CallingAllGuns();
        //SetMoveType(3);
    }
    

    public void SetMoveType(int behaviour)
    {
        switch (behaviour)
        {
            case 0:
                StartCoroutine(Idle());
                break;
            case 1:
                Debug.Log("Spinning");
                StartCoroutine(Spin());
                break;
            case 2:
                Debug.Log("Lcoked");
                StartCoroutine(LockOn());
                break;
            case 3:
                Debug.Log("waving");
                StartCoroutine(Wavy());
                break;
            case 4:
                Debug.Log("Straight");
                StartCoroutine(Straight());
                break;
            case 5:
                Debug.Log("Stopped");
                StartCoroutine(StoppedFire());
                break;
            case 6:
                Debug.Log("TheCage");
                StartCoroutine(TheCage());
                break;
            case 7:
                Debug.Log("Oppress");
                StartCoroutine(Oppress());
                break;
            case 8:
                
                break;
            default:
                break;
        }
    }
    private IEnumerator Idle()
    {
        StopAllGuns();
        yield return new WaitForSeconds(2);
        RandomSelection();
    }
    private void RandomSelection()
    {
        int rando = Random.Range((int)1, 7);
        SetMoveType(rando);
    }
    private IEnumerator Straight()
    {
        StopAllGuns();
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetNormal();
            attachedGuns[i].SetFireRate(normalFR);
            attachedGuns[i].StartFiring();
        }
        yield return new WaitForSeconds(attackTimer);
        SetMoveType(0);
    }

    private IEnumerator Spin()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int j = 0; j < attachedGuns.Length-1; j++)
        {
            attachedGuns[j].SetSpin();
            attachedGuns[j].SetFireRate(spinFR);
            attachedGuns[j].StartFiring();
        }
        attachedGuns[attachedGuns.Length - 1].SetWavy();
        attachedGuns[attachedGuns.Length - 1].SetFireRate(spinFR);
        attachedGuns[attachedGuns.Length - 1].StartFiring();

        yield return new WaitForSeconds(attackTimer);
        SetMoveType(0);
    }
    private IEnumerator LockOn()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length-1; i++)
        {
            attachedGuns[i].SetLockedOn();
            attachedGuns[i].SetFireRate(lockedOnFR);
            attachedGuns[i].StartFiring();
        }
        yield return new WaitForSeconds(attackTimer);
        SetMoveType(0);
    }
    private IEnumerator Wavy()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length-1; i++)
        {
            attachedGuns[i].SetWavy();
            attachedGuns[i].SetFireRate(wavyFR);
            attachedGuns[i].StartFiring();
        }
        attachedGuns[attachedGuns.Length - 1].SetWavy();
        attachedGuns[attachedGuns.Length - 1].SetFireRate(spinFR);
        attachedGuns[attachedGuns.Length - 1].StartFiring();
        yield return new WaitForSeconds(attackTimer);
        SetMoveType(0);
    }
    private IEnumerator StoppedFire()
    {
        StopAllGuns();
        yield return new WaitForSeconds(2);
        for (int i = 0; i < attachedGuns.Length-1; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(spinFR);
            attachedGuns[i].StartFiring();
        }
        yield return new WaitForSeconds(2);
        StopAllGuns();
        yield return new WaitForSeconds(2);
        for (int i = 0; i < attachedGuns.Length-1; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(spinFR);
            attachedGuns[i].StartFiring();
        }
        yield return new WaitForSeconds(2);
        StopAllGuns();
        yield return new WaitForSeconds(2);
        for (int i = 0; i < attachedGuns.Length-1; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(spinFR);
            attachedGuns[i].StartFiring();
        }
        yield return new WaitForSeconds(attackTimer);
        SetMoveType(0);
    }
    private IEnumerator TheCage()
    {
        StopAllGuns();
        for (int i = 0; i < 2; i++)
        {
            attachedGuns[i].SetNormal();
            attachedGuns[i].SetFireRate(spinFR);
            attachedGuns[i].StartFiring();
        }
        for (int i = 2; i < 4; i++)
        {
            attachedGuns[i].LockedOn();
            attachedGuns[i].SetFireRate(lockedOnFR);
            attachedGuns[i].StartFiring();
        }
        for (int i = 4; i < 6; i++)
        {
            attachedGuns[i].SetWavy();
            attachedGuns[i].SetFireRate(lockedOnFR);
            attachedGuns[i].StartFiring();
        }

        yield return new WaitForSeconds(attackTimer * 2);
        SetMoveType(0);
    }
    private IEnumerator Oppress()
    {
        StopAllGuns();
        for (int i = 0; i < 2; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(spinFR);
            attachedGuns[i].StartFiring();
        }
        for (int i = 2; i < 4; i++)
        {
            attachedGuns[i].SetWavy();
            attachedGuns[i].SetFireRate(wavyFR);
            attachedGuns[i].StartFiring();
        }
        for (int i = 4; i < 6; i++)
        {
            attachedGuns[i].SetLockedOn();
            attachedGuns[i].SetFireRate(lockedOnFR);
            attachedGuns[i].StartFiring();
        }

        yield return new WaitForSeconds(attackTimer * 2);
        SetMoveType(0);
    }


    private void StopAllGuns()
    {
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].StopFiring();
            attachedGuns[i].ResetGun();
        }
    }

    public void CalculatePhase()
    {
        int phase = GetComponent<Health>().getHP();
        int maxHp = GetComponent<Health>().getMaxHP();
        if (phase1&&phase<maxHp*.75)
        {
            phase1 = false;
            phase2 = true;
            SpawnShield();
            StopAllCoroutines();
            StopAllGuns();
            SummonMinions();
            //stop all coroutines
            //start shield timer
            //call something on wave spawner
            //after wave restart couroutines
            //SpawnSheild
        }
        if (phase2 && phase < maxHp * .5)
        {
            phase2 = false;
            //phase3 = true;
            //spawnSheild
        }
    }
    public void SpawnShield()
    {
        Instantiate(shield, shieldLocation.transform.position,Quaternion.Euler(0,0,0));
    }
    public void SetMinions(WaveSpawner minion)
    {
        myMinions = minion;
    }
    public void SummonMinions()
    {
        myMinions.BEGINDESTRUCTION();
    }
    public void StopMinions()
    {
        myMinions.StopMe();
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
        //This is where you spawn an explosion particle effect
    }
    private void CallingAllGuns()
    {
        for (int j = 0; j < attachedGuns.Length; j++)
        {
            Debug.Log(attachedGuns[j].gameObject.name);
        }
    }

}
