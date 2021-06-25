using System;
using UnityEngine;

public class EntityState
{
    protected GameObject self_;
    protected Animator animator_;
    protected GameObject headIk_;
    protected CharacterController controller_;
    protected PlayerController playerController_;
    protected Quaternion baseHeadRotation_;
    protected Camera mainCamera_;
    protected Vector3 moveDirection_;
    protected float moveSpeed_ = 0.0f;
    protected const float GRAVITY_ = 20.0f;
    protected const float MAX_SPEED_ = 20.0f;
    protected const float LERP_SPEED_ = 2.5f;
    protected static GameObject inProximityEnemy_;
    protected static float turnSmoothTime_ = 0.1f;
    protected float turnSmoothVelocity_;
    public bool IsMovingState;

    public EntityState(GameObject self, GameObject headIk) {
        self_ = self;
        headIk_ = headIk;
        controller_ = self_.GetComponent<CharacterController>();
        playerController_ = self_.GetComponent<PlayerController>();
        animator_ = self_.GetComponent<Animator>();
        baseHeadRotation_ = headIk_.transform.rotation;
        mainCamera_ = Camera.main;
        IsMovingState = false;
    }

    public virtual void HandleStateSet() {
        //playerController_.StopTimeTracker();
        SetAnimatorFlags();
    }

    public virtual void HandleUpdate() {
        HandleInput();
        HandleMovement();
        
        float currentSpeed = animator_.GetFloat("speedPercent") * MAX_SPEED_;
        float currentSpeedPercent = Mathf.Lerp(currentSpeed, moveSpeed_, Time.deltaTime * LERP_SPEED_) / MAX_SPEED_;
        animator_.SetFloat("speedPercent", currentSpeedPercent);
    }

    public virtual void HandleLateUpdate() {}

    public virtual void HandleOnTriggerEnter(Collider col) {
        inProximityEnemy_ = col.gameObject;
    }

    public virtual void HandleOnTriggerExit(Collider col) {
        inProximityEnemy_ = null;
    }

    protected virtual void HandleInput() {
        moveDirection_ = MovementUtility.GetMoveDirection(self_, mainCamera_, turnSmoothTime_, ref turnSmoothVelocity_);
    }

    protected virtual void HandleMovement() {
        moveDirection_.y -= GRAVITY_ * Time.deltaTime;
        controller_.Move(moveDirection_.normalized * moveSpeed_ * Time.deltaTime);
    }

    protected virtual void SetAnimatorFlags() {}

    public virtual void DoRotate() {}
    public virtual void NotifyAnimationDone() {}

    public virtual void HandleStateEnd() {}

    protected void LookAtInProximityEnemy() {
        if(inProximityEnemy_ != null) {
            Vector3 direction = (inProximityEnemy_.transform.position - headIk_.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(headIk_.transform.forward, direction);
            float rotationAngle = Quaternion.Angle(lookRotation, baseHeadRotation_);
            if(rotationAngle < -80.0f) {

            } else if(rotationAngle > 80.0f) {

            } else {
                headIk_.transform.rotation = lookRotation;
            }
        }
    }

    protected bool IsPlayerStrongerThanEnemy(GameObject enemy) {
        int? enemyAttackStat = enemy.GetComponent<PredatorAI>().GetStatValueByName("Attack");
        int? playerAttackStat = playerController_.GetStatValueByName("Attack");

        bool isPlayerStronger = false;
        if(enemyAttackStat != null && playerAttackStat != null) {
            if(playerAttackStat > enemyAttackStat) {
                isPlayerStronger = true;
            }
        }
        return isPlayerStronger;
    }

    protected void HandleEnemyEncounter(GameObject enemy) {
        if(IsPlayerStrongerThanEnemy(enemy)) {
            playerController_.SetPlayerState("Aggressive");
        } else {
            playerController_.SetPlayerState("Scared");
        }
    }

    [ObsoleteAttribute("This method will be deleted soon")]
    public virtual void HandleEnemyEnterCloseZone(Collider col) {}

    [ObsoleteAttribute("This method will be deleted soon")]
    public virtual void HandleEnemyExitCloseZone(Collider col) {}
}
