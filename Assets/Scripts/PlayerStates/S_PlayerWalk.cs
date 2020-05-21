using UnityEngine;

public class S_PlayerWalk : S_PlayerMove
{
    public S_PlayerWalk(GameObject self, GameObject headIk, float moveSpeed) : base(self, headIk, moveSpeed) {
        IsMovingState = true;
    }

    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", true);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", false);
    }

    protected override void HandleInput() {
        base.MovePlayer();

        if(Input.GetKey("left shift")) {
            playerController_.SetPlayerState("Run");
            return;
        }
    }
}
