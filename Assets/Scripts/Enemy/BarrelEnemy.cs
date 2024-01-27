using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelEnemy : Enemy
{
    
    [SerializeField] private float spawnHeight = 15f;

    void Spawn(Vector2 topDownPosition){
        Vector3 spawnPosition = new Vector3(topDownPosition.x, spawnHeight, topDownPosition.y);
        this.transform.position = spawnPosition;
    }

    public override void Die(){
        Debug.Log("Die of child called..");
        base.Die();
    }
}
