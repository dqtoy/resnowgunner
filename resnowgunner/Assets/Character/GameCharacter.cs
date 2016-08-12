using UnityEngine;
using System.Collections;
//모든 수치를 하나로 통합해주는 클래스
public class GameCharacter : BaseObject {
    double m_CurrentHP = 0;
    double m_DaySpd    = 0;
    double m_NightSpd  = 0;

    CharacterTemplateData m_TemplateData = null;
    CharacterFactorTable m_CharacterFactorTable = new CharacterFactorTable();
    LevelGrowTable m_LevelTable = new LevelGrowTable();

    //grow
    //Skill
    /*List<SkillData> m_listNormalAttack = new List<SkillData>();
    List<SkillData> m_listSkill = new List<SkillData>();
    List<SkillData> m_listPassive = new List<SkillData>();
    public SkillData SELECT_SKILL
    {
        get;
        set;
    }*/

    public CharacterFactorTable CHARACTER_FACTOR { get { return m_CharacterFactorTable; } }
    public CharacterTemplateData CHARACTER_TEMPLATE { get { return m_TemplateData; } }
    public LevelGrowTable LEVEL_TEMPLATE { get { return m_LevelTable; } }
    public void SetTemplate(CharacterTemplateData templateData)
    {

        m_TemplateData = templateData;

        // "CHARACTER" <- Key
        m_CharacterFactorTable.AddFactorTable(eCharacterKey.CHARACTER_1, m_TemplateData.FACTOR_TABLE);
        //m_CharacterFactorTable.AddFactorTable("CHARACTER", m_TemplateData.FACTOR_TABLE);
        m_CurrentHP = CHARACTER_FACTOR.GetFactorData(eFactorData.HEALTH);
        m_DaySpd = CHARACTER_FACTOR.GetFactorData(eFactorData.DAY_SPEED);
        m_NightSpd = CHARACTER_FACTOR.GetFactorData(eFactorData.NIGHT_SPEED);
        //Error1
        //m_LevelTable = LevelGrowMgr.Instance.LEVEL_GROW_TABLE;
    }

    public void IncreaseCurrentHP(double valueData)
    {
        m_CurrentHP += valueData;
        if (m_CurrentHP < 0)
            m_CurrentHP = 0;

        if (m_CurrentHP > CHARACTER_FACTOR.GetFactorData(eFactorData.HEALTH))
            m_CurrentHP = CHARACTER_FACTOR.GetFactorData(eFactorData.HEALTH);

        if (m_CurrentHP == 0)
        {
            OBJECT_STATE = eBaseObjectState.STATE_DIE;
        }
    }
}
