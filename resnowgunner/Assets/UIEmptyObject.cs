using UnityEngine;
using System.Collections;

public class UIEmptyObject : UIBaseObject {

    public virtual GameObject SelfUIEmptyObject
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
}
