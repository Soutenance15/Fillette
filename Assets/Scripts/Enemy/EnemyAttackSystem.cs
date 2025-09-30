using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireRate = 0.5f;
    private float nextFireTime = 0f;

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            // Shoot();
            nextFireTime = Time.time + fireRate;
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
