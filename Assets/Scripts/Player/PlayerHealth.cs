using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int initialHealth = 3;
    int currentHealth;

    void Start()
    {
        this.currentHealth = this.initialHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        this.currentHealth -= damage;
        print("Player got hit!! " + this.currentHealth + " health remaining");
        if (this.currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        print("Player died!");
        EnemySpawner.Instance.enemyCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
