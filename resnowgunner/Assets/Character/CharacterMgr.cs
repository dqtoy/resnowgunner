using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;

/*
0 None
1 기본 캐릭터
2 숨겨진 캐릭터 1
3 숨겨진 캐릭터 2
4 숨겨진 캐릭터 3
5 숨겨진 캐릭터 4
*/

public struct CharacterFactorInfo
{
    public eCharacterType CharacterType;
    public eCharacterKey CharacterKey;
    public eRaceType RaceType;
    public string Key;
    public string Name;
    public string PrefabName;
    public int Health;
    public float DaySpd;
    public float NightSpd;
    public int RequiredLv;
    public bool IsPurchasing;
    public int RequiredPrice;
    public string hobby;
    public FactorTable factorTable;
//    public LevelTable levelTable;
}
public class CharacterMgr : BaseMgr<CharacterMgr>
{
    public string m_temp = string.Empty;

    
    Dictionary<string, CharacterFactorInfo> m_dicFactorInfo = new Dictionary<string, CharacterFactorInfo>();
    
    public Dictionary<string, CharacterFactorInfo> FACTOR_INFO
    {
        get { return m_dicFactorInfo; }
    }
    //Character정보 - Server로 보내는 Data로 연계시켜야함.
    List<string> m_listSelectingCharacter = new List<string>();

    public List<string> SELECTING_CHARACTER
    {
        get { return m_listSelectingCharacter; }
        set { m_listSelectingCharacter = value; }
    }
    //sjh 소유Character
    List<string> m_listHasCharacter = new List<string>();
    public List<string> HAS_CHARACTER
    {
        get { return m_listHasCharacter; }
        set { m_listHasCharacter = value; }
    }
    Dictionary<string, CharacterTemplateData> m_dicTemplateData = new Dictionary<string, CharacterTemplateData>();
    List<GameCharacter> m_listCharacter = new List<GameCharacter>();
    Dictionary<string, GameCharacter> m_dicGameCharacter = new Dictionary<string, GameCharacter>();

    public Dictionary<string, GameCharacter> GAME_CHARACTER
    {
        get { return m_dicGameCharacter; }
    }
    public Dictionary<string, CharacterTemplateData> TEMPLATE_DATA
    {
        get { return m_dicTemplateData; }
    }

    void Start ()
    {
        
        //InitGameCharacter();
        Init();
    }


    public void InitData()
    {
        //Min-Goo 2016년 4월 11일 오전 3시 56분 JSON Parsing
        //Min-Goo 2016년 6월 15일 JSON 파일 형식 읽어오기
        TextAsset charText = Resources.Load<TextAsset>("CHARIC_TEMPL");
        if (charText != null)
        {
            JSONClass nodeData = JSON.Parse(charText.text) as JSONClass; // Dictionary가 넘어온다.
            if (nodeData != null)
            {
                JSONClass charInfoNode = nodeData["CHARIC_TEMPLATE"] as JSONClass;
                if (charInfoNode != null)
                {
                    foreach (KeyValuePair<string, JSONNode> keyValue in charInfoNode)
                    {
                        m_dicTemplateData.Add(keyValue.Key, new CharacterTemplateData(keyValue.Key, keyValue.Value));
                    }
                }
            }
        }
        //sjh ~JSON Parsing
        //sjh InitHasCharacter
        //min Start 함수가 먼저 호출되고 m_listHasCharacter[가지고 있는 캐릭터]의 목록이 생기게끔 임시 해결책.
        InitHasCharacter();
    }
    void InitHasCharacter()
    {
        if (m_dicTemplateData.Count == 0)
            return;

        Dictionary<string, CharacterTemplateData>.Enumerator enumerator = m_dicTemplateData.GetEnumerator();

        while(enumerator.MoveNext())
        {
            if(m_listHasCharacter.Contains(enumerator.Current.Value.KEY)==false)
                m_listHasCharacter.Add(enumerator.Current.Value.KEY);   
        }
    }

    void Init()
    {
        // 소지 캐릭터 정보
        List<string>.Enumerator hasCharEnumerator = m_listHasCharacter.GetEnumerator();

        GameCharacter selectingGameCharacter = null;

        while (hasCharEnumerator.MoveNext())
        {
            AddCharacter(hasCharEnumerator.Current);
        }
    }
    
    public GameCharacter AddCharacter(string strTemplateKey)
    {
        CharacterTemplateData templateData = GetTemplate(strTemplateKey);

        if (templateData == null)
            return null;

        GameCharacter myCharacter = null;
        
        // 전체 캐릭터 한명씩 추가
        if (m_dicGameCharacter.TryGetValue(strTemplateKey, out myCharacter) != true)
        {
            GameCharacter gameCharacter = new GameCharacter();
            gameCharacter.SetTemplate(templateData);
            m_dicGameCharacter.Add(strTemplateKey, gameCharacter);
            return gameCharacter;
        }

        return myCharacter;

    }


    public GameCharacter GetCharacter(string strTemplateKey)
    {
        GameCharacter returnCharacter = null;

        if (m_dicGameCharacter.TryGetValue(strTemplateKey, out returnCharacter) == true)
        {
            return returnCharacter;
        }

        else
            return null;

    }

    public Dictionary<string, GameCharacter> GetFullGameCharacter()
    {
        return m_dicGameCharacter;
    }

    public CharacterTemplateData GetTemplate(string strTemplateKey)
    {
        CharacterTemplateData templateData = null;
        m_dicTemplateData.TryGetValue(strTemplateKey, out templateData);
        return templateData;
    }

    // sjh + Min-Goo Ui Info
    public void MakeUI()
    {
        CharacterFactorInfo stFactorInfo;

        // 소지 캐릭터 정보
        List<string>.Enumerator hasCharEnumerator = m_listHasCharacter.GetEnumerator();

        // 전체 캐릭터 정보
        Dictionary<string, GameCharacter>.Enumerator charenumerator = m_dicGameCharacter.GetEnumerator();

        string strcharkey = string.Empty;

        GameCharacter selectingGameCharacter = null;

        while (hasCharEnumerator.MoveNext())
        {
            if (m_dicGameCharacter.TryGetValue(hasCharEnumerator.Current, out selectingGameCharacter) == true)
            {
                stFactorInfo = new CharacterFactorInfo();
                strcharkey = hasCharEnumerator.Current;

                //sjh
                stFactorInfo.CharacterType = selectingGameCharacter.CHARACTER_TEMPLATE.CHARACTER_TYPE;
                stFactorInfo.CharacterKey = selectingGameCharacter.CHARACTER_TEMPLATE.CHARACTER_KEY;
                stFactorInfo.RaceType = selectingGameCharacter.CHARACTER_TEMPLATE.RACE_TYPE;
                stFactorInfo.Key = selectingGameCharacter.CHARACTER_TEMPLATE.KEY;
                stFactorInfo.Name = selectingGameCharacter.CHARACTER_TEMPLATE.NAME;
                stFactorInfo.PrefabName = selectingGameCharacter.CHARACTER_TEMPLATE.PREFAB_NAME;
                stFactorInfo.factorTable = selectingGameCharacter.CHARACTER_TEMPLATE.FACTOR_TABLE;
                stFactorInfo.RequiredLv = selectingGameCharacter.CHARACTER_TEMPLATE.REQUIRED_LEVEL;
                stFactorInfo.IsPurchasing = selectingGameCharacter.CHARACTER_TEMPLATE.IS_PURCHASING;
                stFactorInfo.RequiredPrice = selectingGameCharacter.CHARACTER_TEMPLATE.REQUIRED_PRICE;
                stFactorInfo.hobby = selectingGameCharacter.CHARACTER_TEMPLATE.HOBBY;
                stFactorInfo.factorTable = selectingGameCharacter.CHARACTER_TEMPLATE.FACTOR_TABLE;
//                stFactorInfo.levelTable = selectingGameCharacter.CHARACTER_TEMPLATE.LEVEL_TABLE;
                // 아직 쓰이지 않는 변수
                // selectingGameCharacter.CHARACTER_TEMPLATE.LIST_EXTRA_CLASS
                // selectingGameCharacter.CHARACTER_TEMPLATE.LIST_EXTRA_CLASS_PARAM_F            
                if (m_dicFactorInfo.ContainsKey(strcharkey) != true)
                    m_dicFactorInfo.Add(strcharkey, stFactorInfo);
            }
        }

        //return m_dicFactorInfo;
    }
    //sjh Selecting Character Info
    
    // 초기화하기 위해 호출해주는 함수
    public void SetUp()
    {
        
        
    }
    // min
}
