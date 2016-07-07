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
   // public
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
        //Min-Goo 2016년 4월 11일 오전 3시 56분 JSON Parsing
        //Min-Goo 2016년 6월 15일 JSON 파일 형식 읽어오기
        TextAsset charText = Resources.Load<TextAsset>("CHARIC_TEMPL");
        if (charText != null)
        {
            JSONClass nodeData = JSON.Parse(charText.text) as JSONClass; // Dictionary가 넘어온다.
            if (nodeData != null)
            {
                JSONClass charInfoNode = nodeData["CHARIC_TEMPLATE"] as JSONClass;
                if(charInfoNode!=null)
                {
                    foreach (KeyValuePair<string, JSONNode> keyValue in charInfoNode)
                    {
                        m_dicTemplateData.Add(keyValue.Key, new CharacterTemplateData(keyValue.Key, keyValue.Value));
                    }
                }  
            }
        }
        //sjh ~JSON Parsing

        Init();
    }
    void Init()
    {
        // 소지 케릭터 정보
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


    public CharacterTemplateData GetTemplate(string strTemplateKey)
    {
        CharacterTemplateData templateData = null;
        m_dicTemplateData.TryGetValue(strTemplateKey, out templateData);
        return templateData;
    }

    //sjh Ui Info
    public Dictionary<string, CharacterFactorInfo> MakeUIInfo()
    {
        CharacterFactorInfo stFactorInfo;

        // 소지 케릭터 정보
        List<string>.Enumerator hasCharEnumerator = m_listHasCharacter.GetEnumerator();

        // 전체 케릭 정보
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
                stFactorInfo.Health = selectingGameCharacter.CHARACTER_TEMPLATE.HEALTH;
                stFactorInfo.DaySpd = selectingGameCharacter.CHARACTER_TEMPLATE.DAYSPD;
                stFactorInfo.NightSpd = selectingGameCharacter.CHARACTER_TEMPLATE.NIGHTSPD;
                stFactorInfo.RequiredLv = selectingGameCharacter.CHARACTER_TEMPLATE.REQUIRED_LEVEL;
                stFactorInfo.IsPurchasing = selectingGameCharacter.CHARACTER_TEMPLATE.IS_PURCHASING;
                stFactorInfo.RequiredPrice = selectingGameCharacter.CHARACTER_TEMPLATE.REQUIRED_PRICE;
                stFactorInfo.hobby = selectingGameCharacter.CHARACTER_TEMPLATE.HOBBY;
                stFactorInfo.factorTable = selectingGameCharacter.CHARACTER_TEMPLATE.FACTOR_TABLE;


    











                //selectingGameCharacter.CHARACTER_TEMPLATE.LIST_EXTRA_CLASS
                //selectingGameCharacter.CHARACTER_TEMPLATE.LIST_EXTRA_CLASS_PARAM_F            
                if (m_dicFactorInfo.ContainsKey(strcharkey) != true)
                    m_dicFactorInfo.Add(strcharkey, stFactorInfo);
            }


        }

        return m_dicFactorInfo;
    }
    //sjh Selecting Character Info
}
