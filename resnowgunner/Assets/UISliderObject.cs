using UnityEngine;
using System.Collections;
using System;
public class UISliderObject : UIBaseObject
{
    public UISlider m_Slider = null;

    public virtual UISlider SelfUISlider
    {
        get
        {
            if (m_cacheObject == null)
            {
                if (UI_OBSERVER_COMPONENT == null)
                    m_cacheObject = gameObject;
                else
                    m_cacheObject = UI_OBSERVER_COMPONENT.SelfObject;

                m_Slider = m_cacheObject.GetComponent<UISlider>();
            }
            return m_Slider;
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
