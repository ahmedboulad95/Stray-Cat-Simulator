using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSitting : EntityState
{
    public S_PlayerSitting(GameObject self, GameObject headIk) : base(self, headIk) {}

    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", false);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", true);
        animator_.SetBool("isAggressive", false);
        animator_.SetBool("isJumping", false);
    }

    protected override void HandleInput() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f)) {
            if(Input.GetKey("left shift")) {
                playerController_.SetPlayerState("Run");
            } else {
                playerController_.SetPlayerState("Walk");
            }
        }
    }
}
