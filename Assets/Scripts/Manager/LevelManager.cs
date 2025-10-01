using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn / Checkpoints")]
    public Vector2 initialSpawnPoint; // position de départ du joueur
    private Transform currentCheckpoint;
    private CheckPoint[] checkpoints;

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

            // Sauvegarde de la position initiale
            initialSpawnPoint = player.transform.position;
        }

        checkpoints = Object.FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);

        // Le checkpoint initial reste null, on utilisera initialSpawnPoint si aucun checkpoint n’est touché
    }

    void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnDie += HandleDeath;

        foreach (CheckPoint checkPoint in checkpoints)
        {
            checkPoint.OnCheckpointEnter += HandleCheckPoint; // ✅ juste la référence, pas ()
        }
    }

    void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnDie -= HandleDeath;

        foreach (CheckPoint checkPoint in checkpoints)
        {
            checkPoint.OnCheckpointEnter -= HandleCheckPoint;
        }
    }

    void HandleDeath()
    {
        // si aucun checkpoint, respawn sur initialSpawnPoint
        Vector3 spawnPos =
            currentCheckpoint != null ? currentCheckpoint.position : initialSpawnPoint;
        RespawnPlayer(spawnPos);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Appelé pour changer le checkpoint quand le joueur atteint un point spécifique
    public void HandleCheckPoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
        if (playerHealth != null)
            playerHealth.currentHealth = playerHealth.maxHealth;
    }

    // Respawn du joueur au checkpoint ou au spawn initial
    public void RespawnPlayer(Vector3 spawnPosition)
    {
        player.transform.position = spawnPosition;

        if (playerHealth != null)
            playerHealth.currentHealth = playerHealth.maxHealth;

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
