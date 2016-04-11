using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum eCharacterType
{
    CHARACTER_TYPE_NONE,
    CHARACTER_TYPE_MAIN,
    CHARACTER_TYPE_ASSIST,
    CHARACTER_TYPE_FRIEND,
    CHARACTER_TYPE_ENEMY,
    CHARACTER_TYPE_COUNT,
}
public enum eRaceType
{
    RACE_TYPE_NONE,
    RACE_TYPE_HUMAN,
    RACE_TYPE_ELF,
    RACE_TYPE_WORGEN,
}
public class CharacterTemplateData
{
    eCharacterType m_CharacterType = eCharacterType.CHARACTER_TYPE_NONE;
    eRaceType m_RaceType = eRaceType.RACE_TYPE_NONE;
    // m_FactorTable에 Json으로 기본 수치를 받아놓을것 임

    string m_strKey   = string.Empty;
    string m_strName  = string.Empty;

    int   m_nHealth   = 0;
    float m_fDaySpd   = 0.0f;
    float m_fNightSpd = 0.0f;

    int m_nRequiredLv  = 0;
    bool m_bPurchasing = false;

    string m_strHobby    = string.Empty;
    int m_nRequiredPrice = 0;

    List<string> m_listExtraClass = new List<string>();
    List<float> m_flistExtraClassParam = new List<float>();
    
    public List<string> LIST_EXTRA_CLASS { get { return m_listExtraClass; } }
    public List<float> LIST_EXTRA_CLASS_PARAM_F { get { return m_flistExtraClassParam; } }



    public CharacterTemplateData(string strkey, SimpleJSON.JSONNode nodeData)
    {
        m_strKey = strkey;

        //"CHARACTER_TYPE": 1
        //"NAME": "캐릭터1"
        m_CharacterType = (eCharacterType)(nodeData["CHARACTER_TYPE"].AsInt);
        m_strName = nodeData["NAME"];
        //m_strPrefabName = nodeData["PREFAB_NAME"];

        FindRace(nodeData["RACE"].Value);

        m_nHealth   = nodeData["HEALTH"].AsInt;
        m_fDaySpd   = nodeData["DAY_SPEED"].AsFloat;
        m_fNightSpd = nodeData["NIGHT_SPEED"].AsFloat;
        //PLUS_EXP
        SimpleJSON.JSONArray arrExtraClass = nodeData["EXTRA"].AsArray;
        SimpleJSON.JSONArray arrExtraClassParam = nodeData["EXTRA_PARAM"].AsArray;

        m_nRequiredLv    = nodeData["LEVEL"].AsInt;
        m_bPurchasing    = nodeData["PURCHASING"] == "Y" ? true : false;

        m_strHobby       = nodeData["HOBBY"];
        m_nRequiredPrice = nodeData["PRICE"].AsInt;

        if (arrExtraClass != null)
        {
            for (int i = 0; i < m_listExtraClass.Count; ++i)
            {
                m_listExtraClass.Add(arrExtraClass[i]);
            }
        }

        if (arrExtraClassParam != null)
        {
            for (int i = 0; i < m_flistExtraClassParam.Count; ++i)
            {
                m_flistExtraClassParam.Add(arrExtraClassParam[i].AsFloat);
            }
        }
    }

    void FindRace(string RaceType)
    {
        
        switch (RaceType)
        {
            case "HUMAN":
                m_RaceType = eRaceType.RACE_TYPE_HUMAN;
                break;
            case "ELF":
                m_RaceType = eRaceType.RACE_TYPE_ELF;
                break;
            case "WORGEN":
                m_RaceType = eRaceType.RACE_TYPE_WORGEN;
                break;
            default:
                m_RaceType = eRaceType.RACE_TYPE_NONE;
                break;
        }

    }
}
