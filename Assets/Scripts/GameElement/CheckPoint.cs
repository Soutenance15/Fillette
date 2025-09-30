using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // public LevelManager levelManager;
    public event System.Action<Transform> OnCheckpointEnter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        OnCheckpointEnter?.Invoke(transform); // 🔹 on passe la référence du checkpoint
        // levelManager.SetCheckpoint(transform);
    }
}
