using UnityEngine;
using System.Collections;

public class LevelTemplateData {
    string m_strLevelKey = string.Empty;

    public string LEVEL_KEY
    {
        get { return m_strLevelKey; }
    }
    // Min-Goo, 2016년 7월 17일 LevelTable의 속성 Lv => Level, Exp => 경험치 Diff => 이전 레벨과 경험치 차이
    LevelTable m_LevelTable = new LevelTable();
    public LevelTable LEVEL_TABLE { get { return m_LevelTable; } }
    public LevelTemplateData(string strkey, SimpleJSON.JSONNode nodeData)
    {
        m_strLevelKey = strkey;

        for (int i = 0; i < (int)eLevelData.COUNT; ++i)
        {
            eLevelData levelData = (eLevelData)i;
            int valueData = nodeData[levelData.ToString("F")].AsInt;
            if (valueData > 0)
                m_LevelTable.IncreaseData(levelData, valueData);
        }
    }
}
