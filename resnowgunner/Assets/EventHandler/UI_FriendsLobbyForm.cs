using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eUIFriendsLobbyGridType
{

}
public class M_GunnerCharacterInfo
{
    public string C_Name;//0
    public string C_NamePrefab;//1
    public int C_Level;//2
    public int C_Health;//3
    public string C_Explanation;//4
    public int C_Price;//5
    public string C_PurchaseButton;//6
}
public class UI_FriendsLobbyForm : UIFormObject {

    [SerializeField]
    UIGridObject m_SelectGrid;
    [SerializeField]
    UIGridObject m_RankGrid;
    [SerializeField]
    UIEmptyObject m_SettingMenu;
    [SerializeField]
    UIEmptyObject m_TermView;
    [SerializeField]
    UISpriteObject m_DarkBG;
    [SerializeField]
    string bundleVersion = "0.0.3";
    [SerializeField]
    UIEmptyObject m_LobbyView;
    [SerializeField]
    UISpriteObject m_Player_Background;

    UILabel VersionLabel;

    List<M_GunnerCharacterInfo> Gunners = new List<M_GunnerCharacterInfo>();

    [SerializeField]
    GameObject CharacterPrefab;
    [SerializeField]
    GameObject LankPrefab;

    int lastWidth;
    int lastHeight;
    bool stay = true;
    void Start()
    {
        VersionLabel = m_SettingMenu.SelfTransform.FindChild("Version Label").GetComponent<UILabel>();
        VersionLabel.text = "게임 버젼 : " + bundleVersion;
        LoadCharacter();
        LoadRanking();

        StartCoroutine(check_for_resize());
    }
    IEnumerator check_for_resize()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;

        while (stay)
        {
            if (lastWidth != Screen.width || lastHeight != Screen.height)
            {
                arrangeItems();
                lastWidth = Screen.width;
                lastHeight = Screen.height;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void arrangeItems()
    {
        UIPanel panel = m_RankGrid.SelfUIGrid.transform.parent.GetComponent<UIPanel>();
        foreach (Transform item in m_RankGrid.SelfUIGrid.transform)
        {
            UIWidget widget = item.GetComponent<UIWidget>();
            int height = widget.height;
            item.GetComponent<UIWidget>().SetDimensions((int)panel.width - 20, height);
        }
    }
    void LoadCharacter()
    {
        // Min-Goo, 2016년 3월 30일 추가된 코드
        // Episode Map
        UIMgr.Instance.HideCharacterView(eCharacterViewObjectType.CharacterView_1);
        // 캐릭터 UI List
        UIMgr.Instance.ShowCharacterView(eCharacterViewObjectType.CharacterView_2);
        GameObject CharacterView_2 = UIMgr.Instance.ShowCharacterViewGameObject(eCharacterViewObjectType.CharacterView_2);
        GameObject parent = CharacterView_2.transform.FindChild("Background").gameObject;
        //CharacterView_2
        int indexer = 0;
        foreach (KeyValuePair<string, GameCharacter> Gunner in CharacterMgr.Instance.GAME_CHARACTER)
        {
            GameObject charteritem = NGUITools.AddChild(m_SelectGrid.SelfObject, CharacterPrefab);
            GameObject GunnerPrefab = Resources.Load("Character/"+Gunner.Value.CHARACTER_TEMPLATE.PREFAB_NAME) as GameObject;
            GameObject go = GameObject.Instantiate(GunnerPrefab) as GameObject;
            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;
                t.localPosition = new Vector3(3.6f - indexer * 1.80f, 0, 0);
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                t.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                go.layer = parent.layer;
                go.name = Gunner.Value.CHARACTER_TEMPLATE.NAME.ToString();
            }

            

            UILabel Name = charteritem.transform.FindChild("Name").GetComponent<UILabel>();
            Name.text = Gunner.Value.CHARACTER_TEMPLATE.NAME;
            //NGUITools.AdjustDepth (charteritem.transform.FindChild("Gunner").gameObject, 1);

            // Min-Goo, 2016년 3월 30일 추가된 코드
            


            //Renderer mat = go.transform.FindChild("HeroBase_01").FindChild("HeroBase_mesh").GetComponent<Renderer>();
            //mat.material = FindMaterial(Gunner.Value.KEY);

            UILabel Level = charteritem.transform.FindChild("CharaterLevel/Label").GetComponent<UILabel>();
            Level.text = "L " + Gunner.Value.CHARACTER_TEMPLATE.REQUIRED_LEVEL;

            UILabel Health = charteritem.transform.FindChild("HealthBar/Label").GetComponent<UILabel>();
            Health.text = Gunner.Value.CHARACTER_TEMPLATE.HEALTH.ToString();
            //Explanation
            UILabel Explanation = charteritem.transform.FindChild("Skill/Label").GetComponent<UILabel>();
            Explanation.text = Gunner.Value.CHARACTER_TEMPLATE.HOBBY.ToString();

            UILabel Price = charteritem.transform.FindChild("Select Button/Label").GetComponent<UILabel>();
            Price.text = Gunner.Value.CHARACTER_TEMPLATE.REQUIRED_PRICE.ToString();

            if (Gunner.Value.CHARACTER_TEMPLATE.IS_PURCHASING.Equals("Y"))
            {
                charteritem.transform.FindChild("Select Button").gameObject.SetActive(false);
                charteritem.transform.FindChild("Jewel Button").gameObject.SetActive(true);
            }
            else if (Gunner.Value.CHARACTER_TEMPLATE.IS_PURCHASING.Equals("N"))
            {
                charteritem.transform.FindChild("Select Button").gameObject.SetActive(true);
                charteritem.transform.FindChild("Jewel Button").gameObject.SetActive(false);
            }
            charteritem.name = "CharacterSelect" + indexer.ToString();
            indexer++;
        }
        m_SelectGrid.SelfUIGrid.Reposition();
        UIMgr.Instance.ShowCharacterView(eCharacterViewObjectType.CharacterView_1);
    }

    Material FindMaterial(string matName)
    {
        Material mat = null;
        if (matName.Equals("Henry"))
            mat = Resources.Load("Materials/HeroSuit01", typeof(Material)) as Material;
        else if (matName.Equals("John"))
            mat = Resources.Load("Materials/HeroSuit02", typeof(Material)) as Material;
        else if (matName.Equals("Martin"))
            mat = Resources.Load("Materials/HeroSuit03", typeof(Material)) as Material;
        else if (matName.Equals("Robert"))
            mat = Resources.Load("Materials/HeroRobo01", typeof(Material)) as Material;
        else if (matName.Equals("Richard"))
            mat = Resources.Load("Materials/HeroSuit04", typeof(Material)) as Material; ;
        return mat;

    }

    void LoadRanking()
    {
        //TextAsset ta= Resources.Load ("Lank", typeof(TextAsset )) as TextAsset;
        //string s = ta.text;
        //string[] lines = s.Split('\n');
        /*foreach(string line in lines){
			string[] words = line.Split(',');
			UserInfo user = new UserInfo();
			user.Number = Convert.ToInt32(words[0]); 

            user.LevelNumber = Convert.ToInt32(words[1]);
			user.Name = words[5];
			user.Score = Convert.ToInt32(words[6]);
			user.Image = words[3];
			user.RatingClass = words[4];
            user.switch_onoff = words[7];
			users.Add(user);
		}*/
        LankMgr.Instance.Sort();

        /*users.Sort (delegate(UserInfo x, UserInfo y) {
			return x.Number.CompareTo(x.Number);
		});*/
        //List<GameObject> itemList = new List<GameObject>();

        // Min-Goo 4월 7일 추가한 코드
        UIGrid grid = m_RankGrid.SelfUIGrid;
        List<LankTemplateData> ranking = LankMgr.Instance.GetLank();
        foreach (LankTemplateData rank in ranking)
        {

            GameObject item = NGUITools.AddChild(m_RankGrid.SelfUIGrid.gameObject, LankPrefab);

            //UISprite itemsprite = item.gameObject.GetComponent<UISprite>();
            //itemsprite.SetAnchor(RankListGrid.transform);
            UISprite itemUI = item.GetComponent<UISprite>();
            itemUI.spriteName = rank.BACK_IMAGE;
            UILabel Number = item.transform.FindChild("Number2").GetComponent<UILabel>();
            Number.text = rank.NUMBER.ToString();
            UISprite RatingClass = item.transform.FindChild("Character").GetComponent<UISprite>();
            RatingClass.spriteName = rank.RATING_CLASS;
            UISprite Star = item.transform.FindChild("Image").transform.FindChild("Star").GetComponent<UISprite>();
            Star.spriteName = rank.STAR_IMAGE;

            UILabel LevelNumber = null;
            if (rank.STAR_IMAGE.Equals("025_star_empt".Trim()))
            {
                LevelNumber = item.transform.FindChild("Image/Star/LevelNumber").GetComponent<UILabel>();
                LevelNumber.text = rank.LEVEL_NUMBER.ToString();
                LevelNumber.color = Color.yellow;
            }

            else if (rank.STAR_IMAGE.Equals("026_star".Trim()))
            {
                LevelNumber = item.transform.FindChild("Image/Star/LevelNumber").GetComponent<UILabel>();
                LevelNumber.text = rank.LEVEL_NUMBER.ToString();
                LevelNumber.color = Color.black;
            }

            UILabel Name = item.transform.FindChild("Name").GetComponent<UILabel>();
            Name.text = rank.NAME;
            UILabel Score = item.transform.FindChild("Score").GetComponent<UILabel>();
            Score.text = rank.SCORE.ToString();
            UISprite OnOff = item.transform.FindChild("OnOff").GetComponent<UISprite>();
            OnOff.spriteName = rank.SWITCH_ONOFF;


            //UISprite group = item.transform.FindChild("Icon").GetComponent<UISprite>();
            //group.spriteName = GetSpriteAnyPangGroup(user.group.ToString ());
            //itemList.Add(item);

            //print ("itemList : "+itemList);

        }
        //RankListGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        m_RankGrid.SelfUIGrid.sorting = UIGrid.Sorting.None;

        m_RankGrid.SelfUIGrid.Reposition();
        //arrangeItems ();
        //foreach (GameObject item in itemList) { 

        //item.GetComponent<UIWidget>().SetAnchor(grid.gameObject);
        //}
        //		float i = 0;
        //		foreach (GameObject _item in itemList) {
        //			_item.GetComponent<UISprite>().SetAnchor(RankListGrid.transform);
        //			_item.GetComponent<UISprite>().leftAnchor = new UIRect.AnchorPoint(10);
        //			_item.GetComponent<UISprite>().rightAnchor = new UIRect.AnchorPoint(-10);
        //			_item.GetComponent<UISprite>().topAnchor = new UIRect.AnchorPoint(i);
        //			_item.GetComponent<UISprite>().bottomAnchor = new UIRect.AnchorPoint(i + grid.cellHeight);
        //			i += grid.cellHeight;
        //		}
    }
}
