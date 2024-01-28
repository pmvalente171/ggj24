using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;

public class EnemySpawner : GenericSingletonClass<EnemySpawner>
{

    [SerializeField] Transform topLeft;

    [SerializeField] Transform bottomRight;

    [SerializeField] Transform playerPos;
    
    public int enemyCount = 0;
    [SerializeField] int maxEnemyCount = 5;

    [SerializeField] float numEnemiesIntervalImpact = 0.9f;

    [SerializeField] private float baseSpawnInterval = 5f;

    private SpawnLocator[] spawnLocators;

    private float left, right, top, bottom;

    private Dictionary<int, Transform> enemies = new Dictionary<int, Transform>();

    void Start()
    {
        left = topLeft.position.x;
        right = bottomRight.position.x;
        top = topLeft.position.z;
        bottom = bottomRight.position.z;
        SpawnLocator barrelSpawner = GetComponent<BarrelSpawner>();
        SpawnLocator runningSpawner = GetComponent<RunningSpawner>();
        SpawnLocator flyingSpawner = GetComponent<FlyingSpawner>();
        spawnLocators = new SpawnLocator[] {barrelSpawner, runningSpawner, flyingSpawner};
        
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {

        while (true)
        {
            float timer = computeNextSpawnTime();
            print(this.enemyCount + " enemies " + timer);
            yield return new WaitForSeconds(timer);
            if (this.enemyCount < this.maxEnemyCount){
                Spawn();
            }
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Y) && (this.enemyCount < this.maxEnemyCount)){
            Spawn();
        }
    }

    void Spawn(){
        float[] probabilities = {0.75f, 0.9f, 1f};
        float random = Random.Range(0f, 1f);
        int index = 0;
        while (random > probabilities[index]){
            index++;
        }
        this.enemyCount++;
        float playerAdvance = playerPos.position.z;
        Vector3 randomSpawnPoint;
        int tries = 3;
        do {
            randomSpawnPoint = spawnLocators[index].ComputeSpawnLocation(playerAdvance);
            tries--;
        } while(isFarFromOtherEnemies(randomSpawnPoint) && tries > 0);
        //GameObject enemy = Instantiate(barrelEnemyPrefab, randomSpawnPoint, Quaternion.identity);
        GameObject enemy = Instantiate(spawnLocators[index].GetPrefab(), randomSpawnPoint, Quaternion.identity);
        enemies.Add(enemy.GetInstanceID(), enemy.transform);
        enemy.GetComponent<Enemy>().Spawn(randomSpawnPoint);
    }

    public bool isFarFromOtherEnemies(Vector3 spawnPoint) {
        float thresholdDistance = 7f;
        foreach (Transform enemy in enemies.Values) {
            float distance = Vector3.Distance(spawnPoint, enemy.position);
            if (distance < thresholdDistance) {
                return false;
            }
        }
        return true;
    }

    private float computeNextSpawnTime(){
        float timer = baseSpawnInterval * Mathf.Pow(numEnemiesIntervalImpact, (maxEnemyCount - enemyCount) - 1);
        Debug.Log("Next spawn time is " + timer);
        return timer;
    }

    public void notifyEnemyDeath(GameObject enemy){
        this.enemyCount--;
        this.enemies.Remove(enemy.GetInstanceID());
    }

}
