using UnityEngine;
using System.Collections;
//모든 수치를 하나로 통합해주는 클래스
public class GameCharacter : BaseObject {
    double m_CurrentHP = 0;
    double m_DaySpd    = 0;
    double m_NightSpd  = 0;

    CharacterTemplateData m_TemplateData = null;
    CharacterFactorTable m_CharacterFactorTable = new CharacterFactorTable();
    //CharacterGrowTable m_CharacterGrowTable = new CharacterGrowTable();

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

    public void SetTemplate(CharacterTemplateData templateData)
    {

        m_TemplateData = templateData;

        // "CHARACTER" <- Key
        m_CharacterFactorTable.AddFactorTable(m_TemplateData.CHARACTER_KEY, m_TemplateData.FACTOR_TABLE);

        m_CurrentHP = CHARACTER_FACTOR.GetFactorData(eFactorData.HEALTH);
        m_DaySpd = CHARACTER_FACTOR.GetFactorData(eFactorData.DAY_SPEED);
        m_NightSpd = CHARACTER_FACTOR.GetFactorData(eFactorData.NIGHT_SPEED);
    }
}
