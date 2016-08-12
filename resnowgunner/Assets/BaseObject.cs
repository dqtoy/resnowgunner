using UnityEngine;
using System.Collections;

public enum eBaseObjectState
{
    STATE_NORMAL,
    STATE_DIE,
}
public class BaseObject : CacheObject
{
    //CacheObject 의 SelfObject 와 Transform을 바꾸자. 

    BaseObject m_Observer = null;

    public BaseObject OBSERVER_COMPONENT
    {
        get { return m_Observer; }
        set { m_Observer = value; }
    }

    eBaseObjectState m_objectState = eBaseObjectState.STATE_NORMAL;

    public eBaseObjectState OBJECT_STATE
    {
        get
        {
            if (OBSERVER_COMPONENT == null)
                return m_objectState;
            else
                return OBSERVER_COMPONENT.m_objectState;
        }

        set
        {
            if (OBSERVER_COMPONENT == null)
                m_objectState = value;
            else
                OBSERVER_COMPONENT.m_objectState = value;

        }
    }

    public override GameObject SelfObject
    {
        get
        {
            if (m_cacheObject == null)
            {
                if (OBSERVER_COMPONENT == null)
                    m_cacheObject = gameObject;

                else
                    m_cacheObject = OBSERVER_COMPONENT.SelfObject;

            }
            return m_cacheObject;
        }

    }

    public override Transform SelfTransform
    {
        get
        {
            if (m_cacheTransform == null)
            {
                if (OBSERVER_COMPONENT == null)
                    m_cacheTransform = transform;

                else
                    m_cacheTransform = OBSERVER_COMPONENT.SelfTransform;
            }
            return m_cacheTransform;
        }


    }

    public virtual object GetData(string keyData, params object[] datas)
    {
        return null;
    }

    public virtual void ThrowEvent(string keyData, params object[] datas)
    {

    }

    public Transform GetChild(string strName)
    {
        //재귀로 탐색
        return _GetChild(strName, SelfTransform);

    }

    Transform _GetChild(string strName, Transform trans)
    {
        if (trans.name == strName)
            return trans;


        for (int i = 0; i < trans.childCount; ++i)
        {
            Transform returnTrans = _GetChild(strName, trans.GetChild(i));

            if (returnTrans != null)
            {
                return returnTrans;
            }

        }

        return null;
    }
}
