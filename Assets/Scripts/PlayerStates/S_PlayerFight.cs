using UnityEngine;

public class S_PlayerFight : EntityState
{
    private GameObject combatEffect_;

    public S_PlayerFight(GameObject self, GameObject headIk, GameObject combatEffect) : base(self, headIk) {
        combatEffect_ = combatEffect;
    }

    public override void HandleStateSet() {
        base.HandleStateSet();
        
        if(combatEffect_ == null) return;
        Object.Instantiate(combatEffect_, self_.transform.position, Quaternion.identity);
    }
}
