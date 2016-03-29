using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class StageMgr : BaseMgr<StageMgr>
{
    Dictionary<int,List<StageTemplateData>> m_dicStageTemplate = new Dictionary<int,List<StageTemplateData>>();
    List<GameObject> StageList = new List<GameObject>();

    public List<GameObject> STAGE_LIST
    {
        get { return StageList; }
    }

    void Awake()
    {
        TextAsset sceneText = Resources.Load<TextAsset>("STATGE_TEMPLATE");
        

        if (sceneText != null)
        {
           JSONClass rootNode = JSON.Parse(sceneText.text) as JSONClass;
            
            JSONArray arrTemplate = rootNode["STAGE_TEMPLATE"] as JSONArray;                 
                     

            for (int i = 0; i < arrTemplate.Count; ++i)
            {
                int nEpisodeID =(arrTemplate[i])["EPISODE_ID"].AsInt;                
                StageTemplateData _stageTemp = new StageTemplateData(arrTemplate[i]);
                List<StageTemplateData> m_listTemplateData = null;

                if (m_dicStageTemplate.ContainsKey(nEpisodeID) == false)
                {
                    m_listTemplateData = new List<StageTemplateData>();
                    m_dicStageTemplate.Add(nEpisodeID, m_listTemplateData);

                }
                else
                {
                    m_listTemplateData = m_dicStageTemplate[nEpisodeID];
                }

                m_listTemplateData.Add(_stageTemp);

            }

        }
    }

    public bool isValidEpisode(int nEpisode)
    {
        return m_dicStageTemplate.ContainsKey(nEpisode);
    }
    

    public List<StageTemplateData> GetStageList(int nEpisode)
    {
        List<StageTemplateData> listTemplateData = null;
        m_dicStageTemplate.TryGetValue(nEpisode, out listTemplateData);
        return listTemplateData;
    }


    public StageTemplateData GetStage(int nEpisode, int nStage)
    {
        List<StageTemplateData> listTemplateData = GetStageList(nEpisode);
        for (int i =0; i<listTemplateData.Count; ++i)
        {
            if (listTemplateData[i].STAGE_ID == nStage)
                return listTemplateData[i];
        }
        return null;
    }


       
}
