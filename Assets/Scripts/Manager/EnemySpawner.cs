using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;

public class EnemySpawner : GenericSingletonClass<EnemySpawner>
{

    [SerializeField] Transform topLeft;

    [SerializeField] Transform bottomRight;

    [SerializeField] Transform playerPos;

    //[SerializeField] Transform playerPos;
    
    public int enemyCount = 0;
    [SerializeField] int maxEnemyCount = 5;

    [SerializeField] float numEnemiesIntervalImpact = 0.9f;

    [SerializeField] private float baseSpawnInterval = 5f;

    [SerializeField] GameObject barrelEnemyPrefab;

    private float left, right, top, bottom;

    void Start()
    {
        left = topLeft.position.x;
        right = bottomRight.position.x;
        top = topLeft.position.z;
        bottom = bottomRight.position.z;
        StartCoroutine(SpawnBarrelCoroutine());
    }

    private IEnumerator SpawnBarrelCoroutine()
    {
        while (true)
        {
            if (this.enemyCount < this.maxEnemyCount){
                SpawnBarrel();
            }
            yield return new WaitForSeconds(computeNextSpawnTime());
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Y) && (this.enemyCount < this.maxEnemyCount)){
            SpawnBarrel();
        }
    }

    void SpawnBarrel(){
        this.enemyCount++;
        float playerAdvance = playerPos.position.z;
        float randomX = Random.Range(left, right);
        float randomZ = playerAdvance + Random.Range(top, bottom);
        Vector3 randomSpawnPoint = new Vector3(randomX, 0, randomZ);
        Vector2 spawnPointIn2D = new Vector2(randomSpawnPoint.x, randomSpawnPoint.z);
        var enemy = Instantiate(barrelEnemyPrefab, randomSpawnPoint, Quaternion.identity);
        enemy.GetComponent<BarrelEnemy>().Spawn(spawnPointIn2D);
    }

    private float computeNextSpawnTime(){
        float timer = baseSpawnInterval * Mathf.Pow(numEnemiesIntervalImpact, (maxEnemyCount - enemyCount) - 1);
        Debug.Log("Next spawn time is " + timer);
        return timer;
    }

}
