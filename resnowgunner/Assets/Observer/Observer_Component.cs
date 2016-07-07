using UnityEngine;
using System.Collections;
public enum eTeamType
{
    TEAM_1,
    TEAM_2,
}
public class Observer_Component : BaseObject
{
    [SerializeField]
    eAIType m_MakeAIType = eAIType.LOBBY_AI;

    [SerializeField]
    bool m_bEnableBoard = true;

    [SerializeField]
    eTeamType m_TeamType;

    [SerializeField]
    string m_TemplateKey = string.Empty;

    GameCharacter m_SelfCharacter = null;
    BaseObject m_TargetObject = null;

    public eTeamType TEAM_TYPE { get { return m_TeamType; } }

    //jj 0116
    public string TEMP_KEY { get { return m_TemplateKey; } }

    BaseAI m_AI = null;

    public BaseAI AI
    {
        get { return m_AI; }
    }
    void Awake() // Mgr에 추가
    {

        switch (m_MakeAIType)
        {
            case eAIType.LOBBY_AI:
                {
                    GameObject aiObject = new GameObject();
                    aiObject.name = m_MakeAIType.ToString("F");
                    m_AI = aiObject.AddComponent<LobbyAI>();
                    aiObject.transform.SetParent(SelfTransform);
                }
                break;
        }

        m_AI.OBSERVER_COMPONENT = this;

        //GameCharacter 를 추가 하는과정.

        GameCharacter gameCharacter = CharacterMgr.Instance.AddCharacter(m_TemplateKey);
        gameCharacter.OBSERVER_COMPONENT = this;
        m_SelfCharacter = gameCharacter;

        //~GameCharacter 를 추가 하는과정.

        //item 등록.

        //TemplateKey 값으로 UsingItem(Inventory)을 메니저에게 등록.

        //~item 등록.

        ObserverMgr.Instance.AddObserver(this);
    }
    public double GetFactorData(eFactorData factorData)
    {
        return m_SelfCharacter.CHARACTER_FACTOR.GetFactorData(factorData);
    }

    void OnDestroy()
    {
        if (m_AI != null)
        {
            Destroy(m_AI.SelfObject);
        }
    }

    void Update()
    {
        //if (m_AI.STUN == true)
        m_AI.UpdateAI();

        if (m_AI.END)
        {
            if (m_TeamType == eTeamType.TEAM_2)
            {
                Destroy(SelfObject);
                this.ThrowEvent("RESET");
            }
            else
            {
                SelfObject.SetActive(false);
            }

        }

        //GetItemFactorTable();
    }
}
