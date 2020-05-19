using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerRun : EntityState
{
    private float runSpeed_ = 12.0f;

    public S_PlayerRun(GameObject self, GameObject headIk) : base(self, headIk) {
        IsMovingState = true;
    }

    public override void SetAnimatorFlags() {
        animator_.SetBool("isRunning", true);
        animator_.SetBool("isWalking", false);
    }

    public override void HandleInput() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f)) {
            if(!Input.GetKey("left shift")) {
                playerController_.SetPlayerState("Walk");
                return;
            }

            Vector3 velocity = mainCamera_.transform.right * x + -self_.transform.up * gravity_ + mainCamera_.transform.forward * z;
            controller_.Move(velocity * runSpeed_ * Time.deltaTime);
        } else {
            playerController_.SetPlayerState("Idle");
        }
    }
}
