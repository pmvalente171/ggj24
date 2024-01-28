using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IDamageable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the dropped item!");
            Destroy(this.gameObject);
        }
        else if (other.GetComponent<Enemy>() != null)
        {
            Debug.Log("Enemy picked up the item!");
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        ScoreCounter.addScore(250);
        Destroy(this.gameObject);
    }
}
    
