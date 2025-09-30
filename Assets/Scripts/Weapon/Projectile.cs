using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float damage = 12.5f;

    [SerializeField]
    private float lifetime = 3f;

    public string shooterType;

    private float direction = 1f; // 1 = droite, -1 = gauche

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Projectile");
        Destroy(gameObject, lifetime); // auto-destruction apres lifetime (si l'objet n'a rien touché)
    }

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        pos.x += speed * direction * Time.deltaTime;
        transform.position = pos;
    }

    public void SetShooterType(string type)
    {
        shooterType = type;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // bool isPlayerCollided = other.CompareTag("Player");
        // bool isEnemyCollided = other.CompareTag("Enemy");

        Debug.Log(other);

        if (shooterType == "Player" && other.GetComponent<GunnerEnemy>() != null)
        {
            GunnerEnemy enemy = other.GetComponent<GunnerEnemy>();
            HealthSystemEnemy heathEnemy = enemy.GetComponent<HealthSystemEnemy>();
            if (heathEnemy != null)
            {
                heathEnemy.TakeDamage(damage);
                Destroy(gameObject); // le projectile disparaît
            }
        }
        else if (shooterType == "Enemy" && other.GetComponent<PlayerController>() != null)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            HealthSystem healthPlayer = player.GetComponent<HealthSystem>();
            if (healthPlayer != null)
            {
                healthPlayer.TakeDamage(damage);
                Destroy(gameObject); // le projectile disparaît
            }
        }
    }
}
