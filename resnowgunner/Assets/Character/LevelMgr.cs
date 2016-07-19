using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;
public class LevelMgr : BaseMgr<LevelMgr> {
    Dictionary<string, LevelTemplateData> m_dicTemplateData = new Dictionary<string, LevelTemplateData>();

    void Start () {
        //Min-Goo 2016년 7월 17일 오전 3시 56분 JSON Parsing
        //Min-Goo 2016년 7월 17일 JSON 파일 형식 읽어오기
        TextAsset levelText = Resources.Load<TextAsset>("LEVEL_TABLE");
        if (levelText != null)
        {
            JSONClass nodeData = JSON.Parse(levelText.text) as JSONClass; // Dictionary가 넘어온다.
            if (nodeData != null)
            {
                JSONClass levelInfoNode = nodeData["LEVEL_TABLE"] as JSONClass;
                if (levelInfoNode != null)
                {
                    foreach (KeyValuePair<string, JSONNode> keyValue in levelInfoNode)
                    {
                        m_dicTemplateData.Add(keyValue.Key, new LevelTemplateData(keyValue.Key, keyValue.Value));
                    }
                }
            }
        }

        Init();
    }

    void Init()
    {
    }
    // 초기화하기 위해 호출하는 함수
    public void LevelSetUp()
    {

    }
    // min
}
