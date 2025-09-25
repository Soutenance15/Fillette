using System;
using UnityEngine;

public class LocomotionSystem : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Jump Settings")]
    public bool enableJump = true;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundLayerMask;

    [Header("Jet Settings")]
    public bool enableJet = true;
    private float jetForce = 0.05f;
    private float maxFuel = 12f;
    private float currentFuel;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;
    }

    void Update()
    {
        if (enableJump && Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (enableJet && Input.GetKey(KeyCode.UpArrow) && currentFuel > 0f)
        {
            Jet();
        }
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.y < -10f || rb.linearVelocity.y > 10f)
        {
            LimitVelocity();
        }
    }

    void LimitVelocity()
    {
        // La vitesse verticale ne doit pas depasser 10f ou -10f
        // des problèmes liés a la physiqye peuvent survenir comme
        // un effet d'ecrasement
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -10f, 10f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Jet()
    {
        rb.AddForce(Vector2.up * jetForce, ForceMode2D.Impulse);
        currentFuel -= Time.deltaTime;
    }

    public void FillFuelJet(float amountFuel)
    {
        currentFuel += amountFuel;
        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundDistance,
            groundLayerMask
        );
    }
}
