using UnityEngine;
using System.Collections;

public class LevelGrowTemplate
{
    //일반 IFactorTable
    IFactorTable m_IFactorTable = new IFactorTable();

    public int LEVEL { get; set; }
    public int EXP { get; set; }
    public int DIFF { get; set; }

    public IFactorTable IFACTOR_TABLE { get { return m_IFactorTable; } }
    public LevelGrowTemplate(SimpleJSON.JSONNode nodeData)
    {
        LEVEL = nodeData["LEVEL"].AsInt;
        EXP = nodeData["EXP"].AsInt;
        DIFF = nodeData["DIFF"].AsInt;

        for (int i = 0; i < (int)eLevelData.COUNT; ++i)
        {
            eLevelData levelData = (eLevelData)i;
            int valueData = 0;
            if (nodeData[levelData.ToString("F")] != null)
            {
                valueData = nodeData[levelData.ToString("F")].AsInt;
            }

            if (valueData > 0)
                m_IFactorTable.IncreaseData(levelData, valueData);
        }
    }
}
