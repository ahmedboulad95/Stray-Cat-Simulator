using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAI : MonoBehaviour
{
    [SerializeField] protected GameObject headIk_;
    protected Animator animator_;
    protected EnemyState state_;
    protected GameObject inProximityEnemy_;
    public Dictionary<string, EnemyState> stateMap;

    void Start() {
        animator_ = GetComponent<Animator>();
        stateMap = BuildEnemyStateMap();
        state_ = stateMap["Normal"];
    }

    public Dictionary<string, EnemyState> BuildEnemyStateMap() {
        return new Dictionary<string, EnemyState> 
        {
            { "Normal", new NormalState(gameObject, headIk_) },
            { "Scared", new ScaredState(gameObject, headIk_) }
        };
    }
}
