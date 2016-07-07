using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eAIStateType
{
    AI_STATE_NONE, // 0
    AI_STATE_IDLE, // 1
    AI_STATE_THROW_ATTACK,
    AI_STATE_RUN, // 3
    AI_STATE_JUMP, // 4
    AI_STATE_MULTIJUMP, // 5
    AI_STATE_HURT, // 6
    AI_STATE_DIE, // 7
    AI_STATE_COUNT // 8
}
public enum eAIType /* 로비 AI enum 값 제거*/
{
    LOBBY_AI,
    GUNNER_AI,
    ARCHER_AI,
    ZOMBIE_AI,
}
public enum eAIAutoMode
{
    AUTO_NONE,
    AUTO_BUTTON,
}
public struct stNextAI
{
    public eAIStateType m_StateType;
    public BaseObject m_TargetObject;
    public Vector3 m_Position;
}
public class BaseAI : BaseObject
{

    protected List<stNextAI> m_listNextAI = new List<stNextAI>();
    protected eAIStateType m_CurrentAIState = eAIStateType.AI_STATE_IDLE;

    bool m_bUpdateAI = false;

    // attacking or not?
    bool m_bAttack = false;
    bool m_bAI = true;
    protected Animator m_Animator = null;

    protected Vector3 m_MovePosition = Vector3.zero;
    Vector3 m_PrevMovePosition = Vector3.zero;

    public eAIAutoMode AUTO_MODE { get; set; }

    public eAIStateType CURRENT_AI_STATE
    {
        get { return m_CurrentAIState; }
    }

    protected int m_nAttackIndex = 0;

    public Animator ANIMATOR
    {
        get
        {
            // 멤버변수 m_Animator를 싱글톤 패턴처럼
            if (m_Animator == null)
            {
                m_Animator = SelfObject.GetComponent<Animator>();
            }
            return m_Animator;
        }
    }

    bool m_bEnd = false;
    public bool END { get { return m_bEnd; } protected set { m_bEnd = value; } }

    public bool IS_ATTACK
    {
        set { m_bAttack = value; }
        get { return m_bAttack; }
    }

    public void ClearAI()
    {
        m_listNextAI.Clear();
    }
    public void ClearAI(eAIStateType stateType)
    {
        m_listNextAI.RemoveAll(nextAI => nextAI.m_StateType == stateType);
    }
    virtual public void AddNextAI(eAIStateType nextStatetype, BaseObject targetObject = null, Vector3 position = new Vector3())
    {
        stNextAI nextAI = new stNextAI();
        nextAI.m_StateType = nextStatetype;
        nextAI.m_TargetObject = targetObject;
        nextAI.m_Position = position;

        m_listNextAI.Add(nextAI);
    }

    public void UpdateAI()
    {
        if (m_bUpdateAI == true)
            return;

        if (m_listNextAI.Count > 0)
        {
            _NextAI(m_listNextAI[0]);
            m_listNextAI.RemoveAt(0);
        }

        /*if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
        {
            m_listNextAI.Clear();
            _ProcessDie();
        }*/

        m_bUpdateAI = true;

        switch (m_CurrentAIState)
        {
            case eAIStateType.AI_STATE_IDLE:
                {
                    StartCoroutine(_Idle());
                }
                break;

                /*case eAIStateType.AI_STATE_ATTACK_1:
                    {
                        StartCoroutine(_Attack1());
                    }
                    break;

                case eAIStateType.AI_STATE_ATTACK_2:
                    {
                        StartCoroutine(_Attack2());
                    }
                    break;

                case eAIStateType.AI_STATE_RUN:
                    {
                        StartCoroutine(_Run());
                    }
                    break;

                case eAIStateType.AI_STATE_DIE:
                    {
                        StartCoroutine(_Die());
                    }
                    break;
                    */
        }
    }
    void _NextAI(stNextAI nextAI)
    {
        if (nextAI.m_TargetObject != null)
            OBSERVER_COMPONENT.ThrowEvent("SET_TARGET", nextAI.m_TargetObject);

        if (nextAI.m_Position != Vector3.zero)
            m_MovePosition = nextAI.m_Position;

        switch (nextAI.m_StateType)
        {
            case eAIStateType.AI_STATE_IDLE:
                {
                    _ProcessIdle();
                }
                break;
        }
    }

    void _ChangeAnimation()
    {
        ANIMATOR.SetInteger("State", (int)m_CurrentAIState);
    }
    virtual protected IEnumerator _Idle()
    {
        m_bUpdateAI = false;
        yield break;
    }
    virtual protected void _ProcessIdle()
    {
        m_CurrentAIState = eAIStateType.AI_STATE_IDLE;
        _ChangeAnimation();

        Debug.Log("Idle");
    }
}
