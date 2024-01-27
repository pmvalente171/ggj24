using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarrelEnemy : Enemy
{
    [SerializeField] private float spawnHeight = 15f;

    void Start()
    {
        base.health = 100;
    }

    void Spawn(Vector2 topDownPosition)
    {
        this.gameObject.SetActive(true);
        Vector3 spawnPosition = new Vector3(topDownPosition.x, spawnHeight, topDownPosition.y);
        this.transform.position = spawnPosition;
    }

    public override void Die()
    {
        Debug.Log("Die of child called..");
        base.Die();
    }

    public void TakeDamage(int damageAmount)
    {
        base.health -= damageAmount;
        if (base.health <= 0)
        {
            Die();
        }
    }
}