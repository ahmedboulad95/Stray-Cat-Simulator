using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : PredatorAI
{
    [SerializeField] private bool isSitting_;
    private Dictionary<string, Stat> statMap_;

    new void Start() {
        base.Start();
        BuildStatMap();
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
                state_ = stateMap["Normal"];
                state_.HandleEnemyExitCloseZone(col);
            }
        }
    }

    private void BuildStatMap() {
        statMap_ = new Dictionary<string, Stat>();
        statMap_.Add("HP", new Stat("HP", 200));
        statMap_.Add("Attack", new Stat("Attack", 10));
    }
}
