using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class M_GunnerCharacterInfo{
	public string C_Name;//0
	public string C_NamePrefab;//1
	public int C_Level;//2
	public int C_Health;//3
	public string C_Explanation;//4
	public int C_Price;//5
	public string C_PurchaseButton;//6
}

public enum GUIMode : int {Normal = 0, SetMode = 1, CharacterSelection = 2};
	
public class FriendsLobbyGameManager : MonoBehaviour {
	// Setting Manager
	public GameObject SettingMenu;
	public GameObject TermView;
	UILabel VersionLabel;
	public GameObject DarkBG;
	//게임 버전 일일히 값을 입력
	public string bundleVersion;
	public int UIMode = (int)GUIMode.Normal;
	// PlayerSelectManager
	public UIGrid SelectGrid;
	public GameObject CharacterPrefab;
	public GameObject Lobby;
	public GameObject Player_Background;
	List<M_GunnerCharacterInfo> Gunners = new List<M_GunnerCharacterInfo>();

	//UserList Ranking 연동
	public UIGrid RankListGrid;
    public GameObject LankPrefab;
	public UILabel GemLabel;
	public UILabel LevelLabel;
	public UISlider ExpSlider;
	public GameObject[] Hearts;
	public Transform StartButton;
	public UILabel ResetLabel;
	int heartCount = 5;
	int exp = 0;
	int level = 1;
	int GemCount = 0;


	int lastWidth;
	int lastHeight;
	bool stay = true;

	public GameObject DepthObjGunner, DepthCubeObj;
	// Use this for initialization
	void Start () {

		VersionLabel = SettingMenu.transform.FindChild ("Version Label").GetComponent<UILabel> ();
		VersionLabel.text = "게임 버젼 : " + bundleVersion;
		LoadCharacter();
		LoadRanking();
	
		StartCoroutine( check_for_resize() );
	}
	
	IEnumerator check_for_resize(){
		lastWidth = Screen.width;
		lastHeight = Screen.height;
		
		while( stay ){
			if( lastWidth != Screen.width || lastHeight != Screen.height ){
				arrangeItems ();
				lastWidth = Screen.width;
				lastHeight = Screen.height;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	void Update() {
		if(Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
			
		}
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
			
		}
	}

	public void SettingButton() {
		if(UIMode == (int)GUIMode.Normal) {
			if (UIButton.current.name == "Setting Icon") {
				StartCoroutine("Settings");
				UIMode = (int)GUIMode.SetMode;
				//NGUITools.PushBack(SettingMenu);//위젯만 해당
				//NGUITools.PushBack (DepthObjGunner);
				//NGUITools.PushBack (DepthCubeObj);
				//NGUITools.BringForward (DepthObjGunner);
				//NGUITools.BringForward (DepthCubeObj);
				//NGUITools.AdjustDepth (DepthObjGunner, 2);
				//NGUITools.AdjustDepth (DepthCubeObj, 2);
			}
		}
	}
	public void UsingTermsBtn(){
		if(UIMode == (int)GUIMode.SetMode){
			if (UIButton.current.name == "Using Terms") {
				iTween.PunchScale (SettingMenu.gameObject, 0.5f * Vector3.one, 0.7f);
				SettingMenu.SetActive(false);
				TermView.SetActive(true);
				iTween.PunchScale (TermView.gameObject, 1.25f * Vector3.one, 0.7f);
			}
		}
	}
	public void Exit() {
		if(UIMode == (int)GUIMode.SetMode) {
			if (UIButton.current.name == "Exit" && UIButton.current.gameObject.transform.parent.name == "Setting Menu") {
				StartCoroutine("SettingMenuQuit");
				UIMode = (int)GUIMode.Normal;
			}
			if (UIButton.current.name == "Exit" && UIButton.current.gameObject.transform.parent.name == "Term View") {
				StartCoroutine("TermViewQuit");
			}
		}
	}
	IEnumerator Settings(){
		DarkBG.SetActive (true);
		yield return new WaitForSeconds(0.2f);
		SettingMenu.SetActive (true);
		//iTween.PunchScale (SettingMenu.gameObject, 1.25f * Vector3.one, 0.7f);
		yield return new WaitForSeconds(0.2f);
	}
	IEnumerator SettingMenuQuit() {	
		iTween.PunchScale (SettingMenu.gameObject, 0.5f * Vector3.one, 0.7f);
		SettingMenu.SetActive (false);
		iTween.PunchScale (DarkBG.gameObject, 0.5f * Vector3.one, 0.7f);
		DarkBG.SetActive(false);
		yield return new WaitForSeconds(0.2f);
	}
	IEnumerator TermViewQuit() {
		iTween.PunchScale (TermView.gameObject, 0.5f * Vector3.one, 0.7f);
		TermView.SetActive (false);
		SettingMenu.SetActive (true);
		iTween.PunchScale (SettingMenu.gameObject, 1.25f * Vector3.one, 0.7f);
		yield return new WaitForSeconds(0.2f);
	}
	public void OnButtonCharacterShow(){
		if(UIMode == (int)GUIMode.Normal){
            //Lobby.SetActive (false);
            Player_Background.SetActive (true);
            // MinGoo, 2016년 3월 30일 추가된 코드
            UIMgr.Instance.HideCharacterView(eCharacterViewObjectType.CharacterView_1);
            UIMgr.Instance.ShowCharacterView(eCharacterViewObjectType.CharacterView_2);
            UIMode = (int)GUIMode.CharacterSelection;
		}
		//DialogMgr.ShowPurchaseDialog(OnCharacterBuyOk, OnCharacterCancel);
	}
	public void OnButtonCharacterShowNo(){
		if(UIMode == (int)GUIMode.CharacterSelection){
			Player_Background.SetActive (false);
            //Lobby.SetActive (true);
            UIMgr.Instance.HideCharacterView(eCharacterViewObjectType.CharacterView_2);
            UIMgr.Instance.ShowCharacterView(eCharacterViewObjectType.CharacterView_1);
            UIMode = (int)GUIMode.Normal;
		}
		//DialogMgr.ShowPurchaseDialog(OnCharacterBuyOk, OnCharacterCancel);
	}
	void OnCharacterBuyOk(){
		print ("buy item ok");
	}
	void OnCharacterCancel(){
		print ("Buy Item Cancel");
	}


    [SerializeField]
    GameObject GunnerPrefab;
    void LoadCharacter(){
		TextAsset ta = Resources.Load("PlayerSelect", typeof(TextAsset)) as TextAsset;
		string s = ta.text;
		string[] lines = s.Split('\n');
		int index_0 = 0; 
		foreach(string line in lines){
			
			string[] words = line.Split(',');
			//			if(words[index_0] == '\r' || words[index_0] == '\n')
			//				continue;
			//			index_0++;
			M_GunnerCharacterInfo my_charater_info = new M_GunnerCharacterInfo();
			my_charater_info.C_Name = words[0];
			my_charater_info.C_NamePrefab = words[1];
			my_charater_info.C_Level = Convert.ToInt32(words[2]);
			my_charater_info.C_Health = Convert.ToInt32(words[3]);
			my_charater_info.C_Explanation = words[4];
			my_charater_info.C_Price = Convert.ToInt32(words[5]);
			my_charater_info.C_PurchaseButton = words[6];
			Gunners.Add (my_charater_info);
			
		}
		Gunners.Sort (delegate (M_GunnerCharacterInfo x, M_GunnerCharacterInfo y) {
			return x.C_Name.CompareTo(y.C_Name);
		});
		int indexer = 0;

        // MinGoo, 2016년 3월 30일 추가된 코드
        UIMgr.Instance.HideCharacterView(eCharacterViewObjectType.CharacterView_1);
        UIMgr.Instance.ShowCharacterView(eCharacterViewObjectType.CharacterView_2);
        GameObject CharacterView_2 = UIMgr.Instance.ShowCharacterViewGameObject(eCharacterViewObjectType.CharacterView_2);
        GameObject parent = CharacterView_2.transform.FindChild("Background").gameObject;
        //CharacterView_2
        foreach (M_GunnerCharacterInfo Gunner in Gunners)
		{
			GameObject charteritem = NGUITools.AddChild(SelectGrid.gameObject, CharacterPrefab);



			UILabel Name = charteritem.transform.FindChild("Name").GetComponent<UILabel>();
            //Name.text = Gunner.C_Name.ToString();
            //NGUITools.AdjustDepth (charteritem.transform.FindChild("Gunner").gameObject, 1);

            // MinGoo, 2016년 3월 30일 추가된 코드
            GameObject go = GameObject.Instantiate(GunnerPrefab) as GameObject;
            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;
                t.localPosition = new Vector3(5.5f - indexer * 2.75f, 0, 0);
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                t.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                go.layer = parent.layer;
                go.name = Gunner.C_Name.ToString();
            }
            

            Renderer mat =  go.transform.FindChild("HeroBase_01").FindChild("HeroBase_mesh").GetComponent<Renderer>();
			mat.material = FindMaterial(Gunner.C_NamePrefab);
			
			UILabel Level = charteritem.transform.FindChild("CharaterLevel/Label").GetComponent<UILabel>();
			Level.text = "L " +  Gunner.C_Level.ToString();
			
			UILabel Health = charteritem.transform.FindChild("HealthBar/Label").GetComponent<UILabel>();
			Health.text = Gunner.C_Health.ToString();
			
			UILabel Explanation = charteritem.transform.FindChild("Skill/Label").GetComponent<UILabel>();
			Explanation.text = Gunner.C_Explanation.ToString();
			
			UILabel Price = charteritem.transform.FindChild("Select Button/Label").GetComponent<UILabel>();
			Price.text = Gunner.C_Price.ToString();
			
			if(Gunner.C_PurchaseButton.Equals("y")){
				charteritem.transform.FindChild ("Select Button").gameObject.SetActive(false);
				charteritem.transform.FindChild ("Jewel Button").gameObject.SetActive(true);
			}else if(Gunner.C_PurchaseButton.Equals("n")){
				charteritem.transform.FindChild ("Select Button").gameObject.SetActive(true);
				charteritem.transform.FindChild ("Jewel Button").gameObject.SetActive(false);
			}
			charteritem.name ="CharacterSelect"+indexer.ToString ();
			indexer++;
		}
		SelectGrid.Reposition();
        UIMgr.Instance.ShowCharacterView(eCharacterViewObjectType.CharacterView_1);
    }
	Material FindMaterial(string matName) {
		Material mat=null;
		if(matName.Equals("Henry"))
			mat = Resources.Load ("Materials/HeroSuit01", typeof(Material)) as Material;
		else if(matName.Equals("John"))
			mat = Resources.Load ("Materials/HeroSuit02", typeof(Material)) as Material;
		else if(matName.Equals("Martin"))
			mat = Resources.Load ("Materials/HeroSuit03", typeof(Material)) as Material;
		else if(matName.Equals("Robert"))
			mat = Resources.Load ("Materials/HeroRobo01", typeof(Material)) as Material;
		else if(matName.Equals("Richard"))
			mat = Resources.Load ("Materials/HeroSuit04", typeof(Material)) as Material;;
		return mat;
		
	}
	public void OnPurchase(){
		gameObject.SetActive(false);
	}
	public void GameStart() {
		if(UIMode == (int)GUIMode.Normal){
			if(UIButton.current.name == "GameStart")
			Application.LoadLevel("Progress Map");
		}
	}
	public void OnButtonShow(){
		print ("show");
		//DialogMgr.ShowDialog(OnBuyOk, OnBuyCancel);
	}
	void OnBuyOk(){
		print ("buy item ok");
	}
	void OnBuyCancel(){
		print ("Buy Item Cancel");
	}
	
	/*
	IEnumerator UpdateResetLabel(){
		while(true){
			yield return new WaitForSeconds(1.0f);
			DateTime now = DateTime.Now;
			int days = (7 + (int)DayOfWeek.Monday - (int)now.DayOfWeek) % 7;
			DateTime resetTime = new DateTime(now.Year, now.Month, now.Day);
			resetTime = resetTime.AddDays(days);
			TimeSpan ts = resetTime - now;
			if(ts.Days >= 1)
				ResetLabel.text = ts.Days + " Days " + ts.Hours + "Hour";
			else {
				ResetLabel.text = string.Format(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds);
			}
			
		}
	}
	public void AddGem(){
		GemCount++;
		GemLabel.text = GemCount.ToString();
	}
	public void StartGame(){
		if(heartCount <= 0)
			return;
		AddExp();	
		UserHeart();
	}
	void UserHeart(){
		Hearts[heartCount-1].GetComponent<TweenPosition>().enabled=true;
		//Hearts[heartCount-1].GetComponent<TweenAlpha>().PlayForward();
		iTween.MoveTo(Hearts[heartCount - 1], StartButton.position, 1.5f);
		heartCount -= 1;
	}	
	void AddExp(){
		exp += 1;
		if(exp > 10.0f){
			exp = 0;
			level++;
		}
		ExpSlider.value = (float)exp / 10.0f;
		LevelLabel.text = level.ToString();
	}
	*/
	void UseOption(){
		
	}
	void CloseOption(){
		
	}

	void arrangeItems() {
		UIPanel panel = RankListGrid.transform.parent.GetComponent<UIPanel> ();
		foreach (Transform item in RankListGrid.transform) {
			UIWidget widget = item.GetComponent<UIWidget>();
			int height = widget.height;
			item.GetComponent<UIWidget>().SetDimensions((int)panel.width - 20, height);
		}
	}

	void LoadRanking(){
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

        // MinGoo 4월 7일 추가한 코드
        UIGrid grid = RankListGrid.GetComponent<UIGrid>();
        List<LankTemplateData> ranking = LankMgr.Instance.GetLank();
        foreach (LankTemplateData rank in ranking) {

			GameObject item = NGUITools.AddChild(RankListGrid.gameObject, LankPrefab);

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

            else if(rank.STAR_IMAGE.Equals("026_star".Trim()))
            {
                LevelNumber = item.transform.FindChild("Image/Star/LevelNumber").GetComponent<UILabel>();
                LevelNumber.text = rank.LEVEL_NUMBER.ToString();
                LevelNumber.color = Color.black;
            }

            UILabel Name = item.transform.FindChild("Name").GetComponent<UILabel>();
			Name.text = rank.NAME;
			UILabel Score = item.transform.FindChild ("Score").GetComponent<UILabel>();
			Score.text = rank.SCORE.ToString();
            UISprite OnOff = item.transform.FindChild("OnOff").GetComponent<UISprite>();
            OnOff.spriteName = rank.SWITCH_ONOFF;
            

            //UISprite group = item.transform.FindChild("Icon").GetComponent<UISprite>();
            //group.spriteName = GetSpriteAnyPangGroup(user.group.ToString ());
            //itemList.Add(item);

			//print ("itemList : "+itemList);

		}
        RankListGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        RankListGrid.Reposition();
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
