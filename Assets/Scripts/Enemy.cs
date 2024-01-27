using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int health = 1;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            Die();
        }
        Debug.Log("Enemy took " + damage + " damage! It has " + this.health + " health left!!");
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(this.gameObject);
    }
}
