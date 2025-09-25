using UnityEngine;

public class MovePlayerSystem : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 velocity;

    void Update()
    {
        // Mouvement horizontal
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocity.y);
    }
}
