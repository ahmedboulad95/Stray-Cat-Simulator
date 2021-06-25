using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Dictionary<string, Stat> statMap_;
    private Dictionary<string, EntityState> stateMap_;
    private EntityState state_;
    private GameObject inProximityEnemy_;
    
    private Animator animator_;
    private bool isJumping_;
    private CharacterController controller_;
    private bool isHalfwayThroughJump_ = false;
    private TimeTracker timeTracker_;

    [SerializeField] private GameObject headIk_;
    [SerializeField] private GameObject bigBallOfViolencePrefab_;

    [SerializeField] private float walkSpeed_ = 6.0f;
    [SerializeField] private float runSpeed_ = 12.0f;

    void Start() {
        animator_ = GetComponent<Animator>();
        controller_ = GetComponent<CharacterController>();
        timeTracker_ = GetComponent<TimeTracker>();
        statMap_ = BuildStatMap();
        stateMap_ = BuildStateMap();
        SetPlayerState("Idle");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        state_.HandleUpdate();

        if(Input.GetKey("space")) {
            animator_.SetBool("isJumping", true);
            isJumping_ = true;
        }

        /*if(isHalfwayThroughJump_) {
            Vector3 velocity = -transform.up * 5.0f + transform.forward;
            controller_.Move(velocity * 6.0f * Time.deltaTime);
        }*/

        /*if(isJumping_) {
            Vector3 velocity = -transform.up * 5.0f + transform.forward;
            controller_.Move(velocity * 6.0f * Time.deltaTime);
        }*/
    }

    private void LateUpdate() {
        state_.HandleLateUpdate();
    }

    private void OnTriggerEnter(Collider col) {
        state_.HandleOnTriggerEnter(col);
    }

    private void OnTriggerExit(Collider col) {
        state_.HandleOnTriggerExit(col);
    }

    private Dictionary<string, Stat> BuildStatMap() {
        Dictionary<string, Stat> statMap = new Dictionary<string, Stat>();
        
        Stat hpStat = new Stat("HP", 300);
        hpStat.AddModifier(new StatModifier(hpStat.Name, StatModifier.ModifierType.ADD, 50.0f));
        hpStat.AddModifier(new StatModifier(hpStat.Name, StatModifier.ModifierType.MULTIPLY, -0.5f));
        statMap.Add(hpStat.Name, hpStat);

        Stat attackStat = new Stat("Attack", 5);
        attackStat.AddModifier(new StatModifier(attackStat.Name, StatModifier.ModifierType.MULTIPLY, 0.5f));
        statMap.Add(attackStat.Name, attackStat);

        return statMap;
    }

    private Dictionary<string, EntityState> BuildStateMap() {
        return new Dictionary<string, EntityState> 
        {
            { "Idle", new S_PlayerIdle(gameObject, headIk_) },
            { "Walk", new S_PlayerWalk(gameObject, headIk_, walkSpeed_) },
            { "Run", new S_PlayerRun(gameObject, headIk_, runSpeed_) },
            { "Scared", new S_PlayerScared(gameObject, headIk_) },
            { "Aggressive", new S_PlayerAggressive(gameObject, headIk_) },
            { "Retreat", new S_PlayerRetreat(gameObject, headIk_) },
            { "Fight", new S_PlayerFight(gameObject, headIk_, bigBallOfViolencePrefab_) },
            { "Sitting", new S_PlayerSitting(gameObject, headIk_) }
        };
    }

    public int? GetStatValueByName(string statName) {
        if(string.IsNullOrEmpty(statName) || !statMap_.ContainsKey(statName)) return null;
        return statMap_[statName].GetStatValue();
    }

    public void SetPlayerState(string stateName) {
        if(string.IsNullOrEmpty(stateName)) return;

        if(stateMap_.ContainsKey(stateName)) {
            if(state_ != null)
                state_.HandleStateEnd();
            state_ = stateMap_[stateName];
            state_.HandleStateSet();
        }
    }

    public bool IsInMovingState() {
        return state_.IsMovingState;
    }

    public void SetIsJumping(bool isJumping) {
        //isJumping_ = isJumping;
        //animator_.SetBool("isJumping", false);
        //isHalfwayThroughJump_ = false;
        state_.NotifyAnimationDone();
    }

    public void HandleRotate() {
        state_.DoRotate();
        //isHalfwayThroughJump_ = true;
        
    }

    public void SetTimeTracker(float seconds, TimeTracker.TimeUpHandler handler) {
        timeTracker_.StartTimer(seconds, handler);
    }

    public void StopTimeTracker() {
        timeTracker_.StopTimer();
    }
}
