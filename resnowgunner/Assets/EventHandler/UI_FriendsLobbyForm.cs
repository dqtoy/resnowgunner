using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eUIFriendsLobbyGridType
{

}

public class UI_FriendsLobbyForm : UIFormObject {


    //Dictionary<키, Value> m_dic = new Dictionary<키, Value>();
   //[SerializeField]
    Dictionary<eUIFriendsLobbyGridType, UIGridObject> m_UIGridObjects = new Dictionary<eUIFriendsLobbyGridType, UIGridObject>();
    public Dictionary<eUIFriendsLobbyGridType, UIGridObject> MyGrid
    {
        get { return m_UIGridObjects; }
    }

    [SerializeField]
    UIGridObject m_UIGridObject;

    [SerializeField]
    List<UIGridObject> myobjestlists;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
