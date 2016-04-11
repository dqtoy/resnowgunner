using UnityEngine;
using System.Collections;

public class MoveToSnowTown : BaseMgr<MoveToSnowTown> {
    GameObject Stage1;
    Vector3 diff;

    [SerializeField]
    Transform m_MapPlayer;
    
    [SerializeField]
    bool m_IsProgreesEnd = false;

    public GameObject MapPlayer { get { return MapPlayer;      } }
    public bool IS_PROGRESSEND  { get { return m_IsProgreesEnd;} set { m_IsProgreesEnd = value; } }

	void Start () {
		Stage1 = GameObject.Find ("PF_UI_3D_PROGRESS_MAP(Clone)/Stage1");
        m_MapPlayer = (Instantiate(Resources.Load("ProgressMap/Model/MapSelectGunner", typeof(GameObject)), new Vector3(0,0.1f,0), Quaternion.identity) as GameObject).transform;
        m_MapPlayer.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        
	}

    public void IdleMotion()
    {
        if (!m_IsProgreesEnd)
            StartCoroutine("iTweenIdleMotion");
    }
    IEnumerator iTweenIdleMotion() {
		// 시작
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
        // Stage1을 향해 쳐다본다.
        m_MapPlayer.LookAt (new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z));	diff =  new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z)  - new Vector3(m_MapPlayer.transform.position.x, 0, m_MapPlayer.transform.position.z);
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
		// 다람쥐가 이동한다.
		iTween.MoveTo (m_MapPlayer.gameObject, new Vector3(Stage1.transform.position.x, 0, Stage1.transform.position.z), 2.0f);
		// iTween.Hash ("position",transform.position = diff1, "Space", Space.World,"time",2.0f));
		// 코루틴을 사용하여 2초간 정지한다.
		yield return new WaitForSeconds (2.0f);
        StateMgr.Instance.ChangeState(eStateType.STATE_TYPE_STAGE);
	}
}
