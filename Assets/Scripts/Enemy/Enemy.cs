using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health = 1;

    private Barks _barks;
    
    private void Awake()
    {
        _barks = FindObjectOfType<Barks>();
    }

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
        //EnemySpawner.Instance.enemyCount--; // singleton
        EnemySpawner.Instance.notifyEnemyDeath(this.gameObject);
        Destroy(this.gameObject);
        ParticleManager.PlayParticle("WoodExplosion", transform.position);
        if (Random.Range(0f, 1f) < 0.15f) _barks.Bark();
    }

    public virtual void Spawn(Vector3 position){
        this.gameObject.SetActive(true);
        Vector3 spawnPosition = position;
        this.transform.position = spawnPosition;
    }
}
