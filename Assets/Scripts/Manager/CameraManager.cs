using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera normalCam;
    public CinemachineCamera panoramicCam;

    private bool isNormal = true;

    void Start()
    {
        // Active la caméra normale au lancement
        normalCam.Priority = 10;
        panoramicCam.Priority = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // touche C pour switcher
        {
            isNormal = !isNormal;

            if (isNormal)
            {
                normalCam.Priority = 10;
                panoramicCam.Priority = 0;
            }
            else
            {
                normalCam.Priority = 0;
                panoramicCam.Priority = 10;
            }
        }
    }
}
