using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int initialHealth = 3;
    public int currentHealth;

    [SerializeField] private float invincibilityDuration = 1f; // Duration of invincibility in seconds
    private bool isInvincible = false;

    void Start()
    {
        this.currentHealth = this.initialHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if (isInvincible) return;

        this.currentHealth -= damage;
        print("Player got hit!! " + this.currentHealth + " health remaining");
        if (this.currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    public virtual void Die()
    {
        print("Player died!");
        EnemySpawner.Instance.enemyCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
