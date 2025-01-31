﻿using UnityEngine;

public class S_EnemyAggressive : EntityState
{
    public S_EnemyAggressive(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleLateUpdate() {
        base.HandleLateUpdate();
        LookAtInProximityEnemy();
    }

    public override void HandleEnemyEnterCloseZone(Collider col) {
        animator_.SetBool("isAggressive", true);
    }

    public override void HandleEnemyExitCloseZone(Collider col) {
        animator_.SetBool("isAggressive", false);
    }
}
