using UnityEngine;

public class FuelJet : MonoBehaviour
{
    public float amountFuel = 50f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LocomotionSystem loco = other.GetComponent<LocomotionSystem>();
            if (loco != null)
            {
                loco.FillFuelJet(amountFuel);
            }

            Destroy(gameObject);
        }
    }
}
