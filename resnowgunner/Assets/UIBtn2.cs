using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/*class UIBtnClickListener
{
    public UIBtnClickListener()
    {
       
    }
    public delegate void OnClick();
    // 가상 소멸자
}*/

class UIBtn2 : MonoBehaviour
{
    void Start()
    {
    }
}
// 먼저 이벤트 발생에 사용할 델리게이트 형식을 선언한다.
// 이것은 System.EventHandler 형식과 같은 델리게이트이다.
// 이 델리게이트는 추상 옵저버로서의 기능을 제공한다.
// 어떠한 구현도 제공하지 않으며, 단지 규약만 제공한다.
//public delegate void EventHandler(object sender, EventArgs e);


// 다음으로, 공개된 이벤트를 선언한다. 이것은 구체적인 서브젝트로서의 기능을 제공한다.
//public class Button
//{
    // 공개된 이벤트를 선언한다.
//    public event EventHandler Clicked;
    //Stack<UIBtnClickListener> mylistener;
    //public void addListener(UIBtnClickListener listener)
    //{
    //   mylistener.Push(listener);
    //}

    // 관습적으로, .NET 이벤트 발생은 가상 메서드로 구현된다.
    // 이는 하위 클래스가 해당 이벤트를 재정의를 통해 사용할 수 있게 하고, 발생 여부도 조정할 수 있게 한다.
//    protected virtual void OnClicked(EventArgs e)
//    {
        // Clicked 이벤트에 등록된 모든 EventHandler 델리게이트를 호출한다.
   //     if (Clicked != null)
    //        Clicked(this, e);
   // }

    //public void click()
    //{
        // 버튼이 클릭되었을 때 관측자에 통지
    //}

    //private void notify()
    //{
     //   foreach (UIBtnClickListener listener in mylistener)
     //   {
            //listener.OnClick();
            //listener
     //   }
    //}
//}
