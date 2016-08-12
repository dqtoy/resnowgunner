using UnityEngine;
using System.Collections;

public class LevelGrowTemplate
{
    public int LEVEL { get; set; }
    public int EXP { get; set; }
    public int DIFF { get; set; }

    public LevelGrowTemplate(SimpleJSON.JSONNode nodeData)
    {
        LEVEL = nodeData["LEVEL"].AsInt;
        EXP = nodeData["EXP"].AsInt;
        DIFF = nodeData["DIFF"].AsInt;
    }
}
