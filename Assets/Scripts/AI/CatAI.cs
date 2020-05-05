using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : PredatorAI
{
    void Update()
    {
        
    }

    private void LateUpdate() {
        state_.HandleLateUpdate(inProximityEnemy_);
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player") {
            inProximityEnemy_ = col.gameObject;
            state_ = stateMap["Scared"];
            state_.HandleEnemyEnterCloseZone(col);
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player") {
            inProximityEnemy_ = null;
            state_ = stateMap["Normal"];
            state_.HandleEnemyExitCloseZone(col);
        }
    }
}
