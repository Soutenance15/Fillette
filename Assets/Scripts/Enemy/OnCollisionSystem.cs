using UnityEngine;

public class OnCollisionSystem : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector2 pushDir = collision.transform.position - transform.position;
                player.movePlayerSystem.PushMe(pushDir, 12f);
                // player.healthSystem.TakeDamage(12.5f);
            }
        }
    }
}
