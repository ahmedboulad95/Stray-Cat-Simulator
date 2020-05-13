using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : PredatorAI
{
    [SerializeField] bool isSitting_;

    void Update()
    {
        if(isSitting_) {
            animator_.SetBool("isSitting", true);
        }
    }

    private void LateUpdate() {
        state_.HandleLateUpdate(inProximityEnemy_);
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player") {
            if(!isSitting_) {
                inProximityEnemy_ = col.gameObject;
                state_ = stateMap["Scared"];
                state_.HandleEnemyEnterCloseZone(col);
            }
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player") {
            if(!isSitting_) {
                inProximityEnemy_ = null;
                state_ = stateMap["Normal"];
                state_.HandleEnemyExitCloseZone(col);
            }
        }
    }
}
