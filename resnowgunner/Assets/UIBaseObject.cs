using UnityEngine;
using System.Collections;

public enum eUIBaseObjectType
{
    TYPE_NULL,      // UI가 없는데?
    TYPE_SCENE,     // UI가 Scene Type?
    TYPE_NORMAL,    // 일반 UI
    TYPE_LOADING,   // 로딩
    TYPE_POPUP,     // 팝업 창
    TYPE_ALERT,     // 메시지 창
    TYPE_SETTING,   // 설정 창 - 확장성 고려
    TYPE_CHECK,     // 확인 창 - 필요해서
}

public enum eUIBaseButtonElementType
{
    TYPE_NO,
    TYPE_CLICK,
    TYPE_YES,
    TYPE_EXIT,
    TYPE_BACK,
    TYPE_STAGE,
}
public class UIBaseObject : CacheObject {

    // CacheObect 의 SelfObject 와 Transform를 바꾸자
    UIBaseObject m_UIObserver = null;
    
    public UIBaseObject UI_OBSERVER_COMPONENT
    {
        get { return m_UIObserver; }
        set { m_UIObserver = value; }
    }

    eUIBaseObjectType m_uiobjectState = eUIBaseObjectType.TYPE_NORMAL;

    public eUIBaseObjectType OBJECT_STATE
    {
        get
        {
            if (UI_OBSERVER_COMPONENT == null)
                return m_uiobjectState;
            else
                return UI_OBSERVER_COMPONENT.m_uiobjectState;
        }

        set
        {
            if (UI_OBSERVER_COMPONENT == null)
                m_uiobjectState = value;
            else
                UI_OBSERVER_COMPONENT.m_uiobjectState = value;

        }
    }

    public override GameObject SelfObject
    {
        get
        {
            if (m_cacheObject == null)
            {
                if (UI_OBSERVER_COMPONENT == null)
                    m_cacheObject = gameObject;

                else
                    m_cacheObject = UI_OBSERVER_COMPONENT.SelfObject;

            }
            return m_cacheObject;
        }

    }

    


    public virtual object GetEventData(string keyData, params object[] datas)
    {
        return null;
    }

    public virtual void ThrowEventHandler(string keyData, params object[] datas)
    {

    }
}
