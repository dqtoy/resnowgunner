using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class ProgressMapInfo{
	public string UserName;
	public int MainStage; // 1 2 3
	public int Lock;	  // 0 1 1
	public string MiniStage; // 0A0B0C 1A1B1C 2A2B2C
	public int MainStageClear; // 0 0 0
	public string MiniStageClear; // 0A0 0B0 0C0 1A01B01C0 2A02B02C0
	 
}
public class MiniStage {
	public string mini0;// 0
	public string mini1;// 0
	public string mini2;// 0
	public string miniA;// 0 - A
	public string miniB;// 0 - B
	public string miniC;// 0 - C
}
public class MiniStageClear {
	public string mini0;// 0
	public string mini1;// 0
	public string mini2;// 0
	public string miniA;// 0 - A
	public string miniB;// 0 - B
	public string miniC;// 0 - C
	public int clearA;
	public int clearB;
	public int clearC;
}
public class iTweenPathGameClearMode : MonoBehaviour {
	List<ProgressMapInfo> pmapinfos = new List<ProgressMapInfo>();
	// Use this for initialization
	void Start () {
		LoadProgressMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void LoadProgressMap() {
		TextAsset gamemaptable_ta= Resources.Load ("GameMapTable", typeof(TextAsset )) as TextAsset;
		string s = gamemaptable_ta.text;
		string[] lines = s.Split('\n');

		foreach(string line in lines){
			string[] words = line.Split(',');
			if(lines[0]!=null)
				continue;
			ProgressMapInfo pmapinfo = new ProgressMapInfo();
			pmapinfo.UserName = words[0];
			pmapinfo.MainStage = Convert.ToInt32 (words[1]);
			pmapinfo.Lock = Convert.ToInt32 (words[2]);
			pmapinfo.MiniStage = words[3];
			pmapinfo.MainStageClear = Convert.ToInt32 (words[4]);
			pmapinfo.MiniStageClear = words[5];
			pmapinfos.Add(pmapinfo);

		}
		//UserName,MainStage,Lock,MiniStage,MainStageClear,MiniStageClear
	}
}
