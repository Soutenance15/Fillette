using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireRate = 1f;
    private float nextAttackTime = 0f;

    DetectionSystem detection;

    private void Awake()
    {
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

    private void HandlePlayerDetected(Transform player)
    {
        // Vérifie que l’ennemi est orienté vers le joueur
        bool facingRight = transform.localScale.x > 0;
        if (
            (player.position.x > transform.position.x && !facingRight)
            || (player.position.x < transform.position.x && facingRight)
        )
        {
            // L’ennemi n’est pas orienté correctement → ne rien faire
            return;
        }
        if (Time.time >= nextAttackTime)
        {
            if (firePoint != null)
            {
                Shoot();
                nextAttackTime = Time.time + fireRate;
            }
        }
    }

    private void Shoot()
    {
        GameObject projectileGO = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetShooterType("Enemy");

            // Définir la direction selon le flip du personnage
            float direction = 1f;
            if (transform.localScale.x < 0)
            {
                direction = -1f;
            }
            projectile.SetDirection(direction);
        }
    }
}
