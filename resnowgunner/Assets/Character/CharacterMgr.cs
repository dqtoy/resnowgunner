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
    public string Name;
    public string Health;
    public string DaySpd;
    public string NightSpd;
}
public class CharacterMgr : BaseMgr<CharacterMgr>
{
    public string m_temp = string.Empty;

    Dictionary<string, CharacterTemplateData> m_dicTemplateData = new Dictionary<string, CharacterTemplateData>();

    //sjh 배치Character정보 - Server로 보내는 Data로 연계시켜야함.
    List<string> m_listSelectingCharacter = new List<string>();

    public List<string> SELECTING_CHARACTER
    {
        get { return m_listSelectingCharacter; }
        set { m_listSelectingCharacter = value; }
    }
    // Use this for initialization
    void Start ()
    {
        //Min-Goo 2016년 4월 11일 오전 3시 56분 JSON Parsing
        TextAsset chatex = Resources.Load<TextAsset>("CHARIC_TEMPL");

        if (chatex != null)
        {
            JSONClass chajson = JSON.Parse(chatex.text) as JSONClass;

            if (chajson != null)
            {
                JSONClass chatemple = chajson["CHARIC_TEMPLATE"] as JSONClass;
                foreach (KeyValuePair<string, JSONNode> keyValue in chatemple)
                {
                    m_dicTemplateData.Add(keyValue.Key, new CharacterTemplateData(keyValue.Key, keyValue.Value));
                }
            }
        }
        //sjh ~JSON Parsing

        //Init();
    }

    /*public GameCharacter AddCharacter(string strTemplateKey)
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
   
    }*/

    // Update is called once per frame
    void Update () {
	
	}
}
