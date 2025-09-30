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
    public float jetForceMax = 0.25f; // poussée max au début
    public float jetForceMin = 0.05f; // poussée minimale (fin de poussée)
    private float jetDecayNormalSpeed = 2f; // vitesse de croissance/decroissance
    private float jetDecayControlSpeed = 1 / 20f; // vitesse de croissance/decroissance
    public float jetStableForce = 0.1f; // poussée constante si Ctrl
    public float jetRecoverySpeed = 0.5f; // vitesse de recharge du jet
    public float maxFuel = 12f;
    public float jetSpeedMax = 10f;
    public float currentFuel;
    public float currentJetForce;
    public Vector2 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;
        currentJetForce = jetForceMax;
    }

    void Update()
    {
        // Jump
        if (enableJump && Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        // Jet
        if (enableJet && Input.GetKey(KeyCode.UpArrow) && currentFuel > 0f)
        {
            Jet();
            // Ici tu peux réduire currentJetForce si tu veux (diminution progressive à chaque frame)
        }
        else
        {
            // Recharge progressive du jet quand la touche est relâchée
            currentJetForce = Mathf.MoveTowards(
                currentJetForce,
                jetForceMax,
                jetRecoverySpeed * Time.deltaTime
            );
        }
        if (Input.GetKeyDown(KeyCode.S)) // ou "DownArrow"
        {
            // Détecter la plateforme sous le joueur
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                1f,
                LayerMask.GetMask("OneWayPlatform")
            );
            // if (hit.collider != null)
            // {
            //     OneWayPlatform platform = hit.collider.GetComponent<OneWayPlatform>();
            //     if (platform != null)
            //     {
            //         platform.DisableCollisionTemporarily(playerCollider, dropTime);
            //     }
            // }
        }
        velocity = rb.linearVelocity;
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.y < -jetSpeedMax || rb.linearVelocity.y > jetSpeedMax)
        {
            LimitVelocity();
        }
    }

    void LimitVelocity()
    {
        // on assure que l'on prend une valeur entre -jetSpeedMax et  jetSpeedMax pour la velocity.y
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -jetSpeedMax, jetSpeedMax);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Jet()
    {
        // Mode Ctrl : faible poussée mais visible
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // currentJetForce = jetStableForce;
            currentJetForce = Mathf.MoveTowards(
                currentJetForce,
                jetForceMin,
                1 / jetDecayControlSpeed * Time.deltaTime
            );
            // Consommation du fuel
            // Ici on multiplie par 10 car la diminuetion serait trop faible sinon
            currentFuel -= Time.deltaTime * jetDecayControlSpeed * 10;
        }
        else
        {
            // Jet normal
            // décroissance progressive
            currentJetForce = Mathf.MoveTowards(
                currentJetForce,
                jetForceMin,
                1 / jetDecayNormalSpeed * Time.deltaTime
            );
            // Consommation du fuel
            currentFuel -= Time.deltaTime * jetDecayNormalSpeed;
        }
        rb.AddForce(Vector2.up * currentJetForce, ForceMode2D.Impulse);
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
