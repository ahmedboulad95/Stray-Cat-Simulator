using UnityEngine;

public class S_PlayerRun : EntityState
{
    public S_PlayerRun(GameObject self, GameObject headIk, float moveSpeed) : base(self, headIk) {
        moveSpeed_ = MAX_SPEED_;
        IsMovingState = true;
    }

    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", false);
        animator_.SetBool("isRunning", true);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", false);
    }

    public override void HandleOnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Enemy") {
            base.HandleOnTriggerEnter(col);
            base.HandleEnemyEncounter(col.gameObject);
        }
    }

    protected override void HandleInput() {
        base.HandleInput();

        if(moveDirection_ == Vector3.zero) {
            playerController_.SetPlayerState("Idle");
            return;
        } else if(!Input.GetKey("left shift")) {
            playerController_.SetPlayerState("Walk");
            return;
        }
    }
}
