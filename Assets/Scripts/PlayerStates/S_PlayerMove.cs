using UnityEngine;

public class S_PlayerMove : EntityState
{
    protected float moveSpeed_;

    public S_PlayerMove(GameObject self, GameObject headIk, float moveSpeed) : base(self, headIk) {
        moveSpeed_ = moveSpeed;
    }

    public override void HandleOnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Enemy") {
            base.HandleOnTriggerEnter(col);
            base.HandleEnemyEncounter(col.gameObject);
        }
    }

    protected void MovePlayer() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f)) {
            Vector3 velocity = mainCamera_.transform.right * x + -self_.transform.up * gravity_ + mainCamera_.transform.forward * z;
            controller_.Move(velocity * moveSpeed_ * Time.deltaTime);
        } else {
            playerController_.SetPlayerState("Idle");
            return;
        }
    }
}
