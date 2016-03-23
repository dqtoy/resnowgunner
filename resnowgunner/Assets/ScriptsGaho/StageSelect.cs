using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour {
	public Transform target;
	public float Duration = 2.0f;
	int current = 1;
	public Transform[] StageButtons;
	Vector3[] paths;
	bool isMoving;
	
	// Use this for initialization
	void Start () {
		target.position = StageButtons[0].position;
	
	}
	public void OnClickMove(){
		if(isMoving == true)
			return;
		string name1 = UIButton.current.transform.parent.name;
		int select = System.Convert.ToInt32(name1.Substring(5,1));
		print ("select stage = " + select);
		StartCoroutine(jobMove(select));
	}
	
	IEnumerator jobMove(int select){
		bool isForward = select > current;
		isMoving = true;
		while(current != select){
			current += (int)Mathf.Sign(select - current);	
			if(isForward){
				paths = iTweenPath.GetPath("Stage" + current);
				print (paths);
			}
			else{
				paths = iTweenPath.GetPath("Stage" + (current + 1));
				print (paths);
			}
			
			float startTime = Time.time;
			while(Time.time < startTime + Duration){
				float percent = (Time.time - startTime) / Duration;
				target.transform.position = iTween.PointOnPath(paths, isForward ? percent : 1.0f - percent);
				yield return null;
			}
		}
		isMoving = false;
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
