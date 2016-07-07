using UnityEngine;
using System.Collections;
//모든 수치를 하나로 통합해주는 클래스
public class GameCharacter : BaseObject { 
    double m_AtkSpd = 0;
    double m_Attack = 0;

    CharacterTemplateData m_TemplateData = null;
    CharacterFactorTable m_CharacterFactorTable = new CharacterFactorTable();
    //CharacterGrowTable m_CharacterGrowTable = new CharacterGrowTable();
    
    public CharacterFactorTable CHARACTER_FACTOR { get { return m_CharacterFactorTable; } }
    public CharacterTemplateData CHARACTER_TEMPLATE { get { return m_TemplateData; } }
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
    public void SetTemplate(CharacterTemplateData templateData)
    {

        m_TemplateData = templateData;

        // "CHARACTER" <- Key
        m_CharacterFactorTable.AddFactorTable("CHARACTER", m_TemplateData.FACTOR_TABLE);

        //m_CurrentHP = CHARACTER_FACTOR.GetFactorData(eFactorData.MAX_HP);
    }
}
