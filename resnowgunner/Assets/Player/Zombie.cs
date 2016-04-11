using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
	public GameObject Gunner;
	// Use this for initialization
	Animator ZombieAnimator;
	void Start () {
		Gunner = GameObject.Find ("Gunner");
		ZombieAnimator = GetComponent<Animator> ();
		ZombieAnimator.SetTrigger ("ZombieIdle");
	}
	
	// Update is called once per frame
	void Update () {
		if (10.0f <= Mathf.Abs(transform.position.x - Gunner.transform.position.x)) {
			//ZombieAnimator.SetTrigger ("ZombieIdle");
		}
		if (4.0f <= Mathf.Abs(transform.position.x - Gunner.transform.position.x) && Mathf.Abs(transform.position.x - Gunner.transform.position.x) < 10.0f) {
			ZombieAnimator.SetTrigger ("ZombieWalk");
		}
		if (1.0f <= Mathf.Abs(transform.position.x - Gunner.transform.position.x) && Mathf.Abs(transform.position.x - Gunner.transform.position.x) < 4.0f) {
			ZombieAnimator.SetTrigger ("ZombieRun");
		}
		if (Mathf.Abs(transform.position.x - Gunner.transform.position.x) < 1.0f ) {
			ZombieAnimator.SetTrigger ("ZombieIdle");
		}
	}
}
