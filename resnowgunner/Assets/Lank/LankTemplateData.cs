using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LankTemplateData
{ 
    public int NUMBER { get; set; }
    public int LEVEL_NUMBER { get; set; }
    public string BACK_IMAGE { get; set; }
    public string STAR_IMAGE { get; set; }
    public string RATING_CLASS { get; set; }
    public string NAME { get; set; }
    public int SCORE { get; set; }
    public string SWITCH_ONOFF { get; set; }

    public LankTemplateData(SimpleJSON.JSONNode nodeData)
    {
        NUMBER = nodeData["NUMBER"].AsInt;
        LEVEL_NUMBER = nodeData["LEVEL_NUMBER"].AsInt;

        BACK_IMAGE = nodeData["BACK_IMAGE"];
        STAR_IMAGE = nodeData["STAR_IMAGE"];
        RATING_CLASS = nodeData["RATING_CLASS"];

        NAME = nodeData["NAME"];
        SCORE = nodeData["SCORE"].AsInt;

        SWITCH_ONOFF = nodeData["SWITCH_ONOFF"];
    }

}
