using UnityEngine;
using System.Collections;

public class UI_Loading_EventHandler : MonoBehaviour {

    [SerializeField]
    GameObject[] m_arrBackgroud = null;


    [SerializeField]
    UISlider m_Slider = null;

    [SerializeField]

    UILabel m_Label = null;

    void OnEnable()
    {
        int nRandom = Random.Range(0, m_arrBackgroud.Length);

        for(int i =0; i<m_arrBackgroud.Length; ++i)
        {
            if (i == nRandom)
                m_arrBackgroud[i].SetActive(true);

            else
                m_arrBackgroud[i].SetActive(false);

        }

    }


    public void SetValue(float fValue)
    {
        m_Slider.value = fValue;

        m_Label.text = string.Format("{0:f0} %", fValue * 100.0f); // f0 소숫점을 0번째자리까지 = 표현 x
    }
    
}
