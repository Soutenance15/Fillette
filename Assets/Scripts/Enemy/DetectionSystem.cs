using System;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    // [SerializeField]
    private float detectionRange = 3f; // portée de détection

    [SerializeField]
    private LayerMask playerLayer; // masque pour détecter le joueur

    private Transform player;

    // Event déclenché quand le joueur est détecté
    public event Action<Transform> OnPlayerDetected;

    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        // Vérifie si un joueur est dans un cercle autour de l'ennemi
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (hit != null)
        {
            player = hit.transform;
            // Ici tu peux appeler une fonction d'attaque ou suivre le joueur
            Debug.Log("Joueur détecté !");
            OnPlayerDetected?.Invoke(player); // déclenche l'event
        }
        else
        {
            player = null;
        }
    }

    // Permet à d'autres scripts de savoir si le joueur est détecté
    public bool IsPlayerDetected()
    {
        return player != null;
    }

    public Transform GetPlayer()
    {
        return player;
    }
}
