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
        //JSON 데이타 Lank

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
        // 레벨이 높은 순서대로 나오도록
        //m_listLankTemplate.Sort(delegate (LankTemplateData x, LankTemplateData y) {
        //return AscendingOrderCompare(x, y);
        //})
        m_listLankTemplate.Sort(new AscendingOrderNumber());

    }
    /// <summary>
    /// 오름차순
    /// </summary>
    public class AscendingOrderNumber : IComparer, IComparer<LankTemplateData>
    {
        public int Compare(LankTemplateData x, LankTemplateData y)
        {
            return y.LEVEL_NUMBER.CompareTo(x.LEVEL_NUMBER); //Level_Number로 구분해서 정렬한다.
        }
        public int Compare(object x, object y)
        {
            return Compare((LankTemplateData)y, (LankTemplateData)x);
        }
    }
    /// <summary>
    /// 내림차순
    /// </summary>
    public class DescendingOrderNumber : IComparer, IComparer<LankTemplateData>
    {
        public int Compare(LankTemplateData x, LankTemplateData y)
        {
            return x.LEVEL_NUMBER.CompareTo(y.LEVEL_NUMBER);   //Level_Number로 구분해서 정렬한다.
        }
        public int Compare(object x, object y)
        {
            return Compare((LankTemplateData)x, (LankTemplateData)y);
        }
    }

    /*public int AscendingOrderCompare(LankTemplateData x, LankTemplateData y)
    {
        if (x.LEVEL_NUMBER > y.LEVEL_NUMBER)
        {
            return 1;
        }
        else if (x.LEVEL_NUMBER < y.LEVEL_NUMBER)
        {
            return -1;
        }
        return 0;
    }*/
}
