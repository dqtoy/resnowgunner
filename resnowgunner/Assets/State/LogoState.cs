using UnityEngine;
using System.Collections;

public class LogoState : BaseState {

    float m_fElapsedTime = 0.0f;
    float m_fLimitTime = 5.0f;

    public override eStateType STATE_TYPE
    {
        get { return eStateType.STATE_TYPE_LOGO; }
    }

    public override void StartState()
    {
        base.StartState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        m_fElapsedTime += Time.smoothDeltaTime;

        if (m_fElapsedTime > m_fLimitTime)
        {
            ChangeState(eStateType.STATE_TYPE_FRIENDS_LOBBY);
        }
    }

    public override void EndState()
    {
        base.EndState();
        m_fElapsedTime = 0;
    }
}
