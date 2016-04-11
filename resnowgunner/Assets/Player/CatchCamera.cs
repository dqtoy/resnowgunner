using UnityEngine;
using System.Collections;

public class CatchCamera : MonoBehaviour {
	
	public Transform Target;
	float OffsetX = -8.0f;
	//float OffsetY;
	void Start () {

		//OffsetX = transform.position.x - Target.position.x;
	}

	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3 (Target.position.x + OffsetX, Target.position.y + OffsetY, transform.position.z);
		transform.position = new Vector3 (Target.position.x - OffsetX, transform.position.y, transform.position.z);
	}
	
}