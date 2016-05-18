using UnityEngine;
using System.Collections;

public class UILabelObject : UIBaseObject {

    public UILabel m_Label = null;

    public virtual UILabel SelfUILabel
    {
        get
        {
            if (m_cacheObject == null)
            {
                if (UI_OBSERVER_COMPONENT == null)
                    m_cacheObject = gameObject;
                else
                    m_cacheObject = UI_OBSERVER_COMPONENT.SelfObject;

                m_Label = m_cacheObject.GetComponent<UILabel>();
            }
            return m_Label;
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
