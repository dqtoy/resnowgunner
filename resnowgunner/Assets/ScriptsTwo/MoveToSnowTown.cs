using UnityEngine;
using System.Collections;

public class MoveToSnowTown : MonoBehaviour {
	GameObject Stage1;
	public Transform Player;
	Vector3 diff;
	// Use this for initialization
	void Start () {
		Stage1 = GameObject.Find ("UI Root (3D)/Stage1");
		StartCoroutine("iTweenIdleMotion");
	}
	IEnumerator iTweenIdleMotion() {
		// 시작
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// Stage1을 향해 쳐다본다.
		Player.LookAt (new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z));	diff =  new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z), 2.0f);
		// iTween.Hash ("position",transform.position = diff1, "Space", Space.World,"time",2.0f));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		Application.LoadLevel ("Stage2");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
