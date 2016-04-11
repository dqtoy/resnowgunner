using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;

public enum eStateType
{
    STATE_TYPE_NONE,
    STATE_TYPE_LOGO,          // 20160328 resnowgunner
    STATE_TYPE_FRIENDS_LOBBY, // 20160328 resnowgunner
    STATE_TYPE_MAP_LOADING,  // 20160411 Min-Goo
    STATE_TYPE_STAGE,         // 20160328 reTemp
    
    STATE_TYPE_COUNT,
}

public struct stSceneInfo
{
    public string SceneName;
    public string[] arrUI;
    public string[] arrObject;
}

public class StateMgr : BaseMgr<StateMgr>
{
    Dictionary<eStateType, BaseState> m_dicState = new Dictionary<eStateType, BaseState>();
    Dictionary<eStateType, stSceneInfo> m_dicSceneInfo = new Dictionary<eStateType, stSceneInfo>();

    BaseState m_CurrnetState = null;
    AsyncOperation m_Operation = null;

    float m_fElapsedTime = 0.0f;
    float m_fIntervalTime = 0.0f;
    void Awake()
    {
        // JSON 형식 파일 읽어오기.
        TextAsset sceneText = Resources.Load<TextAsset>("SCENE_INFO"); // JSON TEXT 파일을 읽고,
        if (sceneText != null)
        {
            JSONClass nodeData = JSON.Parse(sceneText.text) as JSONClass; // Dictionary가 넘어온다. 
            if (nodeData != null)
            {
                JSONClass sceneInfoNode = nodeData["SCENE_INFO"] as JSONClass;

                for (int i = (int)eStateType.STATE_TYPE_LOGO; i < (int)eStateType.STATE_TYPE_COUNT; ++i)
                {
                    JSONClass sceneClass = sceneInfoNode[((eStateType)i).ToString("F")] as JSONClass;
                    //JSONClass sceneClass = nodeData[((eStateType)i).ToString("F")] as JSONClass;
                    if (sceneClass != null)
                    {
                        stSceneInfo sceneInfo = new stSceneInfo();
                        sceneInfo.SceneName = sceneClass["SCENE_NAME"];

                        JSONArray arrUI = sceneClass["UI"] as JSONArray;
                        if (arrUI != null && arrUI.Count > 0)
                        {
                            sceneInfo.arrUI = new string[arrUI.Count];
                            for (int k = 0; k < arrUI.Count; ++k)
                            {
                                sceneInfo.arrUI[k] = arrUI[k];
                            }
                        }


                        JSONArray arrObject = sceneClass["OBJECT"] as JSONArray;
                        if (arrObject != null && arrObject.Count > 0)
                        {
                            sceneInfo.arrObject = new string[arrObject.Count];
                            for (int k = 0; k < arrObject.Count; ++k)
                            {
                                sceneInfo.arrObject[k] = arrObject[k];
                            }
                        }

                        m_dicSceneInfo.Add((eStateType)i, sceneInfo);
                    }
                }
            }

        }









        for (int i = (int)eStateType.STATE_TYPE_LOGO; i < (int)eStateType.STATE_TYPE_COUNT; ++i) // 초기화
        {
            eStateType stateType = (eStateType)i;
            BaseState makeState = _CreateState(stateType);
            m_dicState.Add(stateType, makeState); // 씬 1,2,3 사용 가능
        }

        StartState(eStateType.STATE_TYPE_LOGO);

        RegisterChild(eStateType.STATE_TYPE_LOGO, eStateType.STATE_TYPE_FRIENDS_LOBBY);         // LOGO -> FRIENDS_LOBBY
        RegisterChild(eStateType.STATE_TYPE_LOGO, eStateType.STATE_TYPE_STAGE);

        RegisterChild(eStateType.STATE_TYPE_FRIENDS_LOBBY, eStateType.STATE_TYPE_LOGO);         // FRIENDS_LOBBY -> LOGO
        RegisterChild(eStateType.STATE_TYPE_FRIENDS_LOBBY, eStateType.STATE_TYPE_MAP_LOADING); // FRIENDS_LOBBY -> MAP_LOADING
        RegisterChild(eStateType.STATE_TYPE_FRIENDS_LOBBY, eStateType.STATE_TYPE_STAGE);        // FRIENDS_LOBBY -> STAGE

        RegisterChild(eStateType.STATE_TYPE_MAP_LOADING, eStateType.STATE_TYPE_STAGE);

        RegisterChild(eStateType.STATE_TYPE_STAGE, eStateType.STATE_TYPE_FRIENDS_LOBBY);        // STAGE         -> MAP_LOADING
                                                                                                // 게임 결과창   -> 로비


    }

    void Update()
    {
        if (m_CurrnetState != _GetState(eStateType.STATE_TYPE_MAP_LOADING))
        {
            if (m_Operation != null) // 씬전환중 ?
            {
                m_fIntervalTime = m_fElapsedTime;
                m_fElapsedTime += Time.smoothDeltaTime;

                if (m_fElapsedTime - m_fIntervalTime <= 0.01f)
                    m_fElapsedTime += 0.01f;

                if (m_fElapsedTime > 2.0f) m_fElapsedTime = 2.0f;


                UIMgr.Instance.ShowLoadingUI(m_fElapsedTime / 2.0f);
                //UIMgr.Instance.ShowLoadingUI(m_Operation.progress);

                if (m_Operation.isDone == true && m_fElapsedTime >= 2.0f)
                {
                    // 완료
                    m_Operation = null;
                    UIMgr.Instance.HideLoadingUI();

                    if (m_CurrnetState != null)
                    {
                        m_CurrnetState.StartState();
                    }

                }
                else
                {
                    // 진행중
                    return;
                }
            }
        }
        else if (m_CurrnetState == _GetState(eStateType.STATE_TYPE_MAP_LOADING))
        {
            if (m_Operation != null) // 씬전환중 ?
            { 
                if (m_Operation.isDone == true)
                {
                    // 완료
                    m_Operation = null;
                    
                    if (m_CurrnetState != null)
                    {
                        m_CurrnetState.StartState();
                    }

                }
                else
                {
                    // 진행중
                    return;
                }
            }
        }

        if (m_CurrnetState == null)
            return;

        m_CurrnetState.UpdateState();
        if (m_CurrnetState.NEXT_STATE != null)
        {
            BaseState prevState = m_CurrnetState;
            m_CurrnetState = m_CurrnetState.NEXT_STATE;

            prevState.EndState();

            if (prevState.SCENE_INFO.SceneName != m_CurrnetState.SCENE_INFO.SceneName)
            {
                m_Operation = Application.LoadLevelAsync(m_CurrnetState.SCENE_INFO.SceneName);// 유니티에서 제공하는 씬전환함수. 
                if (m_CurrnetState != _GetState(eStateType.STATE_TYPE_MAP_LOADING))
                {
                    UIMgr.Instance.ShowLoadingUI(0.0f);
                    m_fElapsedTime = 0;
                    m_fIntervalTime = 0.0f;
                }
                else if (m_CurrnetState == _GetState(eStateType.STATE_TYPE_MAP_LOADING))
                {

                }


            }
            else
            {
                m_CurrnetState.StartState();
            }

        }
    }

    void StartState(eStateType startState)
    {
        BaseState start = _GetState(startState);
        if (start != null)
        {
            m_CurrnetState = start;
            m_CurrnetState.StartState();
        }
    }

    void RegisterChild(eStateType parentStateType, eStateType childStateType)
    {
        BaseState parentState = _GetState(parentStateType);
        BaseState childState = _GetState(childStateType);

        if (parentState != null && childState != null)
        {
            parentState.AddChildState(childState);
        }
    }

    BaseState _GetState(eStateType stateType)
    {
        BaseState returnState = null;
        m_dicState.TryGetValue(stateType, out returnState);
        return returnState;
    }

    BaseState _CreateState(eStateType stateType)
    {
        BaseState makeState = null;

        switch (stateType)
        {
            case eStateType.STATE_TYPE_LOGO:
                {
                    GameObject stateObject = new GameObject();
                    stateObject.name = eStateType.STATE_TYPE_LOGO.ToString("F");

                    DontDestroyOnLoad(stateObject);

                    makeState = stateObject.AddComponent<LogoState>();

                    if (m_dicSceneInfo.ContainsKey(stateType))
                    {
                        makeState.SCENE_INFO = m_dicSceneInfo[stateType];
                    }
                }
                break;

            case eStateType.STATE_TYPE_FRIENDS_LOBBY:
                {
                    GameObject stateObject = new GameObject();
                    stateObject.name = eStateType.STATE_TYPE_FRIENDS_LOBBY.ToString("F");

                    DontDestroyOnLoad(stateObject);

                    makeState = stateObject.AddComponent<FriendsLobbyState>();
                    if (m_dicSceneInfo.ContainsKey(stateType))
                    {
                        makeState.SCENE_INFO = m_dicSceneInfo[stateType];
                    }
                }
                break;
            case eStateType.STATE_TYPE_MAP_LOADING:
                {
                    GameObject stateObject = new GameObject();
                    stateObject.name = eStateType.STATE_TYPE_MAP_LOADING.ToString("F");

                    DontDestroyOnLoad(stateObject);

                    makeState = stateObject.AddComponent<MapLoadingState>();
                    if (m_dicSceneInfo.ContainsKey(stateType))
                    {
                        makeState.SCENE_INFO = m_dicSceneInfo[stateType];
                    }
                }
                break;
            case eStateType.STATE_TYPE_STAGE:
                {
                    GameObject stateObject = new GameObject();
                    stateObject.name = eStateType.STATE_TYPE_STAGE.ToString("F");

                    DontDestroyOnLoad(stateObject);

                    makeState = stateObject.AddComponent<StageState>();
                    if (m_dicSceneInfo.ContainsKey(stateType))
                    {
                        makeState.SCENE_INFO = m_dicSceneInfo[stateType];
                    }
                }
                break;
        }

        return makeState;
    }

    public void ChangeState(eStateType stateType, params object[] data)
    {
        if (m_CurrnetState != null)
            m_CurrnetState.ChangeState(stateType, data);
    }

}

