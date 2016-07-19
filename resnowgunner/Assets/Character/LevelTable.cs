using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public enum eLevelData
{
    LEVEL,
    EXP,
    DIFF,
    COUNT
}
public sealed class LevelTable {
    Dictionary<eLevelData, int> m_dicData = new Dictionary<eLevelData, int>();

    public void InitData()
    {
        m_dicData.Clear();
    }

    public void Copy(LevelTable levelTable)
    {
        foreach (KeyValuePair<eLevelData, int> keyValue in levelTable.m_dicData)
        {
            IncreaseData(keyValue.Key, keyValue.Value);
        }
    }
    public void IncreaseData(eLevelData levelData, int valueData)
    {
        int prevValue = 0;
        m_dicData.TryGetValue(levelData, out prevValue);

        m_dicData[levelData] = prevValue + valueData;
    }
    public void DecreaseData(eLevelData levelData, int valueData)
    {
        int prevValue = 0;
        m_dicData.TryGetValue(levelData, out prevValue);

        m_dicData[levelData] = prevValue - valueData;
    }

    public void SetData(eLevelData levelData, int valueData)
    {
        m_dicData[levelData] = valueData;
    }

    public void RemoveData(eLevelData levelData)
    {
        m_dicData.Remove(levelData);
    }

    public int GetFactorData(eLevelData levelData)
    {
        int valueData = 0;
        m_dicData.TryGetValue(levelData, out valueData);
        return valueData;
    }
}
