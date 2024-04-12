using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // enemy spawn point
    [SerializeField] private GameObject enemySpawnPoint;

    // create countdown to next wave
    [SerializeField] private float waveCountDown;

    // create an array of waves
    public EnemyWave[] wavesOfEnemies;
    public Dictionary<int, Enemy> enemySpawnerrrrr = new Dictionary<int, Enemy>();

    // Wave Index
    public int enemyWaveIndex = 0;

    private bool startWaveCountDown;



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

            // spawn waves
            StartCoroutine (SpawnEnemyWave());
        }

        if (wavesOfEnemies[enemyWaveIndex].enemiesLeftInWave == 0)
        {
            startWaveCountDown = true;
            enemyWaveIndex++;
                       
        }
    }


    private IEnumerator SpawnEnemyWave()
    {
        if (enemyWaveIndex < wavesOfEnemies.Length)
        {
            for (int i = 0; i < wavesOfEnemies[enemyWaveIndex].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(wavesOfEnemies[enemyWaveIndex].enemies[i], enemySpawnPoint.transform);
                enemy.transform.SetParent(enemySpawnPoint.transform);
                
                Destroy(enemy, wavesOfEnemies[enemyWaveIndex].enemyLifeTime);

                yield return new WaitForSeconds(wavesOfEnemies[enemyWaveIndex].timeToNextEnemy);
            }
        }
       

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

        // enemies left in wave
        public int enemiesLeftInWave;

        public int enemyLifeTime;

    }
}
