using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HouseInfo{
	public int HouseX;
	public int HouseY;
	public string HouseNumGameObject;
}
public class HouseMap : MonoBehaviour {
	List<HouseInfo> housemapinfos1 = new List<HouseInfo>();
	List<HouseInfo> housemapinfos2 = new List<HouseInfo>();
	List<HouseInfo> housemapinfos3 = new List<HouseInfo>();
	List<HouseInfo> housebonusmapinfos = new List<HouseInfo>();
	Transform House1Prefab;
	Transform House2Prefab;
	Transform House3Prefab;
	Transform House4Prefab;
	Transform House5Prefab;
	Transform House6Prefab;
	Transform House7Prefab;
	Transform House8Prefab;
	Transform Tree1Prefab;
	Transform Tree2Prefab;
	Transform Tree3Prefab;
	Transform WindmillPrefab;
	public List<Transform> HouseList = new List<Transform>();
	string[] lines1, lines2, lines3, lines4;
	int RecycleXHouse = 0;
	int RecycleX;
	int RecycleXBonusHouse = 0;
	public int  theNumberofHouseListmoments = 0;
	Stage2Map stage2map;
	public MapMode housemode;
	// Use this for initialization
	void Start () {
		stage2map = StateMgr.Instance.GetStateObject(eStateType.STATE_TYPE_STAGE, eUIStageObj.Map2.ToString("F")).GetComponent<Stage2Map> ();
		housemode = MapMode.Map1;
		House1Prefab = Resources.Load ("Prefabs/House/House1", typeof(Transform)) as Transform;
		House2Prefab = Resources.Load ("Prefabs/House/House2", typeof(Transform)) as Transform;
		House3Prefab = Resources.Load ("Prefabs/House/House3", typeof(Transform)) as Transform;
		House4Prefab = Resources.Load ("Prefabs/House/House4", typeof(Transform)) as Transform;
		House5Prefab = Resources.Load ("Prefabs/House/House5", typeof(Transform)) as Transform;
		House6Prefab = Resources.Load ("Prefabs/House/House6", typeof(Transform)) as Transform;
		House7Prefab = Resources.Load ("Prefabs/House/House7", typeof(Transform)) as Transform;
		House8Prefab = Resources.Load ("Prefabs/House/House8", typeof(Transform)) as Transform;
		Tree1Prefab = Resources.Load ("Prefabs/House/Tree1", typeof(Transform)) as Transform;
		Tree2Prefab = Resources.Load ("Prefabs/House/Tree2", typeof(Transform)) as Transform;
		Tree3Prefab = Resources.Load ("Prefabs/House/Tree3", typeof(Transform)) as Transform;
		WindmillPrefab = Resources.Load ("Prefabs/House/Windmill", typeof(Transform)) as Transform;

		lines1 = LoadTextAsset ("SnowMap2/Pattern/SnowVillagePatternFirstHouseMap");
		lines2 = LoadTextAsset ("SnowMap2/Pattern/SnowVillagePatternMiddleHouseMap");
		lines3 = LoadTextAsset ("SnowMap2/Pattern/SnowVillagePatternThirdHouseMap");
		lines4 = LoadTextAsset ("SnowMap2/Pattern/SnowVillageBonusHousePatternMap");

		LoadHouseMap (lines1, housemapinfos1, MapMode.Map1);
		LoadHouseMap (lines2, housemapinfos2, MapMode.Map2);
		LoadHouseMap (lines3, housemapinfos3, MapMode.Map3);
		LoadHouseMap (lines4, housebonusmapinfos, MapMode.BonusMap);

		int minHouse = (int)HouseList [0].transform.position.x;
		int maxHouse = (int)HouseList [HouseList.Count - 1].transform.position.x;
		RecycleXHouse = maxHouse - minHouse + 2;
		RecycleX = RecycleXHouse;

	}
	string[] LoadTextAsset(string path){
		TextAsset snowVillageHouseMap_Ta = Resources.Load (path, typeof(TextAsset)) as TextAsset;
		string text = "";//System.String.Empty
		if(text.Equals(""))
			text = snowVillageHouseMap_Ta.text;
		print (text);
		string[] lines = text.Split('\n');
		return lines;
	}
	void LoadHouseMap (string[] lines, List<HouseInfo> housemapinfos, MapMode m_mode) {
		LoadHousePattern(lines, housemapinfos);
		housemapinfos.Sort(delegate(HouseInfo x, HouseInfo y) {
			return x.HouseX.CompareTo (y.HouseX);
		});
		if(housemode == m_mode)
			LoadCreateHouseMap(housemapinfos);
	}
	void LoadHousePattern(string[] lines, List<HouseInfo> housemapinfos) {
		int indexY = 0, indexX = 0;
		for(int i = 0; i < lines.GetLength(0); i++) {
			string[] words = lines[i].Split(',');
			
			for(int j = 0; j < words.GetLength(0); j++) {
				indexY = i;
				indexX = j;
				//Char.IsLetterOrDigit(words[j], 0) == false
				if(words[j].Equals("x"))
					continue;
				else if(words[j].Equals ("h1") || words[j].Equals ("h2") || words[j].Equals ("h3") || words[j].Equals ("h4")
				        || words[j].Equals ("h5") || words[j].Equals ("h6") || words[j].Equals ("h7") || words[j].Equals ("h8")
				        || words[j].Equals ("h9") || words[j].Equals ("t1") || words[j].Equals ("t2") || words[j].Equals ("t3"))
					LoadHouseMapTree(indexX,indexY,words[j], housemapinfos);
			}
		}
	}
	void LoadHouseMapTree(int indexX, int indexY, string word, List<HouseInfo> housemapinfos) {
		if(word!=null){
			HouseInfo housemapinfo = new HouseInfo();
			housemapinfo.HouseX = indexX;
			housemapinfo.HouseY = indexY;
			housemapinfo.HouseNumGameObject = word;
			housemapinfos.Add (housemapinfo);
		}
	}
	void LoadCreateHouseMap(List<HouseInfo> housemapinfos) {
		foreach(HouseInfo housemap in housemapinfos) {
			if(housemap.HouseNumGameObject.Equals("h1"))
				HouseCreateModel(House1Prefab, "house01", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("h2"))
				HouseCreateModel(House2Prefab, "house02", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("h3"))
				HouseCreateModel(House3Prefab, "house03", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("h4"))
				HouseCreateModel(House4Prefab, "house04", housemap, HouseList);	
			if(housemap.HouseNumGameObject.Equals("h5"))
				HouseCreateModel(House5Prefab, "house05", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("h6"))
				HouseCreateModel(House6Prefab, "house06", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("h7"))
				HouseCreateModel(House7Prefab, "house07", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("h8"))
				HouseCreateModel(House8Prefab, "house08", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("h9"))
				HouseCreateModel(WindmillPrefab, "Windmill", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("t1"))
				HouseCreateModel(Tree1Prefab, "Tree1", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("t2"))
				HouseCreateModel(Tree2Prefab, "Tree2", housemap, HouseList);
			if(housemap.HouseNumGameObject.Equals("t3"))
				HouseCreateModel(Tree3Prefab, "Tree3", housemap, HouseList);
		}
	}
	void HouseCreateModel(Transform houseprefab, string housename,HouseInfo _housemap, List<Transform> list){
		GameObject house= Instantiate(houseprefab.gameObject) as GameObject;
		house.gameObject.name = housename +list.Count;
		house.transform.parent = transform;
		house.transform.position = new Vector3 (RecycleXHouse + house.transform.localScale.x/2 +_housemap.HouseX * 2, house.transform.localScale.y/2-8.0f+2*_housemap.HouseY,2.5f);
		//model.transform.position = new Vector3 (RecycleXBrick + 2 * _icebrickmap.BlockX, -2 *_icebrickmap.BlockY + Y, 0);
		list.Add (house.transform);
	}

	// Update is called once per frame
	void Update () {
		RecycleHouseItemModel (HouseList, "HouseList[0]");
		if (HouseList [HouseList.Count- 1].transform.position.x - Player.distanceMoved  <= 4) {
			if(housemode == MapMode.BonusMapChange) {
				print ("MapMode = (BonusMapChange , " +MapMode.BonusMapChange+")");
				RecycleXHouse += 2;
			}
			if(housemode != MapMode.BonusMap) {//
				housemode = stage2map.mapmode;
			}
			
			if (housemode == MapMode.Map1 && housemode != MapMode.BonusMap && housemode != MapMode.BonusMapChange) {
				LoadCreateHouseMap (housemapinfos1);
				RecycleXHouse += RecycleX;
			}
			if (housemode == MapMode.Map2 && housemode != MapMode.BonusMap && housemode != MapMode.BonusMapChange) {
				LoadCreateHouseMap (housemapinfos2);
				RecycleXHouse += RecycleX;
			}
			if (housemode == MapMode.Map3 && housemode != MapMode.BonusMap && housemode != MapMode.BonusMapChange) {
				LoadCreateHouseMap (housemapinfos3);
				RecycleXHouse += RecycleX;
			}
			
			
			
		}
		else if(housemode == MapMode.BonusMap){
			//
			float distanceF = Player.distanceMoved - HouseList[0].transform.position.x;
			int distance = (int)distanceF;
			int distanceVar = distance - distance%2;
			RecycleXBonusHouse += distanceVar;
			RecycleXHouse -= RecycleX;
			RecycleXBonusHouse += 6;

			DestroyAtRemoveSetList1(HouseList, HouseList.Count);

			RecycleXHouse += RecycleXBonusHouse;
			LoadCreateHouseMap (housebonusmapinfos);
			housemode = MapMode.BonusMapChange;
		}
	}
	void DestroyAtRemoveSetList1 (List<Transform> list, int theNumberofListmoments){
		for (int i = 4; i < theNumberofListmoments; i++) {
			Destroy (list [4].gameObject);
			list.RemoveAt (4);
		}
	}
	void RecycleHouseItemModel(List<Transform> list, string logerror)
	{
		if (list.Count > 0) {
			if(list[0] == null || list[0].gameObject == null){
				Debug.LogError (logerror+" is null");
				list.RemoveAt (0);
			}
			else {
				if (Player.distanceMoved - list[0].transform.position.x > 4) { // IceBrickTileList 1
					Destroy (list [0].gameObject);
					list.RemoveAt (0);
				}
			}
		}
	}
}
