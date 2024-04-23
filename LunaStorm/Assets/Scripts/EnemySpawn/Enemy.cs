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
                break;
            case 1:
                StartCoroutine(RightTurn());
                break;
            case 2:
                Debug.Log("what is going on?");
                StartCoroutine(LeftTurn());
                break;
            default:
                break;
        }
    }

    private IEnumerator RightTurn()
    {
        StopAllGuns();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < attachedGuns.Length; i++)
        {
            attachedGuns[i].SetNormal();
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
            attachedGuns[i].StartFiring();
        }

        yield return new WaitForSeconds(2);

        while (turnDegree < 90)
        {
            turnDegree += turnSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime);
            Debug.Log(turnDegree);
            yield return new WaitForSeconds(0);
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
    private void OnDestroy()
    {
        Destroy(gameObject);
        //This is where you spawn an explosion particle effect
    }
}
