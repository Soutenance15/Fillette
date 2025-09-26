using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel; // Assigné dans l'inspecteur

    // HUD
    public LocomotionSystem playerLocomotion; // assigné dans l'inspecteur
    public Slider fuelSlider;
    public Slider jetSlider;

    void Awake()
    {
        if (playerLocomotion == null)
        {
            Debug.LogWarning("UIManager : Player LocomotionSystem non assigné !");
        }
    }

    void Update()
    {
        if (playerLocomotion != null)
        {
            fuelSlider.value = playerLocomotion.currentFuel / playerLocomotion.maxFuel;

            float normalizedJet = Mathf.InverseLerp(
                0f,
                playerLocomotion.jetSpeedMax,
                playerLocomotion.velocity.y
            ); // normalisation pour slider
            jetSlider.value = normalizedJet;
        }
    }

    public void ShowPausePanel(bool show)
    {
        if (pausePanel != null)
            pausePanel.SetActive(show);
    }
}
