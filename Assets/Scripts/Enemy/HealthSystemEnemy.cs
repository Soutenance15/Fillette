using UnityEngine;

public class HealthSystemEnemy : MonoBehaviour
{
    public float currentHealth = 50f;
    public float maxHealth = 50f;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
