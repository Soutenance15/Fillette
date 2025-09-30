using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private Collider2D playerCollider; // référence quand le joueur est dessus
    private float dropTime = 0.6f;

    private void Awake()
    {
        platformCollider = GetComponent<CompositeCollider2D>();
    }

    private void Update()
    {
        // Vérifie si le joueur est en contact et appuie sur S
        if (playerCollider != null && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DisableCollisionCoroutine(playerCollider, dropTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollider = collision.collider;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }

    private IEnumerator DisableCollisionCoroutine(Collider2D pCol, float duration)
    {
        Physics2D.IgnoreCollision(pCol, platformCollider, true);
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreCollision(pCol, platformCollider, false);
    }
}
