using UnityEngine;
using System.Collections;

public class UIScaleAnimation : MonoBehaviour {

	void Awake(){
		SetDisable();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable()
	{
		open();	
	}
	void OnDisable()
	{
		Close();	
	}
	void open()
	{
		init ();	
	}
	
	float duration = 0.2f;
	float startDelay = 0.2f;
	Vector3 scaleTo = new Vector3(1f, 1f, 1f);
	
	AnimationCurve animationCurve = new AnimationCurve(
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(0.7f, 1.2f, 1f, 1f),
		new Keyframe(1f, 1f, 1f, 0f));
	
	void init()
	{
		TweenScale tween = TweenScale.Begin(gameObject, duration, scaleTo);
		tween.duration = duration;
		tween.animationCurve = animationCurve;
	}
	
	public void SetDisable()
	{
		gameObject.transform.localScale = new Vector3(0, 0, 0);
		gameObject.SetActive(false);
	}
	
	public void Close()
	{
		SetDisable();
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
