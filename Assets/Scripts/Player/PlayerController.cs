using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovePlayerSystem movePlayerSystem;

    void Awake()
    {
        movePlayerSystem = GetComponent<MovePlayerSystem>();
        if (movePlayerSystem != null)
        {
            Debug.Log("movePlayerSystem exists");
        }
    }
}
