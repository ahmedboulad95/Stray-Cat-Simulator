using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerCon;
    private CameraState cameraState;
    private Dictionary<string, CameraState> cameraStateMap;
    private Camera mainCamera;

    void Start()
    {
        playerCon = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        BuildStateMap();
        cameraState = cameraStateMap["STATIONARY"];
    }

    void Update()
    {
        Quaternion targetRotation = new Quaternion(mainCamera.transform.rotation.x, 0.0f, mainCamera.transform.rotation.z, 1);

        if (playerCon.IsMoving && Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            //Debug.Log("Still returning to start");
            Debug.Log(mainCamera.transform.rotation.y);
            cameraState.ReturnToStart();
        }
        else
        {
            if (playerCon.IsMoving)
            {
                cameraState = cameraStateMap["MOVING"];
            }
            else
            {
                cameraState = cameraStateMap["STATIONARY"];
            }

            cameraState.Rotate();
        }
    }

    private void BuildStateMap()
    {
        cameraStateMap = new Dictionary<string, CameraState>();
        cameraStateMap.Add("MOVING", new MovingState());
        cameraStateMap.Add("STATIONARY", new StationaryState());
    }

}