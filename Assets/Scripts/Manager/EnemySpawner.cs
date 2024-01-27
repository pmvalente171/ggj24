using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;

public class EnemySpawner : GenericSingletonClass<EnemySpawner>
{
    [SerializeField] GameObject barrelEnemySpawnParent;
    List<Transform> barrelEnemySpawnPoints = new List<Transform>();
    int lastSpawnIndex = 0;
    public int enemyCount = 0;
    [SerializeField] int maxEnemyCount = 5;


    [SerializeField] GameObject barrelEnemyPrefab;


    void Start()
    {
        foreach (Transform child in this.barrelEnemySpawnParent.GetComponentsInChildren<Transform>())
        {
            barrelEnemySpawnPoints.Add(child);
        }

        StartCoroutine(SpawnBarrelCoroutine());
    }

    private IEnumerator SpawnBarrelCoroutine()
    {
        while (true)
        {
            if (this.enemyCount < this.maxEnemyCount){
                SpawnBarrel();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl) && (this.enemyCount < this.maxEnemyCount)){
            SpawnBarrel();
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

    void SpawnBarrel(){
        this.enemyCount++;
        print(this.enemyCount);

        int randomIndex = Random.Range(0, barrelEnemySpawnPoints.Count);
        Vector3 randomSpawnPoint = barrelEnemySpawnPoints[MakeSureItDoesntSpawnInTheSamePlaceTwice(randomIndex)].position;
        Vector2 spawnPointIn2D = new Vector2(randomSpawnPoint.x, randomSpawnPoint.z);
        var enemy = Instantiate(barrelEnemyPrefab, randomSpawnPoint, Quaternion.identity);
        enemy.GetComponent<BarrelEnemy>().Spawn(spawnPointIn2D);

        this.lastSpawnIndex = randomIndex;
    }

}
