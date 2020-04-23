﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryState : CameraState
{
    public override void Rotate()
    {
        base.Rotate();

        offset_ = player_.transform.position - mainCamera_.transform.position;

        mousePosition_ = GetMousePosition();
        mainCamera_.transform.RotateAround(player_.transform.position, Vector3.up, mousePosition_.x);
    }
}
