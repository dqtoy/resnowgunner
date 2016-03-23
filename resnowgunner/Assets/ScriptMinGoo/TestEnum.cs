using UnityEngine;
using System.Collections;
public enum Mode : int {Sat=1, Sun=2, Mon=3, Tue=4, Wed=5, Thu=6, Fri=7};
public class TestEnum : MonoBehaviour {
	int mode = (int)Mode.Sun;
	private GameObject target;
	Transform obj;
	int i = 0;
	public Collider col;
	void Awake() {
		print ("Awake");
		target = GameObject.FindGameObjectWithTag("Player");
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Transform t = cube.transform;
		t.parent = target.transform;
		//target.transform.parent = cube.transform;
		//obj = Instantiate (PrimitiveType.Cube, new Vector3 (i * 2.0F, 0, 0), Quaternion.identity) as Transform;

	}



		

	// Use this for initialization
	void Start () {
		print ("Start");
	}

	// Update is called once per frame
	void Update () {
	
	}
	[ContextMenu ("Loop")]
	void Loop(){
		print ("Loop");
	}
}
