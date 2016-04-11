using UnityEngine;
using System.Collections;

public class MapLoadingState : BaseState
{

    public override eStateType STATE_TYPE
    {
        get { return eStateType.STATE_TYPE_MAP_LOADING; }
    }

    public override void StartState()
    {
        MoveToSnowTown.Instance.IS_PROGRESSEND = false;
        MoveToSnowTown.Instance.IdleMotion();
        base.StartState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void EndState()
    {
        MoveToSnowTown.Instance.IS_PROGRESSEND = true;
        base.EndState();
    }
}
