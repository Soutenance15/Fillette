using System.Collections;
using UnityEngine;

public class FuelJet : MonoBehaviour
{
    public float amountFuel = 50f;
    public float disableDuration = 8f; // durée pendant laquelle le GameObject est désactivé

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LocomotionSystem loco = other.GetComponent<LocomotionSystem>();
            if (loco != null)
            {
                loco.FillFuelJet(amountFuel);
            }
            StartCoroutine(DisableRoutine());
        }
    }

    private IEnumerator DisableRoutine()
    {
        gameObject.SetActive(false); // désactive le GameObject
        yield return new WaitForSeconds(disableDuration); // attend
        gameObject.SetActive(true); // réactive le GameObject
    }
}
