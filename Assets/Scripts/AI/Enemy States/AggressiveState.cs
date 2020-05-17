using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveState : EnemyState
{
    public AggressiveState(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleLateUpdate(GameObject inProximityEnemy) {
        LookAtInProximityEnemy(inProximityEnemy);
    }

    public override void HandleEnemyEnterCloseZone(Collider col) {
        animator_.SetBool("isAggressive", true);
    }

    public override void HandleEnemyExitCloseZone(Collider col) {
        animator_.SetBool("isAggressive", false);
    }
}
