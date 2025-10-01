using System;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public static event Action TakeJetPack;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LocomotionSystem loco = other.GetComponent<LocomotionSystem>();
            if (loco != null)
            {
                if (!loco.hasJet)
                {
                    loco.hasJet = true;
                }
                loco.enableJet = true;
                loco.enableJump = false;
                TakeJetPack?.Invoke();
            }
            Destroy(gameObject);
        }
    }
}
