using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovePlayerSystem movePlayerSystem;
    public HealthSystem healthSystem;

    void Awake()
    {
        movePlayerSystem = GetComponent<MovePlayerSystem>();
        healthSystem = GetComponent<HealthSystem>();
        // if (movePlayerSystem != null)
        // {
        //     Debug.Log("movePlayerSystem exists");
        // }


    }
}
