using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public sealed class CharacterFactorTable
{
    Dictionary<eCharacterKey, FactorTable> m_dicFactor = new Dictionary<eCharacterKey, FactorTable>();
    
    // 모든 수치가 합쳐진 totalfactor

    FactorTable m_totalFactor = new FactorTable();
    //totalFactor 갱신용
    bool m_bRefresh = false;
    
    public void AddFactorTable(eCharacterKey theCharacterKey, FactorTable factorTable)
    {
        m_dicFactor.Remove(theCharacterKey);
        m_dicFactor.Add(theCharacterKey, factorTable);
        m_bRefresh = true;
    }
    public void RemoveFactorTable(eCharacterKey theCharacterKey)
    {
        m_dicFactor.Remove(theCharacterKey);
        m_bRefresh = true;
    }

    public double GetFactorData(eFactorData factorData)
    {
        _RefreshTotalFactor();
        return m_totalFactor.GetData(factorData);
    }

    void _RefreshTotalFactor()
    {
        if (m_bRefresh == false)
            return;

        // 초기화
        m_totalFactor.InitData();

        // 대입
        foreach (KeyValuePair<eCharacterKey, FactorTable> keyValue in m_dicFactor)
        {
            FactorTable table = keyValue.Value;
            m_totalFactor.Copy(table);
        }

        m_bRefresh = false;
    }
}
