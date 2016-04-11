using UnityEngine;
using System.Collections;

public class StageState : BaseState {

    public override eStateType STATE_TYPE
    {
        get { return eStateType.STATE_TYPE_STAGE; }
    }

    public override void StartState()
    {
        base.StartState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }
    public override void EndState()
    {
        base.EndState();
    }
}
