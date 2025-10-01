using System;
using System.Collections;
using UnityEngine;

public class MoveEnemySystem : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 5f;
    public float maxSpeed = 5f;

    public Transform groundCheck; // Point de vérification du sol
    public float groundCheckRadius = 0.2f; // Rayon de détection du sol

    public LayerMask groundLayer; // Masque pour détecter le sol

    private bool facingRight = true; // pour savoir dans quelle direction le perso regarde

    DetectionSystem detection;

    public bool patrol;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        detection = GetComponent<DetectionSystem>();
    }

    private void OnEnable()
    {
        if (detection != null)
        {
            detection.OnPlayerDetected += HandlePlayerDetected;
        }
    }

    private void OnDisable()
    {
        if (detection != null)
        {
            detection.OnPlayerDetected -= HandlePlayerDetected;
        }
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void HandlePlayerDetected(Transform player)
    {
        float yDiff = Mathf.Abs(player.position.y - transform.position.y);

        // Vérifie que le joueur est sur la même plateforme (ou presque)
        if (yDiff > 0.5f)
            return;

        // Vérifie la direction avant de flip
        if (
            (player.position.x > transform.position.x && !facingRight)
            || (player.position.x < transform.position.x && facingRight)
        )
        {
            Flip();
            StartCoroutine(IncreaseSpeedDelay(0.5f));
        }
    }

    private IEnumerator IncreaseSpeedDelay(float delay)
    {
        speed = 1.5f;
        yield return new WaitForSeconds(delay);
        speed = maxSpeed;
    }

    void Patrol()
    {
        float targetX;

        if (facingRight)
        {
            targetX = transform.position.x + 1f;
        }
        else
        {
            targetX = transform.position.x - 1f;
        }

        Vector2 targetPos = new Vector2(targetX, transform.position.y);

        rb.position = Vector2.MoveTowards(rb.position, targetPos, speed / 10 * Time.fixedDeltaTime);

        if (!IsGround())
        {
            Flip();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (patrol)
        {
            Patrol();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
