using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerCon;
    private CameraState cameraState;
    private Dictionary<string, CameraState> cameraStateMap;
    private Camera mainCamera;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerCon = player.GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainCamera = Camera.main;
        BuildStateMap();
        cameraState = cameraStateMap["STATIONARY"];
    }

    void Update()
    {
        Quaternion targetRotation = new Quaternion(mainCamera.transform.rotation.x, 0.0f, mainCamera.transform.rotation.z, 1);

        if (playerCon.IsInMovingState())
        {
            cameraState = cameraStateMap["MOVING"];
        }
        else
        {
            cameraState = cameraStateMap["STATIONARY"];
        }
    }

    void LateUpdate()
    {
        cameraState.Rotate();
    }

    private void BuildStateMap()
    {
        cameraStateMap = new Dictionary<string, CameraState>
        {
            { "MOVING", new S_CameraMoving() },
            { "STATIONARY", new S_CameraStationary() }
        };
    }

}