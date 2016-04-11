using UnityEngine;
using System.Collections;

public class Snow : MonoBehaviour {
	float SnowY;
	// Use this for initialization
	void Start () {
		SnowY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, SnowY, transform.position.z);
	}
}
