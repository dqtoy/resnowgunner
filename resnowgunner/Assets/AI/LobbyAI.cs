using UnityEngine;
using System.Collections;

public class LobbyAI : BaseAI {

    public override void AddNextAI(eAIStateType nextAI, BaseObject targetObject = null, Vector3 position = new Vector3())
    {
        if (m_CurrentAIState != eAIStateType.AI_STATE_IDLE && nextAI != eAIStateType.AI_STATE_IDLE)
            return;

        base.AddNextAI(nextAI, targetObject, position);
    }

    protected override IEnumerator _Idle()
    {
        yield return StartCoroutine(base._Idle());
    }
}
