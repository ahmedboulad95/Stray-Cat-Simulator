using UnityEngine;

public class S_PlayerRetreat : EntityState
{
    public S_PlayerRetreat(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleStateSet() {
        base.HandleStateSet();
        self_.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
    }

    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", true);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", false);
    }

    public override void HandleUpdate() {
        base.HandleUpdate();

        Vector3 velocity = -self_.transform.up * gravity_ + self_.transform.forward;
        controller_.Move(velocity * 6.0f * Time.deltaTime);
    }

    public override void HandleOnTriggerExit(Collider col) {
        base.HandleOnTriggerExit(col);
        playerController_.SetPlayerState("Idle");
    }
}
