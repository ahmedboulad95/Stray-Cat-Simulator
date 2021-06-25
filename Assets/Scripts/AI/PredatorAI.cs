using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PredatorAI : MonoBehaviour
{
    [SerializeField] protected GameObject headIk_;
    protected Animator animator_;
    protected EntityState state_;
    protected GameObject inProximityEnemy_;
    protected Dictionary<string, EntityState> stateMap;
    protected Dictionary<string, Stat> statMap_;

    public void Start() {
        animator_ = GetComponent<Animator>();
        stateMap = BuildEnemyStateMap();
        statMap_ = BuildStatMap();
        state_ = stateMap["Idle"];
    }

    public Dictionary<string, EntityState> BuildEnemyStateMap() {
        return new Dictionary<string, EntityState> 
        {
            { "Idle", new S_EnemyIdle(gameObject, headIk_) },
            { "Scared", new S_EnemyScared(gameObject, headIk_) },
            { "Aggressive", new S_EnemyAggressive(gameObject, headIk_) }
        };
    }

    protected abstract Dictionary<string, Stat> BuildStatMap();

    public virtual int? GetStatValueByName(string statName) {
        if(string.IsNullOrEmpty(statName) || !statMap_.ContainsKey(statName)) return null;
        return statMap_[statName].GetStatValue();
    }
}
