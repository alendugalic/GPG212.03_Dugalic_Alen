using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalintFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn = 2f;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private void Awake()
    {
        onEnemyDestroy.AddListener(OnEnemyDestroyed);
    }
    private void Start()
    {
        StartWave();
    }
    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
           
            SpawnEnemy();
            enemiesLeftToSpawn --;
            enemiesAlive ++;
            timeSinceLastSpawn = 0f;
        }
    }

    private void OnEnemyDestroyed()
    {
        enemiesAlive --;
    }
    private void SpawnEnemy()
    {
        Debug.Log("Spawn Enemies");
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalintFactor));
    }

}
