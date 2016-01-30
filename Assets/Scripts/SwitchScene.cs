using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {
	public string  SceneName ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ToOtherScene ()
	{
		Application.LoadLevel(SceneName);
	}
}
