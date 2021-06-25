using UnityEngine;

public class S_PlayerAggressive : S_PlayerConflict
{
    public S_PlayerAggressive(GameObject self, GameObject headIk) : base(self, headIk) {}

    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", false);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", true);
    }
}
