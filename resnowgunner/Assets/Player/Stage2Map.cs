using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class IceBrickMapInfo{
	public int BlockX;
	public int BlockY;
	public string BlockNumGameObject;
}
public enum MapMode { Map1 = 0, Map2 = 1, Map3 = 2, BonusMap  = 3, BonusMapChange = 4};

public class Stage2Map : MonoBehaviour {

	public List<IceBrickMapInfo> icebrickmapinfos1 = new List<IceBrickMapInfo>();
	public List<IceBrickMapInfo> icebrickmapinfos2 = new List<IceBrickMapInfo>();
	public List<IceBrickMapInfo> icebrickmapinfos3 = new List<IceBrickMapInfo>();
	public List<IceBrickMapInfo> icebrickbonusmapinfos = new List<IceBrickMapInfo>();
	public Transform IceColorPrefab;
	public Transform IceNoColorPrefab;
	public Transform SmallCoinPrefab;
	public Transform BigCoinPrefab;
	public Transform SmallStarPrefab;
	public Transform BigStarPrefab;
	public Transform MonsterPrefab;


	public MapMode mapmode = MapMode.Map1;

	public Transform WPrefab, EPrefab, APrefab, PPrefab, OPrefab, NPrefab, FPrefab, LPrefab, YPrefab;
	public List<Transform> IceBrickTileList = new List<Transform>(); // private
	public List<Transform> SmallCoinList = new List<Transform>();
	public List<Transform> BigCoinList = new List<Transform>();
	public List<Transform> SmallStarList = new List<Transform>();
	public List<Transform> BigStarList = new List<Transform>();
	public List<Transform> AlphabetList = new List<Transform>();
	public List<Transform> MonsterList = new List<Transform>();

	string[] lines1, lines2, lines3, bonuslines;

	public float RecycleXBrick = 0;
	public float RecycleX;
	public float RecycleXBonus = 0;
	public int RemoveOffset = 103;
	System.Random random;
	int seed = -1;
	HouseMap housemap;
	// Use this for initialization
	void Awake() {
		//housemap = StateMgr.Instance.GetStateObject(eStateType.STATE_TYPE_STAGE, eUIStageObj.BackGroundHouse.ToString("F")).GetComponent<HouseMap>();
		seed = DateTime.Now.Millisecond;
		random = new System.Random(seed);
		
		IceColorPrefab = Resources.Load ("Prefabs/Ice/IceColor", typeof(Transform)) as Transform;
		IceNoColorPrefab = Resources.Load ("Prefabs/Ice/IceNoColor", typeof(Transform)) as Transform;
		SmallCoinPrefab = Resources.Load ("Prefabs/Coin/SU Coin 2x2 Spin Small", typeof(Transform)) as Transform;
		BigCoinPrefab = Resources.Load("Prefabs/Coin/SU Coin 2x2 Spin Big", typeof(Transform)) as Transform;
		SmallStarPrefab = Resources.Load ("Prefabs/Coin/SU Star Gold 2x2 Spin Small", typeof(Transform)) as Transform;
		BigStarPrefab = Resources.Load ("Prefabs/Coin/SU Star Gold 2x2 Spin Big", typeof(Transform)) as Transform;
		WPrefab = Resources.Load ("Prefabs/3DAlphabet/00W_Color", typeof(Transform)) as Transform;
		EPrefab = Resources.Load ("Prefabs/3DAlphabet/01E_Color", typeof(Transform)) as Transform;;
		APrefab = Resources.Load ("Prefabs/3DAlphabet/02A_Color", typeof(Transform)) as Transform;
		PPrefab = Resources.Load ("Prefabs/3DAlphabet/03P_Color", typeof(Transform)) as Transform;
		OPrefab = Resources.Load ("Prefabs/3DAlphabet/04O_Color", typeof(Transform)) as Transform;
		NPrefab = Resources.Load ("Prefabs/3DAlphabet/05N_Color", typeof(Transform)) as Transform;
		FPrefab = Resources.Load ("Prefabs/3DAlphabet/06F_Color", typeof(Transform)) as Transform;
		LPrefab = Resources.Load ("Prefabs/3DAlphabet/07L_Color", typeof(Transform)) as Transform;
		YPrefab = Resources.Load ("Prefabs/3DAlphabet/08Y_Color", typeof(Transform)) as Transform;
		MonsterPrefab = Resources.Load ("Prefabs/Monster/Zombie01Prefab", typeof(Transform)) as Transform;

		lines1 = LoadTextAsset ("SnowMap2/Pattern/SnowVillagePatternFirstMap");
		lines2 = LoadTextAsset ("SnowMap2/Pattern/SnowVillagePatternMiddleMap");
		lines3 = LoadTextAsset ("SnowMap2/Pattern/SnowVillagePatternThirdMap");
		bonuslines = LoadTextAsset ("SnowMap2/Pattern/SnowVillageBonusPatternMap");
		
		LoadMap (lines1, icebrickmapinfos1, MapMode.Map1);
		LoadMap (lines2, icebrickmapinfos2, MapMode.Map2);
		LoadMap (lines3, icebrickmapinfos3, MapMode.Map3);
		LoadMap (bonuslines, icebrickbonusmapinfos, MapMode.BonusMap);
	}
	void Start () {
		int minBrick = (int)IceBrickTileList [0].transform.position.x;
		int maxBrick = (int)IceBrickTileList [IceBrickTileList.Count - 1].transform.position.x;
		RecycleXBrick = maxBrick - minBrick + 2;
		RecycleX = RecycleXBrick;
	}
	string[] LoadTextAsset(string path){
		TextAsset snowPatternMap_Ta = Resources.Load (path, typeof(TextAsset)) as TextAsset;
		string text = "";//System.String.Empty
		if(text.Equals(""))
			text = snowPatternMap_Ta.text;
		print (text);
		string[] lines = text.Split('\n');
		return lines;
	}
	void LoadMap(string[] lines, List<IceBrickMapInfo> icebrickmapinfos, MapMode m_mode) {
		LoadPattern(lines, icebrickmapinfos);
		icebrickmapinfos.Sort(delegate(IceBrickMapInfo x, IceBrickMapInfo y) {
			return x.BlockX.CompareTo (y.BlockX);
		});
		if(mapmode == m_mode)
			LoadCreateMap (icebrickmapinfos);
	}

	void LoadIceBrickMap(int indexX, int indexY, string word, List<IceBrickMapInfo> icebrickmapinfos) {
		if(word!=null){
			IceBrickMapInfo icebrickmapinfo = new IceBrickMapInfo();
			icebrickmapinfo.BlockX = indexX;
			icebrickmapinfo.BlockY = indexY;
			icebrickmapinfo.BlockNumGameObject = word;
			icebrickmapinfos.Add (icebrickmapinfo);
		}
	}
	void LoadPattern(string[] lines, List<IceBrickMapInfo> icebrickmapinfos){
		int indexY = 0, indexX = 0;
		for(int i = 0; i < lines.GetLength(0); i++) {
			string[] words = lines[i].Split(',');
			
			for(int j = 0; j < words.GetLength(0); j++) {
				indexY = i;
				indexX = j;
				
				if(words[j].Equals("Empty") || words[j].Equals("x"))
					continue;
				else if(words[j].Equals ("w") || words[j].Equals ("e") || words[j].Equals ("a") || words[j].Equals ("p") ||
				        words[j].Equals ("o") || words[j].Equals ("n") || words[j].Equals ("f") || words[j].Equals ("l") || words[j].Equals("y")
				        || words[j].Equals ("22") || words[j].Equals ("2") || words[j].Equals ("11") | words[j].Equals ("1")
				        || words[j].Equals ("0") || words[j].Equals ("?") || words[j].Equals ("Monster"))
					LoadIceBrickMap(indexX,indexY,words[j], icebrickmapinfos);
			}
		}
	}
	void LoadCreateMap (List<IceBrickMapInfo> icebrickmapinfos){
		foreach(IceBrickMapInfo icebrickmap in icebrickmapinfos) {
			if(icebrickmap.BlockNumGameObject.Equals("0"))
				CreateModel(IceNoColorPrefab, "Snow", icebrickmap, IceBrickTileList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("?"))
				CreateModel(IceColorPrefab, "BrokenSnow", icebrickmap, IceBrickTileList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("22"))
				CreateModel(BigCoinPrefab, "BigCoinPrefab", icebrickmap, BigCoinList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("2"))
				CreateModel(SmallCoinPrefab, "SmallCoin", icebrickmap, SmallCoinList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("11"))
				CreateModel(BigStarPrefab, "BigStar", icebrickmap, BigStarList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("1"))
				CreateModel(SmallStarPrefab, "SmallStar", icebrickmap, SmallStarList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("w"))
				CreateModel(WPrefab, "00W", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("e"))
				CreateModel(EPrefab, "01E", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("a"))
				CreateModel(APrefab, "02A", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("p"))
				CreateModel(PPrefab, "03P", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("o"))
				CreateModel(OPrefab, "04O", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("n"))
				CreateModel(NPrefab, "04N", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("f"))
				CreateModel(FPrefab, "05F", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("l"))
				CreateModel(LPrefab, "06L", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("y"))
				CreateModel(YPrefab, "07Y", icebrickmap, AlphabetList, 0);
			if(icebrickmap.BlockNumGameObject.Equals("Monster"))
				CreateModel(MonsterPrefab, "Monster01", icebrickmap, MonsterList, -1);
		}
		

		
	}
	public void CreateModel(Transform prefab, string name, IceBrickMapInfo _icebrickmap, List<Transform> list, int Y) {
		GameObject model = Instantiate(prefab.gameObject) as GameObject;
		model.gameObject.name = name + list.Count.ToString();
		model.transform.parent = transform;
		model.transform.position = new Vector3 (RecycleXBrick + 2 * _icebrickmap.BlockX, -2 *_icebrickmap.BlockY + Y, 0);
		list.Add (model.transform);
	}
	//stage2map.RemoveModel(index, list);
	//posicao 포르투칼어 위치 position
	public void RemoveModel(int posicao, List<Transform> list){
		if(posicao >=0 && posicao < list.Count){
			Destroy(list[posicao].gameObject);
			list.RemoveAt(posicao);
		}
	}
	void Update () {
		RecycleBrickAndItemModel (IceBrickTileList, "IceBrickTileList[0]");
		RecycleBrickAndItemModel (SmallCoinList, "SmallCoinList[0]");
		RecycleBrickAndItemModel (BigCoinList, "BigCoinList[0]");
		RecycleBrickAndItemModel (SmallStarList, "SmallStarList[0]");
		RecycleBrickAndItemModel (BigStarList, "BigStarList[0]");
		RecycleBrickAndItemModel (AlphabetList, "AlphabetList[0]");
		RecycleBrickAndItemModel (MonsterList, "MonsterList[0]");
		if (IceBrickTileList [IceBrickTileList.Count- 1].transform.position.x - Player.distanceMoved  <= 4) {
			if(mapmode == MapMode.BonusMapChange) {
				print ("MapMode = (BonusMapChange , " +MapMode.BonusMapChange+")");
				RecycleXBrick += 2;
			}
			if(mapmode != MapMode.BonusMap) {
				List<int> mapList = new List<int>();
				for (int i = (int)MapMode.Map1; i <= (int)MapMode.Map3; i++) {
					if (mapmode != (MapMode)i)
						mapList.Add (i);
				}
				int index = random.Next (0, mapList.Count);
				mapmode = (MapMode)mapList[index];
			}

			if (mapmode == MapMode.Map1 && mapmode != MapMode.BonusMap && mapmode != MapMode.BonusMapChange) {
				LoadCreateMap (icebrickmapinfos1);
				RecycleXBrick += RecycleX;
			}
			if (mapmode == MapMode.Map2 && mapmode != MapMode.BonusMap && mapmode != MapMode.BonusMapChange) {
				LoadCreateMap (icebrickmapinfos2);
				RecycleXBrick += RecycleX;
			}
			if (mapmode == MapMode.Map3 && mapmode != MapMode.BonusMap && mapmode != MapMode.BonusMapChange) {
				LoadCreateMap (icebrickmapinfos3);
				RecycleXBrick += RecycleX;
			}



		}
		else if(mapmode == MapMode.BonusMap){
			float distanceF = Player.distanceMoved - IceBrickTileList[0].transform.position.x;
			int distance = (int)distanceF;
			int distanceVar = distance - distance%2;

			RecycleXBonus += distanceVar;
			RecycleXBrick -= RecycleX;
			RecycleXBonus += 6;

			DestroyAtRemoveSetList1(IceBrickTileList, IceBrickTileList.Count);
			DestroyAtRemoveSetList2(SmallCoinList);
			DestroyAtRemoveSetList2(BigCoinList);
			DestroyAtRemoveSetList2(SmallStarList);
			DestroyAtRemoveSetList2(BigStarList);
			DestroyAtRemoveSetList2(AlphabetList);
			DestroyAtRemoveSetList2(MonsterList);

			RecycleXBrick += RecycleXBonus;
			LoadCreateMap (icebrickbonusmapinfos);
			//int distance = (int)Player.distanceMoved - (int)IceBrickTileList[0].transform.position.x;
			//RecycleBonusX = distance;
			mapmode = MapMode.BonusMapChange;
		}
	}
	void DestroyAtRemoveSetList1 (List<Transform> list, int theNumberofListmoments){
		for(int i = 4 ; i < theNumberofListmoments; i++){
				Destroy(list[4].gameObject);
				list.RemoveAt (4);
		}
		
	}
	void DestroyAtRemoveSetList2 (List<Transform> list){
		int listcount = list.Count;
		for(int i = 0; i < listcount; i++){
			Destroy(list[i].gameObject);
		}
		list.RemoveRange (0, listcount);
		
	}
	void RecycleBrickAndItemModel(List<Transform> list, string logerror)
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
