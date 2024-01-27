using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField] private int health = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
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
        Destroy(this.gameObject);
    }
}
