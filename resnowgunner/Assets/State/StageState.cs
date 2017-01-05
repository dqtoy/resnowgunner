using UnityEngine;
using System.Collections;
public enum eUIStageObj
{
    GUNNER,
    Map2,
    BackGroundHouse,
    MainCamera,
}
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
        //StateMgr.Instance.GetStateObject(eStateType.STATE_TYPE_STAGE, eUIStageObj.MainCamera.ToString("F")).SetActive(true);
    }
}
