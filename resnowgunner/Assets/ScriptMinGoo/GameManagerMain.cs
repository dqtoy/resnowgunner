using UnityEngine;
using System.Collections;

public class GameManagerMain : MonoBehaviour {
	// First Edit : HamGaho
	// Second Edit : MinGoo
	public UISlider HealthSlider;
	public UILabel Label;
	int maxlife = 10000;//10000 -> 5000 -> 1000
	public int life;
	Player s;

	GameObject player;
	public GameObject GameOver;
	bool gameover = false;
	//UISprite GameOver;
	// Use this for initialization
	Animator s_animator;
	public UILabel StarLabel, CoinLabel;
	public bool bonusmode = false;
	void Start () {
	
		s = GameObject.FindObjectOfType<Player>();
		life = maxlife;
		InvokeRepeating("MinusHealth", 0, 0.01f);
		player = GameObject.Find("Gunner");
		s_animator = player.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(!gameover)
			Score();

	}
	IEnumerator DeadMode(){
		s_animator.SetTrigger ("Hurt");
		yield return new WaitForSeconds (0.1f);
		GameOver.SetActive(true);
		yield return new WaitForSeconds (0.1f);
		StartCoroutine(StarResult ());
		StartCoroutine(CoinResult ());
		yield return null;
	}
	void MinusHealth(){
		if (!bonusmode) {
			if (life <= 0 && gameover == false) {
					gameover = true;
					s.i = 0.0f;
					StartCoroutine ("DeadMode");
					return;
			}
			HealthSlider.value = (float)(100 * life / maxlife) / 100.0f;
			life -= 1;
		}
	}
	void Score(){
		Label.text = (s.Score).ToString();
	}
	IEnumerator StarResult(){
		StarLabel.text = (s.Score).ToString ()+"스타";
		iTween.PunchScale (StarLabel.gameObject, 1.5f * Vector3.one, 0.7f);
		yield return new WaitForSeconds (0.12f);
	}
	IEnumerator CoinResult(){
		CoinLabel.text = (s.Coin).ToString ()+"골드";
		iTween.PunchScale (CoinLabel.gameObject, 1.5f * Vector3.one, 0.7f);
		yield return new WaitForSeconds (0.12f);
	}


}
