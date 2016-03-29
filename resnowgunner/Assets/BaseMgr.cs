using UnityEngine;
using System.Collections;

public class BaseMgr<ManagerType> : MonoBehaviour where ManagerType : MonoBehaviour
{
    // Singleton BaseMgr 

        
    static GameObject s_Object = null;
    static ManagerType s_Manager = null;
    static bool m_bShutDown = false;


    void OnApplicationQuit()
    {
        m_bShutDown = true;
    }



    public static ManagerType Instance
    {
        get
        {
            if(s_Object == null)
            {
                if(m_bShutDown == false)
                {
                    s_Object = new GameObject();
                    s_Object.name = typeof(ManagerType).Name;

                    DontDestroyOnLoad(s_Object);
                    s_Manager = s_Object.AddComponent<ManagerType>();
                }
                
            }

            return s_Manager;

        }
    }

}
