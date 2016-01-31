using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {
	private Text scoreText; 
	public GameObject validator;
	public GameObject fond;

	// Use this for initialization
	void Start () {

		scoreText = GetComponent<UnityEngine.UI.Text>();
	//	fond.transform.position = 
	//	scoreText.transform.position = 
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreText.text =("Score : "+ validator.GetComponent<ValidatorScript>().score +"\nCombo : "+ validator.GetComponent<ValidatorScript>().serie);
	}
}
