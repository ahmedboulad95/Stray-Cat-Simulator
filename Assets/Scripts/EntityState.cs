using UnityEngine;

public abstract class EntityState
{
    protected GameObject self_;
    protected Animator animator_;
    protected GameObject headIk_;
    protected Quaternion baseHeadRotation_;

    public EntityState(GameObject self, GameObject headIk) {
        self_ = self;
        headIk_ = headIk;
        animator_ = self_.GetComponent<Animator>();
        baseHeadRotation_ = headIk_.transform.rotation;
    }

    public abstract void HandleLateUpdate(GameObject inProximityEnemy);

    public abstract void HandleEnemyEnterCloseZone(Collider col);

    public abstract void HandleEnemyExitCloseZone(Collider col);

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
}
