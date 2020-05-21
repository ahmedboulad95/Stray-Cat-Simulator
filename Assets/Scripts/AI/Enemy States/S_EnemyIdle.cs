using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyIdle : EntityState
{
    public S_EnemyIdle(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleLateUpdate() {
        base.HandleLateUpdate();
    }

    public override void HandleEnemyEnterCloseZone(Collider col) {
        animator_.SetBool("isScared", false);
        animator_.SetBool("isAggressive", false);
    }

    public override void HandleEnemyExitCloseZone(Collider col) {
        animator_.SetBool("isScared", false);
        animator_.SetBool("isAggressive", false);
    }
}
