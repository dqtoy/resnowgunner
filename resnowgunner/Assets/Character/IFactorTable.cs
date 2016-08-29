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
public sealed class IFactorTable {
    Dictionary<eLevelData, int> m_dicData = new Dictionary<eLevelData, int>();

    public void InitData()
    {
        m_dicData.Clear();
    }

    public void AddFullData(eLevelData elvData1, int valueData1, eLevelData elvData2, int valueData2, eLevelData elvData3, int valueData3)
    {
        m_dicData.Add(elvData1, valueData1);
        m_dicData.Add(elvData2, valueData2);
        m_dicData.Add(elvData3, valueData3);
    }

    public void Copy(IFactorTable ifactorTable)
    {
        foreach (KeyValuePair<eLevelData, int> keyValue in ifactorTable.m_dicData)
        {
            IncreaseData(keyValue.Key, keyValue.Value);
        }
    }

    // 증가
    public void IncreaseData(eLevelData ifactorData, int valueData)
    {
        int prevValue = 0;
        if (m_dicData.TryGetValue(ifactorData, out prevValue) == true)
        {
            m_dicData[ifactorData] = prevValue + valueData;
        }
        else
        {
            m_dicData.Add(ifactorData, valueData);
        }
    }
    // 감소
    public void DecreaseData(eLevelData ifactorData, int valueData)
    {
        int prevValue = 0;
        if (m_dicData.TryGetValue(ifactorData, out prevValue) == true)
        {
            m_dicData[ifactorData] = prevValue - valueData;
        }
    }

    public void SetData(eLevelData ifactorData, int valueData)
    {
        m_dicData[ifactorData] = valueData;
    }

    /*public void AddData(eLevelData ifactorData, int valueData)
    {
        m_dicData.Add(ifactorData, valueData);
    }*/

    public void RemoveData(eLevelData ifactorData)
    {
        m_dicData.Remove(ifactorData);
    }

    public int GetData(eLevelData ifactorData)
    {
        int valueData = 0;

        m_dicData.TryGetValue(ifactorData, out valueData);
        return valueData;
    }
}