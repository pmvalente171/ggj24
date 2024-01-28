using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageable
{
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);
            print("Bullet hit something that can take damage!");
        }
        Destroy(this.gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        ScoreCounter.addScore(500);
        Destroy(this.gameObject);
    }

}
