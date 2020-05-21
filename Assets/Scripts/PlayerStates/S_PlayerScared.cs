using UnityEngine;

public class S_PlayerScared : S_PlayerConflict
{
    public S_PlayerScared(GameObject self, GameObject headIk) : base(self, headIk) {}

    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", false);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", true);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", false);
    }
}