using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField] protected int health = 1;

    public virtual void TakeDamage(int damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Enemy died!");
        EnemySpawner.Instance.enemyCount--; // singleton
        Destroy(this.gameObject);
    }

    public virtual void Spawn(Vector2 topDownPosition){
        this.gameObject.SetActive(true);
        Vector3 spawnPosition = new Vector3(topDownPosition.x, 1, topDownPosition.y);
        this.transform.position = spawnPosition;
    }
}
