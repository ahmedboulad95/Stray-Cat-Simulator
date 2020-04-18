using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryState : CameraState
{
    public override void Rotate()
    {
        mousePosition_ = GetMousePosition();
        mainCamera_.transform.RotateAround(player_.transform.position, Vector3.up, mousePosition_.x);
    }
}
