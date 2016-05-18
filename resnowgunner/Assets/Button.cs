using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
class UIBtnClickListener
{
    public UIBtnClickListener()
    {

    }
    public delegate void OnClick();
    // 가상 소멸자
}

//public delegate void EventHandler(object sender, params object[] datas);

public class UIBtn
{
    Stack<UIBtnClickListener> mylistener;
    void addListener(UIBtnClickListener listener)
    {
        mylistener.Push(listener);
    }

    public void click()
    {
        // 버튼이 클릭되었을 때 관측자에 통지
    }

    private void notify()
    {
        foreach (UIBtnClickListener listener in mylistener)
        {
            //listener.OnClick();
            //listener
        }
    }
}
