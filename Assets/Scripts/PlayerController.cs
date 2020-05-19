using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController controller;
    private Camera mainCamera;
    private Vector3 velocity;
    private Dictionary<string, Stat> statMap_;
    private Dictionary<string, EntityState> stateMap_;
    private EntityState state_;
    private GameObject inProximityEnemy_;

    public bool IsMoving { get; set; }

    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float runSpeed = 12.0f;
    [SerializeField] private float gravity = 5.0f;
    [SerializeField] private GameObject headIk_;

    [SerializeField] private GameObject bigBallOfViolencePrefab;
    private bool isFighting = false;

    void Start() {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        IsMoving = false;

        statMap_ = BuildStatMap();
        stateMap_ = BuildStateMap();
        state_ = stateMap_["Idle"];
    }

    void Update() {
        state_.SetAnimatorFlags();
        state_.HandleInput();
    }

    private void LateUpdate() {
        state_.HandleLateUpdate(inProximityEnemy_);
    }

    public Vector3 GetVelocity() {
        return velocity;
    }

    private void OnTriggerEnter(Collider hit) {
        /*if(hit.gameObject.tag == "Enemy") {
            inProximityEnemy_ = hit.gameObject;

            int? enemyAttackStat = hit.gameObject.GetComponent<PredatorAI>().GetStatValueByName("Attack");
            int? playerAttackStat = GetStatValueByName("Attack");

            if(enemyAttackStat != null && playerAttackStat != null) {
                if(enemyAttackStat > playerAttackStat) {
                    state_ = stateMap_["Scared"];
                } else {
                    state_ = stateMap_["Aggressive"];
                }
            }

            state_.HandleEnemyEnterCloseZone(hit);
        }*/
    }

    private void OnTriggerExit(Collider hit) {
        /*if(hit.gameObject.tag == "Enemy") {
            inProximityEnemy_ = null;
            state_ = stateMap_["Idle"];
            state_.HandleEnemyExitCloseZone(hit);
        }*/
    }

    private Dictionary<string, Stat> BuildStatMap() {
        Dictionary<string, Stat> statMap = new Dictionary<string, Stat>();
        
        Stat hpStat = new Stat("HP", 300);
        hpStat.AddModifier(new StatModifier(hpStat.Name, StatModifier.ModifierType.ADD, 50.0f));
        hpStat.AddModifier(new StatModifier(hpStat.Name, StatModifier.ModifierType.MULTIPLY, -0.5f));
        statMap.Add(hpStat.Name, hpStat);

        Stat attackStat = new Stat("Attack", 12);
        attackStat.AddModifier(new StatModifier(attackStat.Name, StatModifier.ModifierType.MULTIPLY, 0.5f));
        statMap.Add(attackStat.Name, attackStat);

        return statMap;
    }

    private Dictionary<string, EntityState> BuildStateMap() {
        return new Dictionary<string, EntityState> 
        {
            { "Idle", new S_PlayerIdle(gameObject, headIk_) },
            { "Walk", new S_PlayerWalk(gameObject, headIk_) },
            { "Run", new S_PlayerRun(gameObject, headIk_) },
            { "Scared", new S_PlayerScared(gameObject, headIk_) },
            { "Aggressive", new S_PlayerAggressive(gameObject, headIk_) }
        };
    }

    public int? GetStatValueByName(string statName) {
        if(string.IsNullOrEmpty(statName) || !statMap_.ContainsKey(statName)) return null;
        return statMap_[statName].GetStatValue();
    }

    public void SetPlayerState(string stateName) {
        if(string.IsNullOrEmpty(stateName)) return;

        if(stateMap_.ContainsKey(stateName)) {
            state_ = stateMap_[stateName];
        }
    }

    public bool IsInMovingState() {
        return state_.IsMovingState;
    }
}
