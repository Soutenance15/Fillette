using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameObjectUIManager; // Assigner dans l'inspecteur
    private UIManager uiManager;
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (gameObjectUIManager != null)
        {
            uiManager = gameObjectUIManager.GetComponent<UIManager>();
        }
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
            PlayGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // S'abonner à l'événement
        LevelExit.OnLevelCompleted += HandleLevelCompleted;
    }

    private void OnDisable()
    {
        // Se désabonner pour éviter les fuites de mémoire
        LevelExit.OnLevelCompleted -= HandleLevelCompleted;
    }

    // Méthode appelée lorsque l'événement est déclenché
    private void HandleLevelCompleted(string nextLevelName)
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            Debug.Log("Chargement du niveau : " + nextLevelName);
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("GameManager : nextLevelName vide !");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Test");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Espace");
            if (IsPaused)
                PlayGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        uiManager.ShowPausePanel(true);
        Debug.Log("Jeu en pause");
    }

    public void PlayGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        uiManager.ShowPausePanel(false);
        Debug.Log("Jeu repris");
    }

    public void LoadNextLevel(string levelName)
    {
        // Si tu veux un petit effet de pause ou animation, tu peux le faire ici
        Time.timeScale = 1f; // Assure que le jeu n’est pas en pause
        SceneManager.LoadScene(levelName);
    }
}
