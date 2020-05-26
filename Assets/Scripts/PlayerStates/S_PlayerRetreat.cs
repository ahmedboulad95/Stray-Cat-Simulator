using UnityEngine;

public class S_PlayerRetreat : EntityState
{
    private bool isWalking_ = false;

    public S_PlayerRetreat(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleStateSet() {
        base.HandleStateSet();
        isWalking_ = false;
    }

    public override void DoRotate() {
        self_.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
    }

    public override void NotifyAnimationDone() {
        Debug.Log("Jump animation complete");
        isWalking_ = true;
        animator_.SetBool("isJumping", false);
    }

    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", false);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", false);
        animator_.SetBool("isJumping", true);
    }

    public override void HandleUpdate() {
        base.HandleUpdate();
        Debug.Log("In retreat update");
        if(isWalking_) {
            Debug.Log("Walking away");
            animator_.SetBool("isWalking", true);
            Vector3 velocity = -self_.transform.up * gravity_ + self_.transform.forward;
            controller_.Move(velocity * 6.0f * Time.deltaTime);
        }
    }

    public override void HandleOnTriggerExit(Collider col) {
        base.HandleOnTriggerExit(col);
        playerController_.SetPlayerState("Idle");
    }
}
