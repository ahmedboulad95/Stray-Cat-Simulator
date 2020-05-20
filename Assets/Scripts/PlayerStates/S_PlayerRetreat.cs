using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerRetreat : EntityState
{
    public S_PlayerRetreat(GameObject self, GameObject headIk) : base(self, headIk) {}

    public override void HandleUpdate() {
        self_.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
    }
}
