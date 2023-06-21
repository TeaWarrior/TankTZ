using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;

    public event EventHandler OnAllEnemiesDie;
    public event EventHandler OnEnemyKilled;

    private void Awake()
    {
        
            if (instance != null)
            {
                Debug.LogWarning("More Than One Instance");
                return;
            }
            instance = this;
        enemyLive = enemyAmount;
    }

    [SerializeField] GameObject enemyPrephab;
    [SerializeField] int enemyAmount;
    [SerializeField] float timeToSpawn;

    bool isSpawning;
    public float enemyLive;
    
    public void SpawnEnemy()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, MazeGenerator.instance.spawnPoints.Count);
        Transform spawnPoint = MazeGenerator.instance.spawnPoints[spawnPointIndex];
        GameObject enemy = Instantiate(enemyPrephab, spawnPoint.position, Quaternion.identity);
        EnemyAi enemyAiScript = enemy.GetComponent<EnemyAi>();
        int randomType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(EnemyType)).Length);
       
        enemyAiScript.SetEnemyType((EnemyType)randomType);
        enemyAmount--;
    }


    
    private void Start()
    {
        

      
    }

    private void Update()
    {
        if (enemyAmount > 0)
        {
            if (!isSpawning)
            {
                StartCoroutine(EnemySpawnCorotine());

            }
        }
    }

    IEnumerator EnemySpawnCorotine()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeToSpawn);
        SpawnEnemy();
        isSpawning = false;
    }

    public void EnemyKilled()
    {
        enemyLive--;

        OnEnemyKilled?.Invoke(this, EventArgs.Empty);
        if (enemyLive<= 0)
        {
            OnAllEnemiesDie?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
