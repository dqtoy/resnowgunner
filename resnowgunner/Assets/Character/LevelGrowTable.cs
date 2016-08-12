using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class LevelGrowTable
{
    Dictionary<string, IFactorTable> m_dicIFactor = new Dictionary<string, IFactorTable>();
    // 원래 계획 현재는 EMPTY 비어있게끔...
    Dictionary<string, List<eLevelData>> m_dicLevelData = new Dictionary<string, List<eLevelData>>();
    
    // 모든 수치가 합쳐진 itotalfactor
    IFactorTable m_totalFactor = new IFactorTable();

    public void InitData()
    {
        m_dicIFactor.Clear();
        //m_dicLevelData.Clear();
    }

    //itotalFactor 갱신용
    bool m_bRefresh = false;

    // strKey 는 임의이 key값... 아이템이름.. 종류 케릭터 등등 다양
    public void AddFactorTable(string strKey, IFactorTable ifactorTable)
    {
        m_dicIFactor.Remove(strKey);
        m_dicIFactor.Add(strKey, ifactorTable);
        m_bRefresh = true;
    }

    public void RemoveFactorTable(string strKey)
    {
        m_dicIFactor.Remove(strKey);

        m_bRefresh = true;
    }

    public bool IsThisFactorData(string strkey)
    {
        return m_dicIFactor.ContainsKey(strkey);
    }

    public double GetFactorData(eLevelData ifactorData)
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
        foreach (KeyValuePair<string, IFactorTable> keyValue in m_dicIFactor)
        {
            IFactorTable table = keyValue.Value;
            m_totalFactor.Copy(table);
        }

        m_bRefresh = false;
    }
}
