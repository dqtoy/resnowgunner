using UnityEngine;
using System.Collections;

public class UIGridObject : UIBaseObject {
    [SerializeField]
    UIGrid m_Grid = null;

    public virtual UIGrid SelfUIGrid
    {
        get
        {
            if (m_cacheObject == null)
            {
                if (UI_OBSERVER_COMPONENT == null)
                    m_cacheObject = gameObject;
                else
                    m_cacheObject = UI_OBSERVER_COMPONENT.SelfObject;

                m_Grid = m_cacheObject.GetComponent<UIGrid>();
            }
            return m_Grid;
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
