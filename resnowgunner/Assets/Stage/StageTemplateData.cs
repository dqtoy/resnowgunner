using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class StageTemplateData
{

    //
    //      난이도 추가시 변수를 추가해서.. 
    List<string> m_Itemlist = new List<string>();
    public int EPISODE_ID { get; set; }
    public int STAGE_ID { get; set; }
    public string SCENE_NAME { get; set; }
    public string SPAWN_ID { get; set; }
    public string MAP_ID { get; set; }

    public int STAGE_EXP { get; set; }
    public List<string> ITEM_LIST { get { return m_Itemlist; } }

    public int GOLD { get; set; }


    public StageTemplateData(SimpleJSON.JSONNode nodeData)
    {
        EPISODE_ID = nodeData["EPISODE_ID"].AsInt;
        STAGE_ID = nodeData["STAGE_ID"].AsInt;

        SCENE_NAME = nodeData["SCENE_NAME"];
        SPAWN_ID = nodeData["SPAWN_ID"];
        MAP_ID = nodeData["MAP_ID"];

        STAGE_EXP = nodeData["STAGE_EXP"].AsInt;
        GOLD = nodeData["GOLD"].AsInt;

        SimpleJSON.JSONArray arrItemlist = nodeData["ITEM_LIST"].AsArray;


        if(arrItemlist!=null)
        {
            for (int i = 0; i < arrItemlist.Count; ++i)
            {
                m_Itemlist.Add(arrItemlist[i]);
            }
        }



    }

}
