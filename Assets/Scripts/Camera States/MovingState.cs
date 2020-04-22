using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : CameraState
{
    public override void Rotate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed_;
        player_.transform.Rotate(Vector3.up * horizontal);
        mainCamera_.transform.Rotate(Vector3.up * horizontal);

        float desiredAngle = mainCamera_.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        mainCamera_.transform.position = player_.transform.position - (rotation * offset_);

        mainCamera_.transform.LookAt(player_.transform.position);
    }
}
