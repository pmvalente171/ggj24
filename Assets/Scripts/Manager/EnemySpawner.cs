using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // list of vector3
    [SerializeField] List<Vector3> spawnPoints = new List<Vector3>();

    [SerializeField] GameObject barrelEnemyPrefab;

    void Spawn(){
        // spawn enemy at random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Vector3 randomSpawnPoint = spawnPoints[randomIndex];
        Vector2 in2D = new Vector2(randomSpawnPoint.x, randomSpawnPoint.z);
        var a = Instantiate(barrelEnemyPrefab, randomSpawnPoint, Quaternion.identity);
        

        a.GetComponent<BarrelEnemy>().Spawn(in2D);
    }

}
