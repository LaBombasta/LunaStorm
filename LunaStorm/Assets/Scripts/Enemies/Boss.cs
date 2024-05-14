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
                StopMinions();
                break;
            case 1:
                //Debug.Log("Spinning");
                StartCoroutine(Spin());
                break;
            case 2:
                //Debug.Log("Lcoked");
                StartCoroutine(LockOn());
                break;
            case 3:
               //Debug.Log("waving");
                StartCoroutine(Wavy());
                break;
            case 4:
                //Debug.Log("Straight");
                StartCoroutine(Straight());
                break;
            case 5:
                //Debug.Log("Stopped");
                StartCoroutine(StoppedFire());
                break;
            case 6:
                //Debug.Log("TheCage");
                StartCoroutine(TheCage());
                break;
            case 7:
                //Debug.Log("Oppress");
                StartCoroutine(Oppress());
                break;
            case 8:
                //Debug.Log("DelayedWave");
                StopMinions();
                StartCoroutine(DelayedWave());
                break;
            default:
                break;
        }
    }
    private IEnumerator Idle()
    {
        //Debug.Log("Starting my Idle");
        StopAllGuns();
        yield return new WaitForSeconds(2);
        RandomSelection();
    }
    private void RandomSelection()
    {
        int rando = Random.Range((int)1, 7);
        SetMoveType(rando);
        //Debug.Log("Choosing:" + rando);
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
            attachedGuns[i].SetFireRate(wavyFR);
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
            attachedGuns[i].SetWavy();
            attachedGuns[i].SetFireRate(wavyFR);
            attachedGuns[i].StartFiring();
            
        }
        for (int i = 2; i < 4; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(spinFR);
            attachedGuns[i].StartFiring();
        }
        for (int i = 4; i < 6; i++)
        {
            attachedGuns[i].SetLockedOn();
            attachedGuns[i].SetFireRate(.5f);
            attachedGuns[i].StartFiring();
        }
        attachedGuns[6].SetSpin();
        attachedGuns[6].SetFireRate(.1f);
        attachedGuns[6].StartFiring();

        yield return new WaitForSeconds(attackTimer * 2);
        SetMoveType(0);
    }
    private IEnumerator DelayedWave()
    {
        StopAllGuns();
        for (int i = 0; i < 2; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(.15f);
            attachedGuns[i].StartFiring();

        }
        yield return new WaitForSeconds(.6f);
        for (int i = 2; i < 4; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(.15f);
            attachedGuns[i].StartFiring();
        }
        yield return new WaitForSeconds(.6f);
        for (int i = 4; i < 6; i++)
        {
            attachedGuns[i].SetSpin();
            attachedGuns[i].SetFireRate(.15f);
            attachedGuns[i].StartFiring();
        }
        attachedGuns[6].LockedOn();
        attachedGuns[6].SetFireRate(.7f);
        attachedGuns[6].StartFiring();
        yield return new WaitForSeconds(12);
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
            StopAllCoroutines();
            StopAllGuns();
            SummonMinions();
            StartCoroutine(SpawnShield());
        }
        if (phase2 && phase < maxHp * .25)
        {
            phase2 = false;
            StopAllCoroutines();
            StopAllGuns();
            SummonMinions();
            StartCoroutine(SpawnShield());
        }
    }
    public IEnumerator SpawnShield()
    {
        GameObject barrier = Instantiate(shield, shieldLocation.transform.position,Quaternion.Euler(-20,180,0));
        while(barrier!=null)
        {
            yield return null;
        }
        SetMoveType(8);
    }
    public void SetMinions(WaveSpawner minion)
    {
        myMinions = minion;
    }
    public void SummonMinions()
    {
        if(myMinions != null)
        {
            myMinions.BEGINDESTRUCTION();
        }
       
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
