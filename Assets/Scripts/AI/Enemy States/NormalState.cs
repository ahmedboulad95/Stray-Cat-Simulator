using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : EnemyState
{
    public NormalState(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleLateUpdate(GameObject inProximityEnemy) {
        
    }

    public override void HandleEnemyEnterCloseZone(Collider col) {
        animator_.SetBool("isScared", false);
    }

    public override void HandleEnemyExitCloseZone(Collider col) {
        animator_.SetBool("isScared", false);
    }
}
