using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


// 먼저 이벤트 발생에 사용할 델리게이트 형식을 선언한다.
// 이것은 System.EventHandler 형식과 같은 델리게이트이다.
// 이 델리게이트는 추상 옵저버로서의 기능을 제공한다.
// 어떠한 구현도 제공하지 않으며, 단지 규약만 제공한다.
public delegate void EventHandler(object sender, EventArgs e);


// 다음으로, 공개된 이벤트를 선언한다. 이것은 구체적인 서브젝트로서의 기능을 제공한다.
public class UIBtnObject
{



    // 공개된 이벤트를 선언한다.
    public event EventHandler Clicked;
    //Stack<UIBtnClickListener> mylistener;
    //public void addListener(UIBtnClickListener listener)
    //{
    //   mylistener.Push(listener);
    //}

    




    // 관습적으로, .NET 이벤트 발생은 가상 메서드로 구현된다.ee
    // 이는 하위 클래스가 해당 이벤트를 재정의를 통해 사용할 수 있게 하고, 발생 여부도 조정할 수 있게 한다.
    protected virtual void OnClicked(EventArgs e)
    {
        // Clicked 이벤트에 등록된 모든 EventHandler 델리게이트를 호출한다.
        if (Clicked != null)
            Clicked(this, e);
    }

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
}


public enum eUIType
{
    PF_UI_LOADING,
    PF_UI_PopUp,
    PF_UI_BoardManager,
}

public enum eCharacterViewObjectType
{
    CharacterView_1,
    CharacterView_2,
}
public class UIMgr : BaseMgr<UIMgr> {
    Dictionary<eUIType, GameObject> m_dicUI = new Dictionary<eUIType, GameObject>();
    Dictionary<eCharacterViewObjectType, GameObject> m_dicCharacterViewObject = new Dictionary<eCharacterViewObjectType, GameObject>();
    UIButton uibutton;

    

    public void ShowLoadingUI(float fValue)
    {
        GameObject loadingui = _GetUI(eUIType.PF_UI_LOADING);
        if (loadingui.activeSelf == false)
        {
            loadingui.SetActive(true);
        }

        UI_Loading_EventHandler eventHandler = loadingui.GetComponentInChildren<UI_Loading_EventHandler>();
        eventHandler.SetValue(fValue);
    }

    public void HideLoadingUI()
    {
        GameObject loadingUI = _GetUI(eUIType.PF_UI_LOADING);
        if (loadingUI.activeSelf == true)
            loadingUI.SetActive(false);
    }

    public GameObject ShowUI(eUIType uiType)
    {
        GameObject showObject = _GetUI(uiType);
        if (showObject != null && showObject.activeSelf == false)
            showObject.SetActive(true);

        return showObject;
    }

    public void HideUI(eUIType uiType)
    {
        GameObject hideObject = _GetUI(uiType);
        if (hideObject != null && hideObject.activeSelf == true)
            hideObject.SetActive(false);
    }
    GameObject _GetUI(eUIType uiType)
    {
        if (m_dicUI.ContainsKey(uiType) == true)
        {
            return m_dicUI[uiType];
        }

        GameObject makeUI = null;
        GameObject prefabUI = Resources.Load<GameObject>(uiType.ToString("F"));
        if (prefabUI != null)
        {
            makeUI = NGUITools.AddChild(null, prefabUI);
            m_dicUI.Add(uiType, makeUI);
            DontDestroyOnLoad(makeUI);

            makeUI.SetActive(false);
        }

        return makeUI;
    }
    GameObject _GetCharacterViewUI(eCharacterViewObjectType characterViewtype, string path)
    {
        if (m_dicCharacterViewObject.ContainsKey(characterViewtype) == true)
        {
            return m_dicCharacterViewObject[characterViewtype];
        }

        GameObject makeUI = null;
        GameObject prefabUI = Resources.Load<GameObject>(path + characterViewtype.ToString("F"));

        if (prefabUI != null)
        {
            makeUI = NGUITools.AddChild(null, prefabUI);
            m_dicCharacterViewObject.Add(characterViewtype, makeUI);

            DontDestroyOnLoad(makeUI);
            m_dicCharacterViewObject[characterViewtype].SetActive(false);


        }
        return makeUI;
    }
    public void ShowCharacterView(eCharacterViewObjectType _CharacterViewObjectType)
    {
        GameObject CharacterViewObject = _GetCharacterViewUI(_CharacterViewObjectType, "CharacterView/View/");
        if (CharacterViewObject.activeSelf == false)
        {
            CharacterViewObject.SetActive(true);
        }

//      UI_Loading_EventHandler eventHandler = loadingui.GetComponentInChildren<UI_Loading_EventHandler>();
//      eventHandler.SetValue(fValue);

    }
    public void HideCharacterView(eCharacterViewObjectType _CharacterViewObjectType)
    {
        GameObject CharacterViewObject = _GetCharacterViewUI(_CharacterViewObjectType, "CharacterView/View/");
        if (CharacterViewObject.activeSelf == true)
            CharacterViewObject.SetActive(false);
    }

    public GameObject ShowCharacterViewGameObject(eCharacterViewObjectType _CharacterViewObjectType)
    {
        GameObject CharacterViewObject = _GetCharacterViewUI(_CharacterViewObjectType, "CharacterView/View/");
        if (CharacterViewObject.activeSelf == false)
        {
            CharacterViewObject.SetActive(true);
        }

        return CharacterViewObject;
        //eventHandler = loadingui.GetComponentInChildren<UI_Loading_EventHandler>();
        //eventHandler.SetValue(fValue);

    }

}
