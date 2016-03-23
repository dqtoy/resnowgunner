using UnityEngine;
using System.Collections;

public class UIButtonClose : MonoBehaviour {
	public UIScaleAnimation anim;
	
	void OnClick()
	{
		anim.Close();	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
