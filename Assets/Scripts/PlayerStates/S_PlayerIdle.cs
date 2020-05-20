using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerIdle : EntityState
{
    public S_PlayerIdle(GameObject self, GameObject headIk) : base(self, headIk) {}
    
    public override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", false);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", false);
    }

    public override void HandleOnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Enemy") {
            if(IsPlayerStrongerThanEnemy(col.gameObject)) {
                playerController_.SetPlayerState("Aggressive");
            } else {
                playerController_.SetPlayerState("Scared");
            }
        }
    }

    public override void HandleInput() {
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
