using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarrelEnemy : Enemy
{
    [SerializeField] private float spawnHeight = 15f;
    [SerializeField] private GameObject itemToDrop;
    [SerializeField] private float likelihoodOfDrop = 0.5f;

    public void Spawn(Vector2 topDownPosition)
    {
        this.gameObject.SetActive(true);
        Vector3 spawnPosition = new Vector3(topDownPosition.x, spawnHeight, topDownPosition.y);
        this.transform.position = spawnPosition;
    }

    public override void Die()
    {
        Debug.Log("Die of barrel called..");
        if (this.itemToDrop != null) {
            float random = Random.Range(0f, 1f);
            if (random <= this.likelihoodOfDrop){
                Instantiate(itemToDrop, this.transform.position, Quaternion.identity);
            }
        }
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