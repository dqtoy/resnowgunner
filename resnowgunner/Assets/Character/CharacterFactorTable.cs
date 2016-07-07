using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public sealed class CharacterFactorTable
{
    Dictionary<string, FactorTable> m_dicFactor = new Dictionary<string, FactorTable>();

    // 모든 수치가 합쳐진 totalfactor

    FactorTable m_totalFactor = new FactorTable();
    //totalFactor 갱신용
    bool m_bRefresh = false;
    
    // strKey 는 임의이 key값... 아이템이름.. 종류 케릭터 등등 다양
    public void AddFactorTable(string strKey, FactorTable factorTable)
    {
        m_dicFactor.Remove(strKey);
        m_dicFactor.Add(strKey, factorTable);
        m_bRefresh = true;
    }

    public void RemoveFactorTable(string strKey)
    {
        m_dicFactor.Remove(strKey);
        m_bRefresh = true;
    }

    public double GetFactorData(eFactorData factorData)
    {
        _RefreshTotalFactor();
        return m_totalFactor.GetFactorData(factorData);
    }

    void _RefreshTotalFactor()
    {
        if (m_bRefresh == false)
            return;

        // 초기화
        m_totalFactor.InitData();

        // 대입
        foreach (KeyValuePair<string, FactorTable> keyValue in m_dicFactor)
        {
            FactorTable table = keyValue.Value;
            m_totalFactor.Copy(table);
        }

        m_bRefresh = false;
    }
}
