using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Bullet : MonoBehaviour {
	Stage2Map stage2map;
	// Use this for initialization
	void Start () {
		stage2map = GameObject.Find ("Map2").GetComponent<Stage2Map> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Zombie")
			TriggerCheck(other, stage2map.MonsterList);
	}

	void TriggerCheck(Collider other, List<Transform> list) {
		int index = -1;
		string modelname="";
		for(int i = 0; i < list.Count ; i++){
			if(other.gameObject == list[i].gameObject){
				index = i;
				modelname = list[index].gameObject.name;
				break;
			}
		}
		if (modelname.Length == 0) {
			Debug.LogWarning("modelname not found");
			return;
		}
		stage2map.RemoveModel(index, list);//
		//stage2map.theNumberofMonsterListmoments -= 1;
	}
}
