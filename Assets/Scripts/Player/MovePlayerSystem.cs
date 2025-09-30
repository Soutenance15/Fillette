using UnityEngine;

public class MovePlayerSystem : MonoBehaviour
{
    private float speed = 5f;
    private float acceleration = 50f;

    private bool facingRight = true; // pour savoir dans quelle direction le perso regarde

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal"); // -1 → 1 avec progressivité

        rb.linearVelocity = new Vector2(
            Mathf.MoveTowards(
                rb.linearVelocity.x,
                inputX * speed,
                acceleration * Time.fixedDeltaTime
            ),
            rb.linearVelocity.y
        );

        // Flip si on change de direction
        if (inputX > 0 && !facingRight)
            Flip();
        else if (inputX < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void PushMe(Vector2 fromDirection, float pushForce)
    {
        // Applique une force dans la direction opposée à l'ennemi
        rb.AddForce(fromDirection.normalized * pushForce, ForceMode2D.Impulse);
    }
}
