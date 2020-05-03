using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected GameObject self_;
    protected Animator animator_;
    protected GameObject headIk_;

    public EnemyState(GameObject self, GameObject headIk) {
        self_ = self;
        headIk_ = headIk;
        animator_ = self_.GetComponent<Animator>();
    }

    public abstract void HandleLateUpdate(GameObject inProximityEnemy);

    public abstract void HandleEnemyEnterCloseZone(Collider col);

    public abstract void HandleEnemyExitCloseZone(Collider col);

    protected void LookAtInProximityEnemy(GameObject inProximityEnemy) {
        if(inProximityEnemy != null) {
            Vector3 direction = (inProximityEnemy.transform.position - headIk_.transform.position).normalized;
            headIk_.transform.rotation = Quaternion.LookRotation(headIk_.transform.forward, direction);
        }
    }
}
