using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : PredatorAI
{
    [SerializeField] private bool isSitting_;

    new void Start() {
        base.Start();
    }

    void Update() {
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

                int attackStatValue = statMap_["Attack"].GetStatValue();
                int? playerAttackValue = col.gameObject.GetComponent<PlayerController>().GetStatValueByName("Attack");

                if(playerAttackValue != null) {
                    if(playerAttackValue > attackStatValue) {
                        state_ = stateMap["Scared"];
                    } else {
                        state_ = stateMap["Aggressive"];
                    }
                }

                state_.HandleEnemyEnterCloseZone(col);
            }
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player") {
            if(!isSitting_) {
                inProximityEnemy_ = null;
                state_ = stateMap["Idle"];
                state_.HandleEnemyExitCloseZone(col);
            }
        }
    }

    protected override Dictionary<string, Stat> BuildStatMap() {
        Dictionary<string, Stat> statMap = new Dictionary<string, Stat>();
        statMap.Add("HP", new Stat("HP", 200));
        statMap.Add("Attack", new Stat("Attack", 10));
        return statMap;
    }
}
