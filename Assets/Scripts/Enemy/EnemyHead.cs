using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour, IDamageable
{
    private Enemy theEnemy;

    void Start()
    {
        this.theEnemy = GetComponentInParent<Enemy>();
    }

    public virtual void TakeDamage(int damage)
    {
        this.theEnemy.Die();
    }


    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {   
            Debug.Log("HEADHSOT!!!");
            this.TakeDamage(1000);
        }
    }
}
    
