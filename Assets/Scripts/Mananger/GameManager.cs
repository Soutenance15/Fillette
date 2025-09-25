using UnityEngine;

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
            DontDestroyOnLoad(gameObject);
            PlayGame();
        }
        else
        {
            Destroy(gameObject);
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
}
