using UnityEngine;
using System.Collections;

public class iTweenStageClear : MonoBehaviour {
	GameObject Stage1;
	GameObject OnOff1_1, OnOff1_2, OnOff1_3;
	GameObject Stage2;
	GameObject OnOff2_1, OnOff2_2, OnOff2_3;
	GameObject Stage3;
	public Transform Player;
	Vector3 diff1,diff1_1,diff1_2,diff1_3,diff2;
	Vector3 diff;
	Animation animation;
	void Start () {
		animation = Player.GetComponent<Animation> ();
		Stage1 = GameObject.Find ("UI Root (3D)/Stage1");
		OnOff1_1 = GameObject.Find ("UI Root (3D)/OnOff 1-1");
		OnOff1_2 = GameObject.Find ("UI Root (3D)/OnOff 1-2");
		OnOff1_3 = GameObject.Find ("UI Root (3D)/OnOff 1-3");
		Stage2 = GameObject.Find ("UI Root (3D)/Stage2");
		OnOff2_1 = GameObject.Find ("UI Root (3D)/OnOff 2-1");
		OnOff2_2 = GameObject.Find ("UI Root (3D)/OnOff 2-2");
		OnOff2_3 = GameObject.Find ("UI Root (3D)/OnOff 2-3");
		Stage3 = GameObject.Find ("UI Root (3D)/Stage3");
		//StartCoroutine("IdleMotion");
		StartCoroutine("iTweenIdleMotion");
		//Debug.LogError ("Target.position[x,y,z] = (" + Target.position.x + "," + Target.position.y + "," + Target.position.z+")");
		//Debug.LogError ("Target2.position[x,y,z] = (" + Target2.position.x + "," + Target2.position.y + "," + Target2.position.z+")");
	}
	IEnumerator iTweenIdleMotion(){
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
		OnOff1_1.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff1_1.transform.FindChild ("Off").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// OnOff1_1을 향해 쳐다본다.
		Player.LookAt (new Vector3 (OnOff1_1.transform.position.x, 0, OnOff1_1.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(OnOff1_1.transform.position.x, 0, OnOff1_1.transform.position.z), 2.0f);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		OnOff1_2.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff1_2.transform.FindChild ("Off").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// OnOff1_2을 향해 쳐다본다.
		Player.LookAt (new Vector3 (OnOff1_2.transform.position.x, 0, OnOff1_2.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(OnOff1_2.transform.position.x, 0, OnOff1_2.transform.position.z), 2.0f);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		OnOff1_3.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff1_3.transform.FindChild ("Off").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// OnOff1_3을 향해 쳐다본다.
		Player.LookAt (new Vector3 (OnOff1_3.transform.position.x, 0, OnOff1_3.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(OnOff1_3.transform.position.x, 0, OnOff1_3.transform.position.z), 2.0f);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		Stage2.transform.FindChild ("Lock").gameObject.SetActive (false);
		Stage2.transform.FindChild ("Label").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// Stage2을 향해 쳐다본다.
		Player.LookAt (new Vector3 (Stage2.transform.position.x, 0, Stage2.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(Stage2.transform.position.x, 0, Stage2.transform.position.z), 2.0f);



		///////// 2 ///////
		yield return new WaitForSeconds (2.0f);
		OnOff2_1.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff2_1.transform.FindChild ("Off").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// OnOff2_1을 향해 쳐다본다.
		Player.LookAt (new Vector3 (OnOff2_1.transform.position.x, 0, OnOff2_1.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(OnOff2_1.transform.position.x, 0, OnOff2_1.transform.position.z), 2.0f);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		OnOff2_2.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff2_2.transform.FindChild ("Off").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// OnOff1_2을 향해 쳐다본다.
		Player.LookAt (new Vector3 (OnOff2_2.transform.position.x, 0, OnOff2_2.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(OnOff2_2.transform.position.x, 0, OnOff2_2.transform.position.z), 2.0f);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		OnOff2_3.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff2_3.transform.FindChild ("Off").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// OnOff1_3을 향해 쳐다본다.
		Player.LookAt (new Vector3 (OnOff2_3.transform.position.x, 0, OnOff2_3.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(OnOff2_3.transform.position.x, 0, OnOff2_3.transform.position.z), 2.0f);
		// 코루틴을 사용하여 2초간 정지한다.

		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		Stage3.transform.FindChild ("Lock").gameObject.SetActive (false);
		Stage3.transform.FindChild ("Label").gameObject.SetActive (true);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// Stage2을 향해 쳐다본다.
		Player.LookAt (new Vector3 (Stage3.transform.position.x, 0, Stage3.transform.position.z));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (Player.gameObject, new Vector3(Stage3.transform.position.x, 0, Stage3.transform.position.z), 2.0f);

	}//
	IEnumerator IdleMotion(){
		yield return new WaitForSeconds (2.0f);
		Player.LookAt (new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z));	diff =  new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
		diff =  new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

		yield return new WaitForSeconds (2.0f);
		Player.Translate (diff, Space.World);

		yield return new WaitForSeconds (2.0f);
		OnOff1_1.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff1_1.transform.FindChild ("Off").gameObject.SetActive (true);

		yield return new WaitForSeconds (2.0f);
		Player.LookAt (new Vector3 (OnOff1_1.transform.position.x, 0, OnOff1_1.transform.position.z));
		diff =  new Vector3(OnOff1_1.transform.position.x, 0, OnOff1_1.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

		yield return new WaitForSeconds (2.0f);
		Player.Translate (diff, Space.World);

		yield return new WaitForSeconds (2.0f);
		OnOff1_2.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff1_2.transform.FindChild ("Off").gameObject.SetActive (true);

		yield return new WaitForSeconds (2.0f);
		Player.LookAt (new Vector3 (OnOff1_2.transform.position.x, 0, OnOff1_2.transform.position.z));
		diff =  new Vector3(OnOff1_2.transform.position.x, 0, OnOff1_2.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
		
		yield return new WaitForSeconds (2.0f);
		Player.Translate (diff, Space.World);


		yield return new WaitForSeconds (2.0f);
		OnOff1_3.transform.FindChild ("On").gameObject.SetActive (false);
		OnOff1_3.transform.FindChild ("Off").gameObject.SetActive (true);

		yield return new WaitForSeconds (2.0f);
		Player.LookAt (new Vector3 (OnOff1_3.transform.position.x, 0, OnOff1_3.transform.position.z));
		diff =  new Vector3(OnOff1_3.transform.position.x, 0, OnOff1_3.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
		
		yield return new WaitForSeconds (2.0f);
		Player.Translate (diff, Space.World);

		yield return new WaitForSeconds (2.0f);
		Stage2.transform.FindChild ("Lock").gameObject.SetActive (false);
		Stage2.transform.FindChild ("Label").gameObject.SetActive (true);

		yield return new WaitForSeconds (2.0f);
		Player.LookAt (new Vector3 (Stage2.transform.position.x, 0, Stage2.transform.position.z));
		diff =  new Vector3(Stage2.transform.position.x, 0, Stage2.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

		yield return new WaitForSeconds (2.0f);
		Player.Translate (diff, Space.World);
	}
	// Update is called once per frame
	void Update () {
		// Stage1


		/*
		Player.LookAt (new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z));
		diff =  new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

		StartCoroutine("IdleMotion");
		Player.Translate (diff, Space.World);

		// 1-1
		StartCoroutine("IdleMotion");
		Player.LookAt (new Vector3 (OnOff1_1.transform.position.x, 0, OnOff1_1.transform.position.z));
		diff =  new Vector3(OnOff1_1.transform.position.x, 0, OnOff1_1.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

		StartCoroutine("IdleMotion");
		OnOff1_1.transform.FindChild ("On").gameObject.SetActive (false);

		StartCoroutine("IdleMotion");
		OnOff1_1.transform.FindChild ("Off").gameObject.SetActive (true);

		StartCoroutine("IdleMotion");
		Player.Translate (diff, Space.World);

		// 1-2
		StartCoroutine("IdleMotion");
		Player.LookAt (new Vector3 (OnOff1_2.transform.position.x, 0, OnOff1_2.transform.position.z));
		diff =  new Vector3(OnOff1_2.transform.position.x, 0, OnOff1_2.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
		
		StartCoroutine("IdleMotion");
		OnOff1_2.transform.FindChild ("On").gameObject.SetActive (false);
		
		StartCoroutine("IdleMotion");
		OnOff1_2.transform.FindChild ("Off").gameObject.SetActive (true);
		
		StartCoroutine("IdleMotion");
		Player.Translate (diff, Space.World);

		// 1-3
		StartCoroutine("IdleMotion");
		Player.LookAt (new Vector3 (OnOff1_3.transform.position.x, 0, OnOff1_3.transform.position.z));
		diff =  new Vector3(OnOff1_3.transform.position.x, 0, OnOff1_3.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
		
		StartCoroutine("IdleMotion");
		OnOff1_3.transform.FindChild ("On").gameObject.SetActive (false);
		
		StartCoroutine("IdleMotion");
		OnOff1_3.transform.FindChild ("Off").gameObject.SetActive (true);
		
		StartCoroutine("IdleMotion");
		Player.Translate (diff, Space.World);

		// Stage 2 Lock -> UnLocked

		StartCoroutine("IdleMotion");
		Player.LookAt (new Vector3 (Stage2.transform.position.x, 0, Stage2.transform.position.z));
		diff =  new Vector3(Stage2.transform.position.x, 0, Stage2.transform.position.z)  - new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
		
		StartCoroutine("IdleMotion");
		Stage2.transform.FindChild ("Lock").gameObject.SetActive (false);
		
		StartCoroutine("IdleMotion");
		Stage2.transform.FindChild ("Label").gameObject.SetActive (true);
		
		StartCoroutine("IdleMotion");
		Player.Translate (diff, Space.World);
		*/

	}
}
