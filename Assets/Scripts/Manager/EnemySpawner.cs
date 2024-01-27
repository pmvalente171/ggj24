using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;

public class EnemySpawner : GenericSingletonClass<EnemySpawner>
{
    [SerializeField] GameObject enemySpawnParent;
    List<Transform> enemySpawnPoints = new List<Transform>();
    int lastSpawnIndex = 0;
    public int enemyCount = 0;
    [SerializeField] int maxEnemyCount = 5;


    [SerializeField] GameObject barrelEnemyPrefab;
    [SerializeField] GameObject runnerEnemyPrefab;


    void Start()
    {
        foreach (Transform child in this.enemySpawnParent.GetComponentsInChildren<Transform>())
        {
            enemySpawnPoints.Add(child);
        }

        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            if (this.enemyCount < this.maxEnemyCount)
            {
                float randomValue = Random.value;
                print(randomValue);
                if (randomValue <= 0.75f)
                {
                    SpawnEnemy(this.barrelEnemyPrefab);
                }
                else
                {
                    SpawnEnemy(this.runnerEnemyPrefab);
                }
            }
            yield return new WaitForSeconds(5f);
        }
    }


    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl) && (this.enemyCount < this.maxEnemyCount)){
            SpawnEnemy(this.barrelEnemyPrefab);
        }
    }

    private int MakeSureItDoesntSpawnInTheSamePlaceTwice(int index){
        if (index == this.lastSpawnIndex){
            return index + 1;
        }
        else {
            return index;
        }
    }

    void SpawnEnemy(GameObject enemyPrefab){
        this.enemyCount++;

        int randomIndex = Random.Range(0, enemySpawnPoints.Count);
        Vector3 randomSpawnPoint = enemySpawnPoints[MakeSureItDoesntSpawnInTheSamePlaceTwice(randomIndex)].position;
        Vector2 spawnPointIn2D = new Vector2(randomSpawnPoint.x, randomSpawnPoint.z);
        var enemy = Instantiate(enemyPrefab, randomSpawnPoint, Quaternion.identity);
        enemy.GetComponent<Enemy>().Spawn(spawnPointIn2D);

        this.lastSpawnIndex = randomIndex;
    }

}
