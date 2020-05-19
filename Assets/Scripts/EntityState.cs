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
    protected static float gravity_ = 5.0f;
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

    public virtual void SetAnimatorFlags() {}

    public virtual void HandleLateUpdate(GameObject inProximityEnemy) {}

    public virtual void HandleEnemyEnterCloseZone(Collider col) {}

    public virtual void HandleEnemyExitCloseZone(Collider col) {}

    public virtual void HandleInput() {}

    protected void LookAtInProximityEnemy(GameObject inProximityEnemy) {
        if(inProximityEnemy != null) {
            Vector3 direction = (inProximityEnemy.transform.position - headIk_.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(headIk_.transform.forward, direction);
            float rotationAngle = Quaternion.Angle(lookRotation, baseHeadRotation_);
            if(rotationAngle < -80.0f) {
                //self_.transform.LookAt(inProximityEnemy.transform);
                //self_.transform.Rotate(0, rotationAngle + 80, 0, Space.Self);
                //headIk_.transform.rotation = Quaternion.AngleAxis(80, new Vector3(0, 0, -1));
            } else if(rotationAngle > 80.0f) {
                //self_.transform.LookAt(inProximityEnemy.transform);
                //self_.transform.Rotate(0, rotationAngle - 80, 0, Space.Self);
                //headIk_.transform.rotation = Quaternion.AngleAxis(80, new Vector3(0, 0, 1));
            } else {
                headIk_.transform.rotation = lookRotation;
            }
            //headIk_.transform.rotation = Quaternion.LookRotation(headIk_.transform.forward, direction);
        }
    }

    protected void HandleMovement(float moveSpeed, string shiftToState) {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f)) {
            if(Input.GetKey("left shift")) {
                playerController_.SetPlayerState(shiftToState);
                return;
            }

            Vector3 velocity = mainCamera_.transform.right * x + -self_.transform.up * gravity_ + mainCamera_.transform.forward * z;
            controller_.Move(velocity * moveSpeed * Time.deltaTime);
        } else {
            playerController_.SetPlayerState("Idle");
        }
    }
}
