using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIFormObject : UIBaseObject
{

    [SerializeField]
    UICamera m_UICamera = null;

   
    public virtual UICamera SelfUICamera
    {
        get
        {
            if (m_cacheObject == null)
            {
                if (UI_OBSERVER_COMPONENT == null)
                    m_cacheObject = gameObject;
                else
                    m_cacheObject = UI_OBSERVER_COMPONENT.SelfObject;

                m_UICamera = m_cacheObject.GetComponentInChildren<UICamera>();
            }
            return m_UICamera;
        }
    }

    public override object GetEventData(string keyData, params object[] datas)
    {
        switch (keyData)
        {
            case "Test":
                return "Test";
        }
        return base.GetEventData(keyData);
    }

    public override void ThrowEventHandler(string keyData, params object[] datas)
    {
        switch (keyData)
        {

        }
    }
}
