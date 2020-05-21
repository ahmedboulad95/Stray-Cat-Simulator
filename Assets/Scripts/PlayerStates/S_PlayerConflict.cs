using UnityEngine;

public class S_PlayerConflict : EntityState
{
    public S_PlayerConflict(GameObject self, GameObject headIk) : base(self, headIk) {}

    protected override void HandleInput() {
        if(Input.GetKeyDown("space")) {
            Debug.Log("Moving out of radius");
            playerController_.SetPlayerState("Retreat");
        } else if(Input.GetKeyDown("x")) {
            Debug.Log("Attacking");
            playerController_.SetPlayerState("Fight");
        }
    }

    public override void HandleLateUpdate() {
        base.HandleLateUpdate();
        LookAtInProximityEnemy();
    }
}
