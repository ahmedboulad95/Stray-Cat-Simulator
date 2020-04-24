using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : CameraState
{
    public override void Rotate()
    {
        float vertical = Input.GetAxisRaw("Vertical") * mainCamera_.transform.forward.z;
        float horizontal = Input.GetAxisRaw("Horizontal") * mainCamera_.transform.right.x;

        Vector3 desiredMoveDirection = Input.GetAxisRaw("Vertical") * mainCamera_.transform.forward + Input.GetAxisRaw("Horizontal") * mainCamera_.transform.right;
        desiredMoveDirection.y = 0.0f;

        player_.transform.LookAt(desiredMoveDirection + player_.transform.position);
    }
}
