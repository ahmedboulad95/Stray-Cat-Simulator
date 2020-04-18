using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : CameraState
{
    public override void Rotate()
    {
        mousePosition_ = GetMousePosition();
        player_.transform.Rotate(Vector3.up * mousePosition_.x);
    }
}
