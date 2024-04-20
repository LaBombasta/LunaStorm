using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // speed of enemy
    public float enemySpeed;
    // reference to wave spawner
    private WaveSpawner waveSpawner;
    private bool doOnce;
    [SerializeField] private BulletSpawner[] attachedGuns;


    private void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
        attachedGuns = gameObject.GetComponentsInChildren<BulletSpawner>(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * enemySpeed * Time.deltaTime, Space.World);
        if(doOnce)
        {
            //switch
            //start couroutines for lifetime of the enemy
            //rotate the y value peroidically
            //lower or raise enemy speed over times
            //change gun pattern
        }
        doOnce = false;
    }

    public void SetWaveSpawner(WaveSpawner spawner)
    {

    }
    private IEnumerator SetMoveType(int behaviour)
    {
        yield return new WaitForSeconds(5f);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
        //This is where you spawn an explosion particle effect
    }
}
