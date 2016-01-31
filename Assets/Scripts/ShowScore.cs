using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {
	private Text scoreText; 
	public GameObject validator;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (scoreText.gameObject.name == "Score") {
			scoreText.text = ("Score : " + validator.GetComponent<ValidatorScript> ().score);
		}
		if (scoreText.gameObject.name == "Multiplicateur") {
			scoreText.text = ("x" + validator.GetComponent<ValidatorScript> ().combo);
		}
		if (scoreText.gameObject.name == "Combo") {
			scoreText.text = ("Combo : " + validator.GetComponent<ValidatorScript> ().serie);
		}
	}
}
