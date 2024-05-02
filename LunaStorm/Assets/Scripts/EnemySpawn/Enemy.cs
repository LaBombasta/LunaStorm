using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // speed of enemy
    public float enemySpeed;
    [SerializeField] private float turnSpeed;
    // reference to wave spawner 
    private float turnDegree = 0;
    [SerializeField] private BulletSpawner[] attachedGuns;
    [SerializeField] private GameObject[] droppedItem;


    private void Start()
    {
        attachedGuns = gameObject.GetComponentsInChildren<BulletSpawner>(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * enemySpeed * Time.deltaTime, Space.World);
    }

    public void SetMoveType(int behaviour)
    {
        switch (behaviour)
        {
            case 0:
                StartCoroutine(Straight());
                break;
            case 1:
                StartCoroutine(RightTurn());
                break;
            case 2:
                StartCoroutine(LeftTurn());
                break;
            case 3:
                StartCoroutine(RightTurn180());
                break;
            case 4:
                StartCoroutine(LeftTurn180());
                break;
            case 5:
                StartCoroutine(BuzzSaw());
                break;
            case 6:
                StartCoroutine(StraightLockOn());
                break;
            case 7:
                StartCoroutine(StraightWavy());
                break;
            default:
                break;
        }
    }
    private IEnumerator Straight()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetNormal();
            attachedGuns[i].SetFireRate(1);
            attachedGuns[i].StartFiring();
        }
    }
    
    
    private IEnumerator RightTurn()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetNormal();
            attachedGuns[i].SetFireRate(1);
            attachedGuns[i].StartFiring();
        }

        yield return new WaitForSeconds(2);
        
        while(turnDegree < 90)
        {
            turnDegree += turnSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, turnSpeed, 0) * Time.deltaTime);
            Debug.Log(turnDegree);
            yield return new WaitForSeconds(0);
        }
    }
    private IEnumerator LeftTurn()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetNormal();
            attachedGuns[i].SetFireRate(1);
            attachedGuns[i].StartFiring();
        }

        yield return new WaitForSeconds(2);

        while (turnDegree < 90)
        {
            turnDegree += turnSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator RightTurn180()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetLockedOn();
            attachedGuns[i].SetFireRate(1.5f);
            attachedGuns[i].StartFiring();
        }

        yield return new WaitForSeconds(6);
        StopAllGuns();
        SetTurnSpeed(120);
        while (turnDegree < 180)
        {
            
            turnDegree += turnSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, turnSpeed, 0) * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetWavy();
            attachedGuns[i].SetFireRate(.9f);
            attachedGuns[i].StartFiring();
        }

    }

    private IEnumerator LeftTurn180()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetLockedOn();
            attachedGuns[i].SetFireRate(1.5f);
            attachedGuns[i].StartFiring();
        }

        yield return new WaitForSeconds(6);
        StopAllGuns();
        SetTurnSpeed(120);
        while (turnDegree < 180)
        {
            turnDegree += turnSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }
        for (int j = 0; j < attachedGuns.Length; j++)
        {
            attachedGuns[j].SetWavy();
            attachedGuns[j].SetFireRate(.4f);
            attachedGuns[j].StartFiring();
        }
    }

    private IEnumerator BuzzSaw()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int j = 0; j < attachedGuns.Length; j++)
        {
            attachedGuns[j].SetSpin();
            attachedGuns[j].SetFireRate(.2f);
            attachedGuns[j].StartFiring();
        }
    }
    private IEnumerator StraightLockOn()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetLockedOn();
            attachedGuns[i].SetFireRate(1);
            attachedGuns[i].StartFiring();
        }
    }
    private IEnumerator StraightWavy()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetWavy();
            attachedGuns[i].SetFireRate(.5f);
            attachedGuns[i].StartFiring();
        }

    }

    private void StopAllGuns()
    {
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].StopFiring();
        }
    }

    private void SetTurnSpeed(float speed)
    {
        turnSpeed = speed;
    }
    private void ItemDrop()
    {
        int rand = (int)Random.Range(0, droppedItem.Length);
        if (droppedItem[rand] != null)
        {
            GameObject drop = Instantiate(droppedItem[rand], transform);
            Destroy(drop, 10);
        }
        else
        {
            Debug.Log("you hit nothing as your reward");
        }
       
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
        //This is where you spawn an explosion particle effect
    }
}
