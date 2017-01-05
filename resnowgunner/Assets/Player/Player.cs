using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum ItemMode {DefaultMode = 0, SmallStarMode = 1, BigStarMode = 2, SmallCoinMode = 3, BigCoinMode = 4,
	W_ColorMode = 5, E_ColorMode = 6, A_ColorMode = 7, P_ColorMode = 8, O_ColorMode = 9, N_ColorMode = 10,
	F_ColorMode = 11, L_ColorMode = 12, Y_ColorMode = 13
};

public class Player : MonoBehaviour {
	//Attack
	public GameObject bullet;
	public float bulletSpeed = 1000.0f;

	//Animatior
	private Animator anim;
	private AnimatorStateInfo currentBaseState;


	public bool useCurves = true;
	public float useCurvesHeight = 0.5f;

	// 캐릭터 이동 변수 Character movement variables
	private Vector3 velocity;
	public float speed = 10.0f;
	public float i;
	
	public GameObject particle;


	public static float distanceMoved;
	float StartPosY;


	public int Score;
	public int Coin;
	public float acceleration;


	public Material HeroSuit01 ,HeroRobo01;

	// 캐릭터 점프 변수 Character Jump Variables
	public int jump_step = 0;
	public float jumpspeed = 20.0f;
	bool grounded;
	public Transform groundCheck;//-0.5f
	bool isJumping = false;

	UIButton AttackButton;
	GameObject AttackGameObject;
	UISprite AttackSprite;
	public float CoolDelta = 0.05f;
	UILabel CoolTimeLabel;
	UISprite W_Color, E_Color, A_Color, P_Color, O_Color, N_Color, F_Color, L_Color, Y_Color;

	bool IsCoolTime = false;
	GameManagerMain GameMgr;
	Stage2Map stage2map;
	HouseMap housemap;
	[ContextMenu ("BonusTime")]
	public void BonusTime(){
		W_Color.fillAmount = 1;
		E_Color.fillAmount = 1;
		A_Color.fillAmount = 1;
		P_Color.fillAmount = 1;
		O_Color.fillAmount = 1;
		N_Color.fillAmount = 1;
		F_Color.fillAmount = 1;
		L_Color.fillAmount = 1;
		Y_Color.fillAmount = 1;
	}
	void Start () {
		housemap = GameObject.Find ("BackGroundHouse").GetComponent<HouseMap>();
        GameMgr = UIMgr.Instance.GetShowUI(eStateType.STATE_TYPE_STAGE, eUIStateUI.PF_UI_MAIN_STAGE).GetComponent<GameManagerMain>();
        
        AttackGameObject = GameMgr.Attack.gameObject;
		AttackSprite = AttackGameObject.transform.FindChild("Screen").GetComponent<UISprite>();
		CoolTimeLabel = AttackGameObject.transform.Find ("CoolTimeTimeLabel").GetComponent<UILabel>();
        W_Color = GameMgr.W_Color;
        E_Color = GameMgr.E_Color;
        A_Color = GameMgr.A_Color;
        P_Color = GameMgr.P_Color;
        O_Color = GameMgr.O_Color;
        N_Color = GameMgr.N_Color;
        F_Color = GameMgr.F_Color;
        L_Color = GameMgr.L_Color;
        Y_Color = GameMgr.Y_Color;
       
		anim = GetComponent<Animator>();
		StartPosY = transform.position.y+12.0f;
		stage2map = GameObject.Find ("Map2").GetComponent<Stage2Map> ();
		i = 0.5f;
        IsCoolTime = false;

        SkillTimeCheck();
	}

	void Update () {
		if (isJumping == false && Physics.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Tile"))) {
			grounded = true;
			jump_step = 0;
		} else {
			grounded = false;
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("B")) {//Jump
			TouchJump ();
		}
		if (Input.GetKeyDown (KeyCode.Keypad0)) {//Attack
			MoveRight();                                                      
		}
		if (Input.GetKeyDown (KeyCode.Keypad4)) {
			BonusTime();
		}
		if (W_Color.fillAmount == 1.0f && E_Color.fillAmount == 1.0f && A_Color.fillAmount == 1.0f && P_Color.fillAmount == 1.0f && O_Color.fillAmount == 1.0f 
						&& N_Color.fillAmount == 1.0f && F_Color.fillAmount == 1.0f && L_Color.fillAmount == 1.0f && Y_Color.fillAmount == 1.0f) {
			stage2map.mapmode = MapMode.BonusMap;
			housemap.housemode = MapMode.BonusMap;
            GameMgr.bonusmode = true;
			StartCoroutine(BonusMode(W_Color,E_Color,A_Color,P_Color,O_Color,N_Color,F_Color,L_Color,Y_Color));
            GameMgr.bonusmode = false;
		}
	}
	void FixedUpdate () {
		// 주인공 이동로직 Character movement logic
        float translation = i;
        translation *= Time.fixedDeltaTime;
        transform.Translate(new Vector3(translation*speed, 0, 0), Space.World);
		float speedX = GetComponent<Rigidbody>().velocity.x;
		GetComponent<Rigidbody>().velocity = new Vector3(speedX, GetComponent<Rigidbody>().velocity.y);
		// 주인공 부활로직 Character resurrection logic
		if (transform.position.y < - 13.0f) {
			i = 0;
			transform.position = new Vector3 (transform.position.x+2.0f, StartPosY, transform.position.z);
			i = 0.5f;
		}
		// 이동한 거리 확인 Check the distance traveled?
		distanceMoved = transform.position.x;
	}
	void Jump(){
		if (jump_step == 0)
			GetComponent<Animator> ().SetTrigger ("Jump");
		else if (jump_step == 1)
			GetComponent<Animator> ().SetTrigger ("MultiJump");
		GetComponent<Rigidbody>().velocity = Vector3.up * jumpspeed;
		isJumping = true;
		Invoke ("endJumping", 0.1f);
	}
	void BonusJump() {
		jump_step = 0;
		StartCoroutine ("BonusJumpCoroutine");
	}
	void endJumping() {
		isJumping = false;
	}
	IEnumerator BonusJumpCoroutine(){
		GetComponent<Rigidbody>().velocity = Vector3.up * jumpspeed;
		for (int i = 0; i < 1/0.2f; i++) {
			yield return new WaitForSeconds (0.2f);
		}
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().velocity = Vector3.up * -jumpspeed;
		for (int i = 0; i < 1/0.2f; i++) {
			yield return new WaitForSeconds (0.2f);
		}
		GetComponent<Rigidbody>().useGravity = true;
		yield return null;
	}
	public void TouchJump() {
		//print ("TouchJump : grounded = " + grounded + ", jump_step = " + jump_step);
		if (grounded) {
	
			if (jump_step != 0 && jump_step != 1)
				return;
			if (jump_step == 0) {
				Jump ();
				jump_step = 1;
			}
		}
		else if (jump_step == 1) {
			Jump ();
			jump_step = 0;
		}
	}
	IEnumerator TurboMode(Collider other){
		Destroy(other.gameObject);
		gameObject.transform.GetChild (1).GetChild (1).GetComponent<Renderer>().material = HeroRobo01;
		yield return new WaitForSeconds (5.0f);
		gameObject.transform.GetChild (1).GetChild (1).GetComponent<Renderer>().material = HeroSuit01;
	}
    public void SkillTimeCheck()
    {
        StartCoroutine(CooldownTimeCheckCoroutine());
    }
    int loop_state = 0;
    public float initTime = 0.0f;
    IEnumerator CooldownTimeCheckCoroutine()
    {
        float startTime = Time.time;
        if (cooltime_state == 0)
        {

            cooltime_state = 1;
            if (cooltime_state == 1)
            {
                float elapsed = Time.time - startTime;

                if (loop_state == 0)
                {
                    float remain = 1.0f / CoolDelta - elapsed;
                    int remainInt = (int)remain;
                    initTime = remainInt;
                    loop_state = 1;
                    CoolTimeLabel.text = " " + remainInt.ToString();
                }
                else
                {

                }
                yield return null;
            }
            cooltime_state = 2;
            yield return null;
        }
        if (cooltime_state == 2)
        {
            CoolTimeLabel.text = " " + initTime.ToString();
            initTime = 0.0f;
            cooltime_state = 0;
            loop_state = 0;
            yield return null;
        }
        yield return null;
    }
	public void MoveRight(){
		// IsCoolTime = false
		if (!IsCoolTime) {// 총 쏘는 로직 
			GameObject a = Instantiate (bullet, transform.position + new Vector3 (2.0f, 1.5f, 0f), Quaternion.Euler (0, 90, 0)) as GameObject;
			a.GetComponent<Rigidbody>().AddForce (Vector3.right * bulletSpeed);
			Destroy (a, 5.0f);
			anim.SetTrigger ("Attack");
			IsCoolTime = true;
		// IsCoolTime = true
		}if (IsCoolTime) {// 총 안 쏘는 로직 bool 값으로 제어
			if(cooltime_state == 0)
				CooldownTime();
			else
				CooldownTime();
		}
	}
	public void CooldownTime() {
		StartCoroutine(CooldownTimeCoroutine());
	}

	int cooltime_state = 0;
	/*이전로직
     * IEnumerator CooldownTimeCoroutine(){
		float startTime = Time.time;
		if (cooltime_state == 0) {
			while (0 < AttackSprite.fillAmount) {
				cooltime_state = 1;
				if(cooltime_state == 1){
					float elapsed = Time.time - startTime;
					AttackSprite.fillAmount = Mathf.Clamp01 (1.0f - CoolDelta * elapsed);
					float remain = 1.0f / CoolDelta - elapsed;
					int remainInt = (int)remain;
					CoolTimeLabel.text = remainInt.ToString ();
					//print ("remain = " + remain);
					yield return new WaitForSeconds (0.05f);
				}
			}
			cooltime_state = 2;
			yield return null;
		}
		if (cooltime_state == 2) {
			if (AttackSprite.fillAmount <= 0) {
				IsCoolTime = false;
				AttackSprite.fillAmount = 1;
				yield return new WaitForSeconds (0.25f);
				cooltime_state = 0;
				yield return null;
			}
			yield return null;
		}
		yield return null;
	}*/
    IEnumerator CooldownTimeCoroutine()
    {
        float startTime = Time.time;
        if (cooltime_state == 0)
        {
            while (0 < AttackSprite.fillAmount)
            {
                cooltime_state = 1;
                if (cooltime_state == 1)
                {
                    float elapsed = Time.time - startTime;

                    if (loop_state == 0)
                    {
                        AttackSprite.fillAmount = Mathf.Clamp01(1.0f - CoolDelta * elapsed);
                        float remain = 1.0f / CoolDelta - elapsed;
                        int remainInt = (int)remain;
                        initTime = remainInt;
                        loop_state = 1;
                        CoolTimeLabel.text = " " + remainInt.ToString();
                    }
                    else
                    {
                        AttackSprite.fillAmount = Mathf.Clamp01(1.0f - CoolDelta * elapsed);
                        float remain = 1.0f / CoolDelta - elapsed;
                        int remainInt = (int)remain;
                        CoolTimeLabel.text = " " + remainInt.ToString();
                    }
                    //initremainInt = remainInt;

                    //print ("remain = " + remain);
                    yield return new WaitForSeconds(0.05f);
                }
                //joystickMove.IsAttack = false;
            }
            cooltime_state = 2;

            yield return null;
        }
        if (cooltime_state == 2)
        {
            AttackSprite.fillAmount = 1.0f;
            CoolTimeLabel.text = " " + initTime.ToString();
            initTime = 0.0f;
            cooltime_state = 0;
            loop_state = 0;
            IsCoolTime = false;
        }

        yield return null;
    }
	void TriggerCheck(Collider other, List<Transform> list, ItemMode itemMode) {
		int index = -1;
		string modelname="";
		for(int i = 0; i < list.Count ; i++) {
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

		if (itemMode == ItemMode.SmallStarMode)
			Score += 20;
		if (itemMode == ItemMode.BigStarMode)
			Score += 50;
		if (itemMode == ItemMode.SmallCoinMode)
			Coin += 30;
		if (itemMode == ItemMode.BigCoinMode)
			Coin += 70;

	}
	void TriggerCheckAlphabet(Collider other, List<Transform> list, ItemMode itemMode) {
		int index = -1;
		string modelname="";
		for(int i = 0; i < list.Count ; i++) {
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
		
		if(itemMode == ItemMode.W_ColorMode)
			ColorItemUp(1, W_Color);
		else if(itemMode == ItemMode.E_ColorMode)
			ColorItemUp(1, E_Color);
		else if(itemMode == ItemMode.A_ColorMode)
			ColorItemUp(1, A_Color);
		else if(itemMode == ItemMode.P_ColorMode)
			ColorItemUp(1, P_Color);
		else if(itemMode == ItemMode.O_ColorMode)
			ColorItemUp(1, O_Color);
		else if(itemMode == ItemMode.N_ColorMode)
			ColorItemUp(1, N_Color);
		else if(itemMode == ItemMode.F_ColorMode)
			ColorItemUp(1, F_Color);
		else if(itemMode == ItemMode.L_ColorMode)
			ColorItemUp(1, L_Color);
		else if(itemMode == ItemMode.Y_ColorMode)
			ColorItemUp(1, Y_Color);
	}
	//OnTriggerEnter
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "W")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.W_ColorMode);
		if (other.gameObject.tag == "E")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.E_ColorMode);
		if (other.gameObject.tag == "A")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.A_ColorMode);
		if (other.gameObject.tag == "P")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.P_ColorMode);
		if (other.gameObject.tag == "O")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.O_ColorMode);
		if (other.gameObject.tag == "N")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.N_ColorMode);
		if (other.gameObject.tag == "F")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.F_ColorMode);
		if (other.gameObject.tag == "L")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.L_ColorMode);
		if (other.gameObject.tag == "Y")
			TriggerCheckAlphabet(other, stage2map.AlphabetList, ItemMode.Y_ColorMode);

		if (other.gameObject.tag == "Change")
			StartCoroutine("TurboMode", other);

		if (other.gameObject.tag == "SmallStar")
			TriggerCheck(other, stage2map.SmallStarList, ItemMode.SmallStarMode);
		if (other.gameObject.tag == "BigStar")
			TriggerCheck(other, stage2map.BigStarList, ItemMode.BigStarMode);
		if (other.gameObject.tag == "SmallCoin")
			TriggerCheck(other, stage2map.SmallCoinList, ItemMode.SmallCoinMode);
		if (other.gameObject.tag == "BigCoin")
			TriggerCheck(other, stage2map.BigCoinList, ItemMode.BigCoinMode);
	}

	void OnCollisionEnter(Collision info) {
		if(info.gameObject.tag == "Box"){
			anim.SetTrigger("Damage");
		}
	}
	public void Restart() {
		StartCoroutine("RestartCoroutine");
	}
	IEnumerator RestartCoroutine() {
		yield return new WaitForSeconds(0.5f);
        StateMgr.Instance.ChangeState(eStateType.STATE_TYPE_STAGE, null);
        //Application.LoadLevel ("Stage2");
	}
	public void NextStage() {
		StartCoroutine("NextStageCoroutine");
	}
	IEnumerator NextStageCoroutine() {
		yield return new WaitForSeconds(0.5f);
        StateMgr.Instance.ChangeState(eStateType.STATE_TYPE_FRIENDS_LOBBY, null);
		//Application.LoadLevel ("FriendsWindow2Stage");
	}
	void ColorItemUp(int count, UISprite _ColorSprite) {
		if (!GameMgr.bonusmode) {
			StartCoroutine (StartColorItemUp (_ColorSprite));
		}
	}
	IEnumerator StartColorItemUp(UISprite _ColorSprite){
        GameMgr.life +=100;
		while(_ColorSprite.fillAmount < 1) {
			_ColorSprite.fillAmount +=0.1f;
			yield return new WaitForSeconds(0.1f);
		}
		yield return null;
	}
	public float bonuspeed = 0.05f;
	IEnumerator BonusMode(UISprite _W_Color, UISprite _E_Color, UISprite _A_Color, UISprite _P_Color, UISprite _O_Color, 
	                      UISprite _N_Color,UISprite _F_Color,UISprite _L_Color,UISprite _Y_Color){
		if (GameMgr.bonusmode) {
			while (0 < _Y_Color.fillAmount) {
				_Y_Color.fillAmount -= bonuspeed;	
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _L_Color.fillAmount) {
				_L_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _F_Color.fillAmount) {
				_F_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _N_Color.fillAmount) {
				_N_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _O_Color.fillAmount) {
				_O_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _P_Color.fillAmount) {
				_P_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _A_Color.fillAmount) {
				_A_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _E_Color.fillAmount) {
				_E_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
			while (0 < _W_Color.fillAmount) {
				_W_Color.fillAmount -= bonuspeed;
				yield return new WaitForSeconds (bonuspeed);
			}
            GameMgr.life +=100;
		}
	}

}