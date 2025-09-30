using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float currentHealth = 50f;
    public float maxHealth = 50f;

    public event Action OnDie;

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
        OnDie?.Invoke();
    }
}
