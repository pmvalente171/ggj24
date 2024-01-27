using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy enemy;

    public virtual void TakeDamage(int damage)
    {
        print("HEADSHOT");
        this.enemy.Die();
    }
}
    
