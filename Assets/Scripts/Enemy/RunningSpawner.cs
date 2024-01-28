using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSpawner : MonoBehaviour, SpawnLocator
{

    [SerializeField] private Transform leftTopLeft;

    [SerializeField] private Transform leftBottomRight;

    [SerializeField] private Transform rightTopLeft;

    [SerializeField] private Transform rightBottomRight;

    [SerializeField] private GameObject runningEnemyPrefab;

    [SerializeField] float spawnHeight = 2;

    private float[] left, right, top, bottom;

    void Start()
    {
        left = new float[] {leftTopLeft.position.x, rightTopLeft.position.x};
        right = new float[] {leftBottomRight.position.x, rightBottomRight.position.x};
        top = new float[] {leftTopLeft.position.z, rightTopLeft.position.z};
        bottom = new float[] {leftBottomRight.position.x, rightBottomRight.position.x};
    }

    public Vector3 ComputeSpawnLocation(float playerAdvance){
        int index = Random.Range(0, 2);

        float x = Random.Range(left[index], right[index]);
        float z = playerAdvance + Random.Range(top[index], bottom[index]);
        return new Vector3(x, spawnHeight, z);
    }

    public GameObject GetPrefab(){
        return runningEnemyPrefab;
    }
}
