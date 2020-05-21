using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyScared : EntityState
{
    public S_EnemyScared(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleLateUpdate() {
        base.HandleLateUpdate();
        LookAtInProximityEnemy();
    }

    public override void HandleEnemyEnterCloseZone(Collider col) {
        animator_.SetBool("isScared", true);
    }

    public override void HandleEnemyExitCloseZone(Collider col) {
        animator_.SetBool("isScared", false);
    }
}
