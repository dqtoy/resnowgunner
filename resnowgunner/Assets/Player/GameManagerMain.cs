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

    public UIButton Attack;
    public UIButton Jump;

    public UISprite W_Color;
    public UISprite E_Color;
    public UISprite A_Color;
    public UISprite P_Color;
    public UISprite O_Color;
    public UISprite N_Color;
    public UISprite F_Color;
    public UISprite L_Color;
    public UISprite Y_Color;

    void Start () {
	
		//s = GameObject.FindObjectOfType<Player>();
		life = maxlife;
		InvokeRepeating("MinusHealth", 0, 0.01f);
		player = StateMgr.Instance.GetStateObject(eStateType.STATE_TYPE_STAGE, eUIStageObj.GUNNER.ToString("F"));
		s_animator = player.GetComponent<Animator> ();

        EventDelegate onClickEvent1 = new EventDelegate(player.GetComponent<Player>(), "MoveRight");

        EventDelegate.Parameter param1 = new EventDelegate.Parameter();
        //param.value = 1;
        //onClickEvent.parameters[0] = param;
        EventDelegate.Add(Attack.onClick, onClickEvent1);

        EventDelegate onClickEvent2 = new EventDelegate(player.GetComponent<Player>(), "TouchJump");

        EventDelegate.Parameter param2 = new EventDelegate.Parameter();
        //param.value = 1;
        //onClickEvent.parameters[0] = param;
        EventDelegate.Add(Jump.onClick, onClickEvent2);
        s = player.GetComponent<Player>();
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

    public void RestartGame()
    {
        player.GetComponent<Player>().Restart();
    }

    public void NextStageGame()
    {
        player.GetComponent<Player>().NextStage();
    }

}
