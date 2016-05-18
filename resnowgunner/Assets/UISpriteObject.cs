using UnityEngine;
using System.Collections;

public class UISpriteObject : UIBaseObject {

    public UISprite m_Sprite = null;

    public virtual UISprite SelfUISprite
    {
        get
        {
            if (m_cacheObject == null)
            {
                if (UI_OBSERVER_COMPONENT == null)
                    m_cacheObject = gameObject;
                else
                    m_cacheObject = UI_OBSERVER_COMPONENT.SelfObject;

                m_Sprite = m_cacheObject.GetComponent<UISprite>();
            }
            return m_Sprite;
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


