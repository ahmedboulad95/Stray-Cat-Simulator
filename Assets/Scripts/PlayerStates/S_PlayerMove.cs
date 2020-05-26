using UnityEngine;

public class S_PlayerMove : EntityState
{
    protected float moveSpeed_;
    protected float turnSmoothTime_ = 0.1f;
    protected float turnSmoothVelocity_;

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

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if(direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera_.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(self_.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity_, turnSmoothTime_);
            self_.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller_.Move(moveDirection.normalized * moveSpeed_ * Time.deltaTime);
        } else {
            playerController_.SetPlayerState("Idle");
            return;
        }
    }
}
