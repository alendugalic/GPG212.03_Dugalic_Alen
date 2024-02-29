using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject miniBossPrefab;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalintFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondMax = 10f;
    [SerializeField] private int bossWaveInterval = 10;
    [SerializeField] private int minibossWaveInterval = 7;
    [SerializeField] private int bossesSpawned = 0;
    [SerializeField] private int maxBosses = 3;
    [SerializeField] private int miniBossesSpawned = 0;
    [SerializeField] private int maxMiniBosses = 3;

    private int currentWave = 1;
    private float timeSinceLastSpawn = 2f;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float enemiesPerSecondSpawn;
    private float bossesMultiplier = 0.5f;
    private int bossesIncreasePerInterval = 1;
    private int miniBossesIncreasePerInterval = 1;
    private bool isSpawning = false;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private void Awake()
    {
        onEnemyDestroy.AddListener(OnEnemyDestroyed);
    }
    private void Start()
    {
        StartCoroutine(StartWave());
    }
    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / enemiesPerSecondSpawn) && enemiesLeftToSpawn > 0)
        {
            if (currentWave % bossWaveInterval == 0 && bossesSpawned < maxBosses)
            {
                SpawnBoss();
                bossesSpawned += Mathf.RoundToInt(bossesIncreasePerInterval * bossesMultiplier);
            }
            else if (currentWave % minibossWaveInterval == 0 && miniBossesSpawned < maxMiniBosses)
            {
                SpawnMiniBoss();
                miniBossesSpawned += Mathf.RoundToInt(miniBossesIncreasePerInterval * bossesMultiplier);
            }
            else
            {
                SpawnEnemy();
            }
            
            enemiesLeftToSpawn --;
            enemiesAlive ++;
            timeSinceLastSpawn = 0f;
        }
        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void OnEnemyDestroyed()
    {
        enemiesAlive --;
    }
    private void SpawnEnemy()
    {
        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }
    private void SpawnBoss()
    {
        Instantiate(bossPrefab, LevelManager.instance.startPoint.position, Quaternion.identity);
    }
    private void SpawnMiniBoss()
    {
        Instantiate(miniBossPrefab, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemiesPerSecondSpawn = EnemiesPerSecond();
    }
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        StartCoroutine(StartWave());
        currentWave++;
    }

    private int EnemiesPerWave()
    {
        int baseEnemiesForWave = Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalintFactor));

        baseEnemiesForWave -= Mathf.RoundToInt(bossesIncreasePerInterval * bossesMultiplier);
        baseEnemiesForWave -= Mathf.RoundToInt(miniBossesIncreasePerInterval * bossesMultiplier);

        return Mathf.Max(baseEnemiesForWave, 0);
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp((enemiesPerSecond * Mathf.Pow(currentWave, 0.2f)), 0f, enemiesPerSecondMax);
    }

}
