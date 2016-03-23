using UnityEngine;
using System.Collections;

public class Terms : MonoBehaviour {
	//Koorrad Studio Terms of Use
	// Use this for initialization
	TextAsset terms;
	UILabel termsLabel;
	void Start () {
		termsLabel = gameObject.GetComponent<UILabel> ();
		terms = Resources.Load ("Koorrad Studio Terms of Use", typeof(TextAsset)) as TextAsset;
		string text = terms.text;
		string[] lines = text.Split('\n');
		for(int i = 0; i < lines.GetLength(0) ;i++){
			termsLabel.text+=lines[i];
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
