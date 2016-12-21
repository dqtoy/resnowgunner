using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;

public class LevelGrowMgr : BaseMgr<LevelGrowMgr> {
    // EXP DATA
    Dictionary<string, List<LevelGrowTemplate>> m_dicLevelGrowTemp = new Dictionary<string, List<LevelGrowTemplate>>();
    Dictionary<string, LevelGrowTable> m_dicLevelGrowTable = new Dictionary<string, LevelGrowTable>();

    void Awake()
    {
        _LoadGrowTemplate("LEVEL_TABLE");
        _LoadGrowTable();
    }
    void _LoadGrowTemplate(string strFileName)
    {
        TextAsset textLevelGrowTemplate = Resources.Load(strFileName) as TextAsset;

        if (textLevelGrowTemplate != null)
        {
            // 텍스트 파일을 읽어올 것입니다.
            JSONClass CharacterGrowDataNode = JSON.Parse(textLevelGrowTemplate.text) as JSONClass;
            if (CharacterGrowDataNode != null)
            {
                // 키에 따라 값을 재할당하고 싶다.
                JSONArray arrCharacter_1 = CharacterGrowDataNode["CHARACTER_1"] as JSONArray;
                JSONArray arrCharacter_2 = CharacterGrowDataNode["CHARACTER_2"] as JSONArray;
                JSONArray arrCharacter_JOHN = CharacterGrowDataNode["CHARACTER_JOHN"] as JSONArray;
                JSONArray arrCharacter_MARTHIN = CharacterGrowDataNode["CHARACTER_MARTIN"] as JSONArray;
                JSONArray arrCharacter_RICHARD = CharacterGrowDataNode["CHARACTER_RICHARD"] as JSONArray;
                JSONArray arrCharacter_ROBERT = CharacterGrowDataNode["CHARACTER_ROBERT"] as JSONArray;
                JSONArray arrCharacter_CATHERINE = CharacterGrowDataNode["CHARACTER_CATHERINE"] as JSONArray;

                // 기본 캐릭터
                if (arrCharacter_1 != null)
                {
                    List<LevelGrowTemplate> m_list = new List<LevelGrowTemplate>();
                    for (int i = 0, imax = arrCharacter_1.Count; i < imax; ++i)
                    {
                        LevelGrowTemplate _template = new LevelGrowTemplate(arrCharacter_1[i]);
                        m_list.Add(_template);
                        Debug.Log("LEVEL = " + _template.LEVEL + ", EXP = " + _template.EXP + ", DIFF = " + _template.DIFF);
                    }
                    m_dicLevelGrowTemp.Add("CHARACTER_1", m_list);
                }

                // 여자 아처(여성궁수)
                if (arrCharacter_2 != null)
                {
                    List<LevelGrowTemplate> m_list = new List<LevelGrowTemplate>();
                    for (int i = 0, imax = arrCharacter_2.Count; i < imax; ++i)
                    {
                        LevelGrowTemplate _template = new LevelGrowTemplate(arrCharacter_2[i]);
                        m_list.Add(_template);
                        Debug.Log("LEVEL = " + _template.LEVEL + ", EXP = " + _template.EXP + ", DIFF = " + _template.DIFF);
                    }
                    m_dicLevelGrowTemp.Add("CHARACTER_2", m_list);
                }
                // 존 거너
                if (arrCharacter_JOHN != null)
                {
                    List<LevelGrowTemplate> m_list = new List<LevelGrowTemplate>();
                    for (int i = 0, imax = arrCharacter_JOHN.Count; i < imax; ++i)
                    {
                        LevelGrowTemplate _template = new LevelGrowTemplate(arrCharacter_JOHN[i]);
                        m_list.Add(_template);
                        Debug.Log("LEVEL = " + _template.LEVEL + ", EXP = " + _template.EXP + ", DIFF = " + _template.DIFF);
                    }
                    m_dicLevelGrowTemp.Add("CHARACTER_JOHN", m_list);
                }
                // 마틴 거너
                if (arrCharacter_MARTHIN != null)
                {
                    List<LevelGrowTemplate> m_list = new List<LevelGrowTemplate>();
                    for (int i = 0, imax = arrCharacter_MARTHIN.Count; i < imax; ++i)
                    {
                        LevelGrowTemplate _template = new LevelGrowTemplate(arrCharacter_MARTHIN[i]);
                        m_list.Add(_template);
                        Debug.Log("LEVEL = " + _template.LEVEL + ", EXP = " + _template.EXP + ", DIFF = " + _template.DIFF);
                    }
                    m_dicLevelGrowTemp.Add("CHARACTER_MARTHIN", m_list);
                }
                // 리차드 거너
                if (arrCharacter_RICHARD != null)
                {
                    List<LevelGrowTemplate> m_list = new List<LevelGrowTemplate>();
                    for (int i = 0, imax = arrCharacter_RICHARD.Count; i < imax; ++i)
                    {
                        LevelGrowTemplate _template = new LevelGrowTemplate(arrCharacter_RICHARD[i]);
                        m_list.Add(_template);
                        Debug.Log("LEVEL = " + _template.LEVEL + ", EXP = " + _template.EXP + ", DIFF = " + _template.DIFF);
                    }
                    m_dicLevelGrowTemp.Add("CHARACTER_RICHARD", m_list);
                }
                // 로버트 거너
                if (arrCharacter_ROBERT != null)
                {
                    List<LevelGrowTemplate> m_list = new List<LevelGrowTemplate>();
                    for (int i = 0, imax = arrCharacter_ROBERT.Count; i < imax; ++i)
                    {
                        LevelGrowTemplate _template = new LevelGrowTemplate(arrCharacter_ROBERT[i]);
                        m_list.Add(_template);
                        Debug.Log("LEVEL = " + _template.LEVEL + ", EXP = " + _template.EXP + ", DIFF = " + _template.DIFF);
                    }
                    m_dicLevelGrowTemp.Add("CHARACTER_ROBERT", m_list);
                }
                // 캐서린 마법사
                if (arrCharacter_CATHERINE != null)
                {
                    List<LevelGrowTemplate> m_list = new List<LevelGrowTemplate>();
                    for (int i = 0, imax = arrCharacter_CATHERINE.Count; i < imax; ++i)
                    {
                        LevelGrowTemplate _template = new LevelGrowTemplate(arrCharacter_CATHERINE[i]);
                        m_list.Add(_template);
                        Debug.Log("LEVEL = " + _template.LEVEL + ", EXP = " + _template.EXP + ", DIFF = " + _template.DIFF);
                    }
                    m_dicLevelGrowTemp.Add("CHARACTER_CATHERINE", m_list);
                }
                /* JSONArray 전체 출력
                foreach (KeyValuePair<string,JSONNode> keyValue in CharacterGrowDataNode)
                {
                    Debug.Log(keyValue);
                    List<LevelGrowTemplate> m_list = null;
                    if(m_dicLevelGrowTable.TryGetValue(keyValue.Key,out m_list )==false)
                    {
                        m_list = new List<LevelGrowTemplate>();
                        for (int i = 0, imax = keyValue.Value.Count; i < imax; ++i)
                        {
                            LevelGrowTemplate _template = new LevelGrowTemplate(keyValue.Value[i]);
                            m_list.Add(_template);
                        }
                        m_dicLevelGrowTemp.Add(keyValue.Key, m_list);
                    }
                    else
                    {
                        for (int i = 0, imax = keyValue.Value.Count; i < imax; ++i)
                        {
                            LevelGrowTemplate _template = new LevelGrowTemplate(keyValue.Value[i]);
                            if (m_list.Contains(_template) == false)
                                m_list.Add(_template);
                        }
                    }
                }*/
                Debug.Log(m_dicLevelGrowTemp);
            }
        }
    }
    public void _LoadGrowTable()
    {
        foreach (KeyValuePair<string, List<LevelGrowTemplate>> item in m_dicLevelGrowTemp)
        {
            List<LevelGrowTemplate> m_growlist = null;
            m_dicLevelGrowTemp.TryGetValue(item.Key, out m_growlist);
            LevelGrowTable levelgrowtable = new LevelGrowTable();
            List<IFactorTable> ifactortableList = new List<IFactorTable>();
            for (int i = 0, imax = m_growlist.Count; i < imax; ++i)
            {
                IFactorTable ifactorTableData = new IFactorTable();
                
                ifactorTableData.AddFullData(eLevelData.LEVEL, m_growlist[i].LEVEL, eLevelData.EXP, m_growlist[i].EXP, eLevelData.DIFF, m_growlist[i].DIFF);
                ifactortableList.Add(ifactorTableData);
                ifactorTableData.InitData();
            }
            levelgrowtable.AddFactorTable(item.Key, ifactortableList);
            m_dicLevelGrowTable.Add(item.Key, levelgrowtable);
        }
    }
    public List<LevelGrowTemplate> GetLevelGrowTemplate(string strName)
    {
        List<LevelGrowTemplate> LevelGrowData = null;
        m_dicLevelGrowTemp.TryGetValue(strName, out LevelGrowData);
        return LevelGrowData;
    }

    public LevelGrowTable GetLevelGrowTable(string strName)
    {
        LevelGrowTable growTable = null;
        m_dicLevelGrowTable.TryGetValue(strName, out growTable);
        return growTable;
    }
}
