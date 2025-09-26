using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn / Checkpoints")]
    public Transform initialSpawnPoint; // position de départ du joueur
    private Transform currentCheckpoint;

    private GameObject player;
    private HealthSystem playerHealth;
    private LocomotionSystem playerLocomotion;

    void Awake()
    {
        // On récupère le joueur
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthSystem>();
            playerLocomotion = player.GetComponent<LocomotionSystem>();

            // Position initiale
            // RespawnPlayer(initialSpawnPoint.position);
        }

        // Le checkpoint initial est le spawn
        currentCheckpoint = initialSpawnPoint;
    }

    void Update()
    {
        // if (playerHealth != null && playerHealth.currentHealth <= 0f)
        // {
        //     RespawnPlayer(currentCheckpoint.position);
        // }
    }

    // Appelé pour changer le checkpoint quand le joueur atteint un point spécifique
    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    // Respawn du joueur au checkpoint ou au spawn initial
    public void RespawnPlayer(Vector3 spawnPosition)
    {
        player.transform.position = spawnPosition;

        if (playerHealth != null)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
        }

        if (playerLocomotion != null)
        {
            playerLocomotion.currentFuel = playerLocomotion.maxFuel;
            playerLocomotion.currentJetForce = playerLocomotion.jetForceMax;
        }
    }

    // Appelé quand le joueur touche la sortie
    public void CompleteLevel(string nextLevelName)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadNextLevel(nextLevelName);
        }
        else
        {
            Debug.LogWarning("GameManager introuvable pour charger le niveau suivant !");
        }
    }
}
