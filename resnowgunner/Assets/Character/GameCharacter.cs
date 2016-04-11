using UnityEngine;
using System.Collections;

public class GameCharacter : BaseObject {
    CharacterTemplateData m_TemplateData = null;

    public void SetTemplate(CharacterTemplateData templateData)
    {

        m_TemplateData = templateData;

        // "CHARACTER" <- Key
        //m_CharacterFactorTable.AddFactorTable("CHARACTER", m_TemplateData.FACTOR_TABLE);

        //m_CurrentHP = CHARACTER_FACTOR.GetFactorData(eFactorData.MAX_HP);
    }
}
