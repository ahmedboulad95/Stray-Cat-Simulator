using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : CameraState
{
    public override void Rotate()
    {
        base.Rotate();

        Debug.Log("Offset :: " + offset_);

        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed_;

        offset_ = Quaternion.AngleAxis(horizontal, Vector3.up) * offset_;
        player_.transform.Rotate(Vector3.up * horizontal);

        mainCamera_.transform.position = player_.transform.position - offset_;

        mainCamera_.transform.LookAt(player_.transform.position);
    }
}
