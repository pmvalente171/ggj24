using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour, SpawnLocator
{

    [SerializeField] private Transform topLeft;

    [SerializeField] private Transform bottomRight;

    [SerializeField] private GameObject barrelEnemyPrefab;

    [SerializeField] float spawnHeight = 15f;

    private float left, right, top, bottom;

    void Start()
    {
        left = topLeft.position.x;
        right = bottomRight.position.x;
        top = topLeft.position.z;
        bottom = bottomRight.position.z;
    }
    
    public Vector3 ComputeSpawnLocation(float playerAdvance){
        float x = Random.Range(left, right);
        float z = playerAdvance + Random.Range(top, bottom);
        return new Vector3(x, spawnHeight, z);
    }

    public GameObject GetPrefab(){
        return barrelEnemyPrefab;
    }

}
