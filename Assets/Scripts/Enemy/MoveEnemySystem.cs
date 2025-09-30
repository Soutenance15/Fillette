using UnityEngine;

public class MoveEnemySystem : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 5f;

    public Transform groundCheck; // Point de vérification du sol
    public float groundCheckRadius = 0.2f; // Rayon de détection du sol

    public LayerMask groundLayer; // Masque pour détecter le sol

    private bool facingRight = true; // pour savoir dans quelle direction le perso regarde

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        bool onGround = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
        if (!onGround)
        {
            Flip();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
