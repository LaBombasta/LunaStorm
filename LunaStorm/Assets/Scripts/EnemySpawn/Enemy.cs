using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // speed of enemy
    [SerializeField] private float enemySpeed;

    // destroy enemy after # seconds
    [SerializeField] private float timeToDestroyEnemy = 5f;

    // reference to wave spawner
    private WaveSpawner waveSpawner;

    private void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * enemySpeed * Time.deltaTime);

        timeToDestroyEnemy -= Time.deltaTime;

        if (timeToDestroyEnemy < 0)
        {
            Destroy(gameObject);

            waveSpawner.wavesOfEnemies[waveSpawner.enemyWaveIndex].enemiesLeftInWave--;
        }

    }
}
