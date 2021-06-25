using UnityEngine;

public class S_PlayerIdle : EntityState
{
    public S_PlayerIdle(GameObject self, GameObject headIk) : base(self, headIk) {
        moveSpeed_ = 0.0f;
    }
    
    protected override void SetAnimatorFlags() {
        animator_.SetBool("isWalking", false);
        animator_.SetBool("isRunning", false);
        animator_.SetBool("isScared", false);
        animator_.SetBool("isHeadShaking", false);
        animator_.SetBool("isSitting", false);
        animator_.SetBool("isAggressive", false);
        animator_.SetBool("isJumping", false);
    }

    public override void HandleStateSet() {
        base.HandleStateSet();
        playerController_.SetTimeTracker(10.0f, () => { 
            Debug.Log("Switching to player sitting");
            playerController_.SetPlayerState("Sitting"); 
        });
    }

    public override void HandleOnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Enemy") {
            base.HandleOnTriggerEnter(col);
            
            if(IsPlayerStrongerThanEnemy(col.gameObject)) {
                playerController_.SetPlayerState("Aggressive");
            } else {
                playerController_.SetPlayerState("Scared");
            }
        }
    }

    protected override void HandleInput() {
        base.HandleInput();
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f)) {
            if(Input.GetKey("left shift")) {
                playerController_.SetPlayerState("Run");
            } else {
                playerController_.SetPlayerState("Walk");
            }
        }
    }

    public override void HandleStateEnd() {
        playerController_.StopTimeTracker();
    }
}
