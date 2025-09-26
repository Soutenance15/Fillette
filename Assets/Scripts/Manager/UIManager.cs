using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel; // Assigné dans l'inspecteur

    // HUD
    [Header("Locomotion")]
    public LocomotionSystem playerLocomotion; // assigné dans l'inspecteur
    public Slider fuelSlider;
    public Slider jetSlider;

    [Header("Health")]
    public HealthSystem playerHealth; // assigné dans l'inspecteur
    public Slider healthSlider;

    void Awake()
    {
        if (playerLocomotion == null)
        {
            Debug.LogWarning("UIManager : Player LocomotionSystem non assigné !");
        }
        if (playerHealth == null)
        {
            Debug.LogWarning("UIManager : Player HealthSystem non assigné !");
        }
    }

    void Update()
    {
        // Locomotion
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

        // Health
        if (playerHealth != null)
        {
            Debug.Log("Health:" + playerHealth.currentHealth);
            float normalizedHealth = playerHealth.currentHealth / playerHealth.maxHealth;
            healthSlider.value = normalizedHealth;
        }
    }

    public void ShowPausePanel(bool show)
    {
        if (pausePanel != null)
            pausePanel.SetActive(show);
    }
}
