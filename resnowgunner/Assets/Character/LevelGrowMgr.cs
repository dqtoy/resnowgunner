using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;

public class LevelGrowMgr : BaseMgr<LevelGrowMgr> {
    // EXP DATA
    Dictionary<string, List<LevelGrowTemplate>> m_dicLevelGrowTable = new Dictionary<string, List<LevelGrowTemplate>>();

    void Awake()
    {
        _LoadGrowTemplate("LEVEL_TABLE");
    }
    void _LoadGrowTemplate(string strFileName)
    {
        TextAsset textLevelGrowTemplate = Resources.Load(strFileName) as TextAsset;


        if (textLevelGrowTemplate != null)
        {
            JSONClass CharacterGrowDataNode = JSON.Parse(textLevelGrowTemplate.text) as JSONClass;
            if (CharacterGrowDataNode != null)
            {
                // 키에 따라 값을 재할당하고 싶다.
                JSONArray CharacterExpData_Character_1 = CharacterGrowDataNode["CHARACTER_1"] as JSONArray;
                

                foreach (KeyValuePair<string,JSONNode> keyValue in CharacterGrowDataNode)
                {
                    Debug.Log(keyValue);
                }



                foreach (KeyValuePair<string, JSONNode> keyValue in CharacterExpData_Character_1)
                {
                    LevelGrowTemplate _Character_1Temp = new LevelGrowTemplate(keyValue.Key);
                    List<LevelGrowTemplate> m_list = null;
                    if (m_dicLevelGrowTable.ContainsKey(keyValue.Key) == false)
                    {
                        // m_list == JSONNode 불일치
                        m_list = new List<LevelGrowTemplate>();
                        //Error1
                        /*LevelGrowTemplate levelGrowTemplate = LevelGrowTemplate(keyValue.Key);
                        m_list.Add(levelGrowTemplate);
                        m_dicLevelGrowTable.Add(keyValue.Key, m_list);*/
                    }
                    else
                    {
                        m_list.Add(_Character_1Temp);
                    }
                    m_list.Add(_Character_1Temp);
                }
            }
            /* 현재 위의 예전의 소스
            for (int i = 0; i < arrCharacter_1.Count; ++i)
            {
                LevelGrowTemplate _Character_1Temp = new LevelGrowTemplate(arrCharacter_1[i]);
                List<LevelGrowTemplate> m_list = null;
                LevelGrowTable _Character_1Temp = new LevelGrowTable(arrCharacter_1[i]);
                if (m_dicLevelGrowTable.ContainsKey(arrCharacter_1) == false)
                {
                    m_list = new List<LevelGrowTemplate>();
                    m_dicLevelGrowTemplate.Add(arrCharacter_1, m_list);
                }
                else
                {
                    m_list = m_dicLevelGrowTemplate[arrCharacter_1];
                }
                m_list.Add(_Character_1Temp);
            }*/

            /* 여자 아처(여성궁수)
            for (int i = 0; i < arrCharacter_2.Count; ++i)
            {
                LevelGrowTemplate _Character_2Temp = new LevelGrowTemplate(arrCharacter_2[i]);
                List<LevelGrowTemplate> m_list = null;
                if (m_dicLevelGrowTemplate.ContainsKey(arrCharacter_2) == false)
                {
                    m_list = new List<LevelGrowTemplate>();
                    m_dicLevelGrowTemplate.Add(arrCharacter_2, m_list);
                }
                else
                {
                    m_list = m_dicLevelGrowTemplate[arrCharacter_2];
                }
                m_list.Add(_Character_2Temp);
            }*/

            /* 존 거너
            for (int i = 0; i < arrCharacter_JOHN.Count; ++i)
            {
                LevelGrowTemplate _Character_JOHNTemp = new LevelGrowTemplate(arrCharacter_JOHN[i]);
                List<LevelGrowTemplate> m_list = null;
                if (m_dicLevelGrowTemplate.ContainsKey(arrCharacter_JOHN) == false)
                {
                    m_list = new List<LevelGrowTemplate>();
                    m_dicLevelGrowTemplate.Add(arrCharacter_JOHN, m_list);
                }
                else
                {
                    m_list = m_dicLevelGrowTemplate[arrCharacter_JOHN];
                }
                m_list.Add(_Character_JOHNTemp);
            }*/

            /* 마틴 거너
            for (int i = 0; i < arrCharacter_MARTHIN.Count; ++i)
            {
                LevelGrowTemplate _Character_MARTHINTemp = new LevelGrowTemplate(arrCharacter_MARTHIN[i]);
                List<LevelGrowTemplate> m_list = null;
                if (m_dicLevelGrowTemplate.ContainsKey(arrCharacter_MARTHIN) == false)
                {
                    m_list = new List<LevelGrowTemplate>();
                    m_dicLevelGrowTemplate.Add(arrCharacter_MARTHIN, m_list);
                }
                else
                {
                    m_list = m_dicLevelGrowTemplate[arrCharacter_MARTHIN];
                }
                m_list.Add(_Character_MARTHINTemp);
            }*/

            /*리차드 거너
            for (int i = 0; i < arrCharacter_RICHARD.Count; ++i)
            {
                LevelGrowTemplate _Character_RICHARDTemp = new LevelGrowTemplate(arrCharacter_RICHARD[i]);
                List<LevelGrowTemplate> m_list = null;
                if (m_dicLevelGrowTemplate.ContainsKey(arrCharacter_RICHARD) == false)
                {
                    m_list = new List<LevelGrowTemplate>();
                    m_dicLevelGrowTemplate.Add(arrCharacter_RICHARD, m_list);
                }
                else
                {
                    m_list = m_dicLevelGrowTemplate[arrCharacter_RICHARD];
                }
                m_list.Add(_Character_RICHARDTemp);
            }*/

            /* 로버트 거너
            for (int i = 0; i < arrCharacter_ROBERT.Count; ++i)
            {
                LevelGrowTemplate _Character_ROBERTTemp = new LevelGrowTemplate(arrCharacter_ROBERT[i]);
                List<LevelGrowTemplate> m_list = null;
                if (m_dicLevelGrowTemplate.ContainsKey(arrCharacter_ROBERT) == false)
                {
                    m_list = new List<LevelGrowTemplate>();
                    m_dicLevelGrowTemplate.Add(arrCharacter_ROBERT, m_list);
                }
                else
                {
                    m_list = m_dicLevelGrowTemplate[arrCharacter_ROBERT];
                }
                m_list.Add(_Character_ROBERTTemp);
            }*/

            /* 캐서린 마법사
            for (int i = 0; i < arrCharacter_CATHERINE.Count; ++i)
            {
                 LevelGrowTemplate _Character_CATHERINETemp = new LevelGrowTemplate(arrCharacter_CATHERINE[i]);
                 List<LevelGrowTemplate> m_list = null;
                 if (m_dicLevelGrowTemplate.ContainsKey(arrCharacter_CATHERINE) == false)
                 {
                     m_list = new List<LevelGrowTemplate>();
                     m_dicLevelGrowTemplate.Add(arrCharacter_CATHERINE, m_list);
                 }
                 else
                 {
                     m_list = m_dicLevelGrowTemplate[arrCharacter_CATHERINE];
                 }
                 m_list.Add(_Character_CATHERINETemp);
             }*/

        }
    }
    public List<LevelGrowTemplate> GetLevelGrowTemplate(string strName)
    {
        List<LevelGrowTemplate> LevelGrowData = null;
        m_dicLevelGrowTable.TryGetValue(strName, out LevelGrowData);
        return LevelGrowData;
    }
}
