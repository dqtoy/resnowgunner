using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
