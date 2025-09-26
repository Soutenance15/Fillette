using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float hazardDamage = 10f;

    public float damageCooldown = 1f; // une fois par seconde

    private float lastDamageTime;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.GetComponent<HealthSystem>();
            playerHealth.TakeDamage(hazardDamage);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                HealthSystem playerHealth = collision.GetComponent<HealthSystem>();
                playerHealth.TakeDamage(hazardDamage);
                lastDamageTime = Time.time;
            }
        }
    }
}
