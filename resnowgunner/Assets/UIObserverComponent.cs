﻿using UnityEngine;
using System.Collections;

public class UIObserverComponent : UIBaseObject {

    public override object GetEventData(string keyData, params object[] datas)
    {
        return null;
    }

    public override void ThrowEventHandler(string keyData, params object[] datas)
    {
        switch (keyData)
        {
            
        }
    }
}
