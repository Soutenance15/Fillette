using UnityEngine;

public class MovePlayerSystem : MonoBehaviour
{
    private float speed = 5f;
    private float acceleration = 50f;

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
    }
}
