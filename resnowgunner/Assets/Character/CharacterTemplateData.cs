using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum eCharacterKey
{
    //CHARACTER_KEY_NONE, // 0
    CHARACTER_1,        // 1
    CHARACTER_2,        // 2
    CHARACTER_JOHN,     // 3
    CHARACTER_MARTIN,   // 4
    CHARACTER_RICHARD,  // 5
    CHARACTER_ROBERT,   // 6
    CHARACTER_CATHERINE,// 7
    CHARACTER_COUNT,
}
public enum eCharacterType
{
    //CHARACTER_TYPE_NONE,  // 0
    CHARACTER_TYPE_MAIN,  // 1
    CHARACTER_TYPE_ASSIST,// 2
    CHARACTER_TYPE_FRIEND,// 3
    CHARACTER_TYPE_ENEMY, // 4
    CHARACTER_TYPE_COUNT,
}
public enum eRaceType
{
    //RACE_TYPE_NONE,
    RACE_TYPE_HUMAN,
    RACE_TYPE_ELF,
    RACE_TYPE_WORGEN,
}

public class CharacterTemplateData
{
    string m_strKey = string.Empty;

    public string KEY
    {
        get { return m_strKey; }
    }

    //KEY eCharacterKey
    eCharacterKey m_CharacterKey = eCharacterKey.CHARACTER_1;
    //TYPE 캐릭터 타입 CHARACTER_TYPE_MAIN 1
    eCharacterType m_CharacterType;
    
    public eCharacterType CHARACTER_TYPE
    {
        get { return m_CharacterType; }
    }
    public eCharacterKey CHARACTER_KEY
    {
        get { return m_CharacterKey; }
    }

    // m_FactorTable에 Json으로 기본 수치를 받아놓을것임 
    FactorTable m_FactorTable = new FactorTable();
    public FactorTable FACTOR_TABLE { get { return m_FactorTable; } }

    //LevelTable m_LevelTable = new LevelTable();
    //public LevelTable LEVEL_TABLE { get { return m_LevelTable; } }

    string m_strName  = string.Empty;
    string m_strPrefabName = string.Empty;

    public string NAME
    {
        get { return m_strName; }
    }
    public string PREFAB_NAME
    {
        get { return m_strPrefabName; }
    }

    //RACE eRaceType RACE_TYPE_HUMAN
    eRaceType m_RaceType;
    public eRaceType RACE_TYPE
    {
        get { return m_RaceType; }
    }
    /*
    int   m_nHealth   = 0;
    float m_fDaySpd   = 0.0f;
    float m_fNightSpd = 0.0f;
    */
    /*
    public int HEALTH
    {
        get { return m_nHealth;  }
    }

    public float DAYSPD
    {
        get { return m_fDaySpd; }
    }

    public float NIGHTSPD
    {
        get { return m_fNightSpd; }
    }
    */
    int m_nRequiredLv  = 0;
    bool m_bPurchasing = false;

    public int REQUIRED_LEVEL
    {
        get { return m_nRequiredLv; }
        set { m_nRequiredLv = value; }
    }
    public bool IS_PURCHASING
    {
        get { return m_bPurchasing; }
        set { m_bPurchasing = value; }
    }

    string m_strHobby    = string.Empty;
    int m_nRequiredPrice = 0;

    public string HOBBY
    {
        get { return m_strHobby; }
    }
    public int REQUIRED_PRICE
    {
        get { return m_nRequiredPrice; }
    }

    List<string> m_listExtraClass = new List<string>();
    List<float> m_flistExtraClassParam = new List<float>();
    
    public List<string> LIST_EXTRA_CLASS { get { return m_listExtraClass; } }
    public List<float> LIST_EXTRA_CLASS_PARAM_F { get { return m_flistExtraClassParam; } }

    public CharacterTemplateData(string strkey, SimpleJSON.JSONNode nodeData)
    {
        m_strKey = strkey;

        //"CHARACTER_TYPE": 1, CHARACTER_TYPE_MAIN
        //"CHARACTER_KEY" : CHARACTER_1
        //"NAME"          : "명랑한 헨리군"

        m_CharacterKey = (eCharacterKey)Enum.Parse(typeof(eCharacterKey), strkey.ToString(), true);
        m_CharacterType = (eCharacterType)(nodeData["CHARACTER_TYPE"].AsInt);

        for (int i = 0; i < (int)eFactorData.COUNT; ++i)
        {
            eFactorData factorData = (eFactorData)i;
            double valueData = nodeData[factorData.ToString("F")].AsDouble;
            if (valueData > 0)
                m_FactorTable.IncreaseData(factorData, valueData);
        }


        //m_LevelTable = LevelMgr.Instance.LEVEL_TEMPLATE[m_strKey].LEVEL_TABLE;
        m_strName = nodeData["NAME"];
        m_strPrefabName = nodeData["PREFAB_NAME"];

        FindRace(nodeData["RACE"].Value);

        //m_nHealth   = nodeData["HEALTH"].AsInt;
        //m_fDaySpd   = nodeData["DAY_SPEED"].AsFloat;
        //m_fNightSpd = nodeData["NIGHT_SPEED"].AsFloat;
        //PLUS_EXP
        SimpleJSON.JSONArray arrExtraClass = nodeData["EXTRA"].AsArray;
        SimpleJSON.JSONArray arrExtraClassParam = nodeData["EXTRA_PARAM"].AsArray;
        // 구매가능레벨 ↓
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
            /*default:
                m_RaceType = eRaceType.RACE_TYPE_NONE;
                break;
                */
        }

    }
}
