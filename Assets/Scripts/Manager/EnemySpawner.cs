using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject barrelEnemySpawnParent;
    List<Transform> barrelEnemySpawnPoints = new List<Transform>();


    [SerializeField] GameObject barrelEnemyPrefab;


    void Start(){
        foreach (Transform child in this.barrelEnemySpawnParent.GetComponentsInChildren<Transform>()){
            barrelEnemySpawnPoints.Add(child);
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.LeftAlt)){
            SpawnBarrel();
        }
    }

    void SpawnBarrel(){
        int randomIndex = Random.Range(0, barrelEnemySpawnPoints.Count);
        Vector3 randomSpawnPoint = barrelEnemySpawnPoints[randomIndex].position;
        Vector2 in2D = new Vector2(randomSpawnPoint.x, randomSpawnPoint.z);
        var enemy = Instantiate(barrelEnemyPrefab, randomSpawnPoint, Quaternion.identity);
        enemy.GetComponent<BarrelEnemy>().Spawn(in2D);
    }

}
