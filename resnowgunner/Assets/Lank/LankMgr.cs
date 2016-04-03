using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class LankMgr : BaseMgr<LankMgr>
{
    List<LankTemplateData> m_listLankTemplate = new List<LankTemplateData>();

    void Awake()
    {
        TextAsset sceneText = Resources.Load<TextAsset>("Lank");


        if (sceneText != null)
        {
            JSONClass rootNode = JSON.Parse(sceneText.text) as JSONClass;

            JSONArray arrTemplate = rootNode["USERINFO_TEMPLATE"] as JSONArray;


            for (int i = 0; i < arrTemplate.Count; ++i)
            {
                int nNUMBER = (arrTemplate[i])["NUMBER"].AsInt;
                LankTemplateData _lankTemp = new LankTemplateData(arrTemplate[i]);
                m_listLankTemplate.Add(_lankTemp);
            }
        }
    }

    public List<LankTemplateData> GetLank()
    {
        List<LankTemplateData> _templateData = null;
        _templateData = m_listLankTemplate;
        return _templateData;        
    }

    public void Sort()
    {

        m_listLankTemplate.Sort(delegate (LankTemplateData x, LankTemplateData y) {
            if (x.LEVEL_NUMBER.CompareTo(y.LEVEL_NUMBER) < 0)
            {
                // 레벨이 높은 순서대로 나오도록
                return -1;
            }
            return x.LEVEL_NUMBER.CompareTo(y.LEVEL_NUMBER);
        });

    }
}
