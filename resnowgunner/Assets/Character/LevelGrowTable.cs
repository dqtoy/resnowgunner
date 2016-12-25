using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class LevelGrowTable
{
    Dictionary<string, List<IFactorTable>> m_dicIFactor = new Dictionary<string, List<IFactorTable>>();
    
    // 모든 수치가 합쳐진 itotalfactor
    IFactorTable m_totalFactor = new IFactorTable();

    //itotalFactor 갱신용
    bool m_bRefresh = false;

    public void InitData()
    {
        m_dicIFactor.Clear();
    }

    

    // 레벨 초기화 데이터 키를 처음에 제거하면 안됨
    public void AddFactorTableData(Dictionary<string, List<IFactorTable>> dicifactor)
    {
        m_bRefresh = true;
        m_dicIFactor = dicifactor;
    }

    // strKey 는 임의이 캐릭터 key값... 레벨, EXP, 경험치 차이.
    public void AddFactorTable(string strKey, List<IFactorTable> ifactorTable)
    {
        m_dicIFactor.Remove(strKey);
        m_dicIFactor.Add(strKey, ifactorTable);
        m_bRefresh = true;
    }

    /* 원본
    public void AddFactorTable(string strKey, FactorTable factorTable)
    {
        m_dicFactor.Remove(strKey);
        m_dicFactor.Add(strKey, factorTable);
        m_bRefresh = true;
    }
    */

    public void RemoveFactorTable(string strKey)
    {
        m_dicIFactor.Remove(strKey);
        m_bRefresh = true;
    }

    public bool IsThisFactorData(string strkey)
    {
        return m_dicIFactor.ContainsKey(strkey);
    }

    public int GetFactorData(eLevelData ifactorData)
    {
        _RefreshTotalFactor();

        return m_totalFactor.GetData(ifactorData);
    }

    void _RefreshTotalFactor()
    {
        if (m_bRefresh == false)
            return;

        //초기화
        m_totalFactor.InitData();

        //대입
        foreach (KeyValuePair<string, List<IFactorTable>> keyValue in m_dicIFactor)
        {
            List<IFactorTable> table = keyValue.Value;
            for (int i = 0, imax = table.Count; i < imax; ++i)
            {
                m_totalFactor.Copy(table[i]);
            }   
        }
        m_bRefresh = false;
    }
}
