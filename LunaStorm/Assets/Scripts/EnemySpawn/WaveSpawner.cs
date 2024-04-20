using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // enemy spawn point
    //[SerializeField] private GameObject SpawnPoints;
    [SerializeField] private GameObject[] spawnPoints; 

    // create countdown to next wave
    [SerializeField] private float waveCountDown;

    // create an array of waves
    public EnemyWave[] wavesOfEnemies;

    // Wave Index
    public int enemyWaveIndex = 0;
    private bool doOnce = true;

    // spawn point index
    //private int spawnPointIndex = 0;

    //private bool startWaveCountDown;


    /*
    // Start is called before the first frame update
    void Start()
    {
             
        
        // count down between waves... might be changed with collision
        startWaveCountDown = true;

        for (int i = 0; i < wavesOfEnemies.Length; i++)
        {
            wavesOfEnemies[i].enemiesLeftInWave = wavesOfEnemies[i].enemies.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyWaveIndex >= wavesOfEnemies.Length)
        {
            Debug.Log("Congratulations!!! you have completed all waves");

            return;
        }

        // Only start countdown when startWaveCountDown is true
        if (startWaveCountDown == true)
        {
            // countdown to next wave
            waveCountDown -= Time.deltaTime;

        }

        if (waveCountDown <= 0)
        {
            startWaveCountDown = false;

            // the waveCountDown timer = pauseBetweenWave amount
            waveCountDown = wavesOfEnemies[enemyWaveIndex].pauseBetweenWaves;

            // using the spawn point array, will move from 1 spawn point to the next releasing each wave.
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                Debug.Log("made it to spawn points");

                // spawn waves
                StartCoroutine(SpawnEnemyWave());
                spawnPointIndex++;
            }
            
        }

        if (wavesOfEnemies[enemyWaveIndex].enemiesLeftInWave == 0)
        {
            startWaveCountDown = true;
            enemyWaveIndex++;
                       
        }
    }

    */
    public void BEGINDESTRUCTION()
    {
        StartCoroutine(SpawnEnemyWave());
    }
    private IEnumerator SpawnEnemyWave()
    {
        yield return new WaitForSeconds(waveCountDown);
        if (enemyWaveIndex < wavesOfEnemies.Length)
        {
            for (int i = 0; i < wavesOfEnemies[enemyWaveIndex].enemies.Length; i++)
            {

                Enemy enemy = Instantiate(wavesOfEnemies[enemyWaveIndex].enemies[i], spawnPoints[wavesOfEnemies[enemyWaveIndex].spawnLocation].transform); //spawnPoints[Enemywave.spawn].transform;
                //enemy.transform.SetParent(spawnPoints[i].transform);
                
                Destroy(enemy, wavesOfEnemies[enemyWaveIndex].enemyLifeTime);

                yield return new WaitForSeconds(wavesOfEnemies[enemyWaveIndex].timeToNextEnemy);
            }
            yield return new WaitForSeconds(wavesOfEnemies[enemyWaveIndex].timeUntilNextWave);
            enemyWaveIndex++;
            
        }

        GameManager.instance.FinishWave();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(doOnce)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.EnterBattle();
                BEGINDESTRUCTION();
            }
        }
        doOnce = false;
        

    }

    // create an array of enemies
    [System.Serializable]
    public class EnemyWave
    {
        // array of enemies
        public Enemy[] enemies;

        // time betweeen waves
        public float pauseBetweenWaves;

        // spacing between enemies
        public float timeToNextEnemy;

        public float timeUntilNextWave;

        public int enemyLifeTime;

        [Range (0, 6)]
        public int spawnLocation;

        //public int enemyMovement;
    }
}
