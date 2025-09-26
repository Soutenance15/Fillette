using System;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    // Événement déclenché lorsqu'un joueur atteint la sortie
    // Le string correspond au nom du prochain niveau
    public static event Action<string> OnLevelCompleted;

    [Header("Nom du niveau suivant")]
    public string nextLevelName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(nextLevelName))
            {
                // Déclenche l'événement
                OnLevelCompleted?.Invoke(nextLevelName);
            }
            else
            {
                Debug.LogWarning("LevelExit : nextLevelName vide !");
            }
        }
    }
}
