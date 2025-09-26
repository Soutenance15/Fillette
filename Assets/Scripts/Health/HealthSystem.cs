using UnityEngine;

public class HealthSystem : MonoBehaviour
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
        Debug.LogWarning("DIE");
        //TODO
        // Envoie evenement Die
    }
}
