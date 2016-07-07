using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObserverMgr : BaseMgr<ObserverMgr> {

    Dictionary<eTeamType, List<Observer_Component>> m_dicObserver = new Dictionary<eTeamType, List<Observer_Component>>();

    public void AddObserver(Observer_Component addObserver)
    {
        List<Observer_Component> listObserver = null;
        eTeamType teamType = addObserver.TEAM_TYPE;
        if (m_dicObserver.ContainsKey(teamType) == false)
        {
            listObserver = new List<Observer_Component>();
            m_dicObserver.Add(teamType, listObserver);
        }

        else
        {
            m_dicObserver.TryGetValue(teamType, out listObserver);
        }

        listObserver.Add(addObserver);

    }


    public void RemoveObserver(Observer_Component removeObserver, bool bDelete = true)
    {
        eTeamType teamType = removeObserver.TEAM_TYPE;

        if (m_dicObserver.ContainsKey(teamType) == true)
        {
            List<Observer_Component> listObserver = null;
            m_dicObserver.TryGetValue(teamType, out listObserver);


            listObserver.Remove(removeObserver);
        }

        if (bDelete == true) // 객체까지 지우고 싶을 경우와 아닐경우를 구문.
        {
            Destroy(removeObserver.SelfObject);
        }

    }

    public BaseObject GetSearchEnemy(BaseObject _observer, float fRadious = 50.0f)
    {
        //eTeamType teamType = _observer.TEAM_TYPE;

        //Boxing - UnBoxing object -> enum
        eTeamType teamType = (eTeamType)_observer.GetData("TEAM");

        Vector3 myPosition = _observer.SelfTransform.position;

        float fNearDistance = fRadious;
        Observer_Component nearObserver = null;

        foreach (KeyValuePair<eTeamType, List<Observer_Component>> keyValue in m_dicObserver)
        {
            if (keyValue.Key == teamType)
                continue;

            for (int i = 0; i < keyValue.Value.Count; ++i)
            {
                if (keyValue.Value[i].OBJECT_STATE == eBaseObjectState.STATE_DIE)
                    continue;

                if (keyValue.Value[i].SelfObject.activeSelf == false)
                    continue;


                float fDistance = Vector3.Distance(myPosition, keyValue.Value[i].SelfTransform.position);
                if (fDistance <= fNearDistance)
                {
                    fNearDistance = fDistance;
                    nearObserver = keyValue.Value[i];
                }
            }
        }

        return nearObserver;


    }



    //0116 jj tempkey를 이용하여 캐릭터 찾기
    public BaseObject GetCharacter(string tempKey)
    {
        Observer_Component CHARACTER = null;

        foreach (KeyValuePair<eTeamType, List<Observer_Component>> keyValue in m_dicObserver)
        {
            for (int i = 0; i < keyValue.Value.Count; ++i)
            {
                if (keyValue.Value[i].TEMP_KEY == tempKey)
                    CHARACTER = keyValue.Value[i];
            }
        }

        return CHARACTER;

    }

    public void SendAllMessage(string Messages, params object[] datas)
    {
        //Dictionary<eTeamType, List<Observer_Component>>.Enumerator enumerator = m_dicObserver.GetEnumerator();

        List<Observer_Component> listObserverComponent = null;
        for (int i = 0; i <= (int)(eTeamType.TEAM_2); ++i)
        {
            if (m_dicObserver.TryGetValue((eTeamType)(i), out listObserverComponent) == true)
            {
                List<Observer_Component>.Enumerator enumerator = listObserverComponent.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    enumerator.Current.ThrowEvent(Messages, datas);
                }
            }
        }
    }


    public void SendTeamMessage(eTeamType TeamType, string Messages, params object[] datas)
    {
        List<Observer_Component> listTeamObserver = null;
        //Dictionary<eTeamType, List<Observer_Component>>.Enumerator enumerator = m_dicObserver.GetEnumerator();

        if (m_dicObserver.TryGetValue(TeamType, out listTeamObserver) == true)
        {
            List<Observer_Component>.Enumerator enumerator = listTeamObserver.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerator.Current.ThrowEvent(Messages, datas);
            }
        }
    }
}
