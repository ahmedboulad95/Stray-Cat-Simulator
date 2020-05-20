using UnityEngine;

public class S_PlayerWalk : EntityState
{
    private float walkSpeed_ = 6.0f;

    public S_PlayerWalk(GameObject self, GameObject headIk) : base(self, headIk) {
        IsMovingState = true;
    }

    public override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", true);
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
                return;
            }

            Vector3 velocity = mainCamera_.transform.right * x + -self_.transform.up * gravity_ + mainCamera_.transform.forward * z;
            controller_.Move(velocity * walkSpeed_ * Time.deltaTime);
        } else {
            playerController_.SetPlayerState("Idle");
        }
    }
}
