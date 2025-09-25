// UIManager.cs
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel; // Assigné dans l'inspecteur

    public void ShowPausePanel(bool show)
    {
        if (pausePanel != null)
            pausePanel.SetActive(show);
    }
}
