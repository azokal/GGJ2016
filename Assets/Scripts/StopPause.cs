using UnityEngine;
using System.Collections;

public class StopPause : MonoBehaviour {
	public GameObject[] SwitchWhenContinue ;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PausDisable()
	{
		foreach(GameObject ToPause in SwitchWhenContinue)
		{
			ToPause.SetActive(false);
		}
		ForStopPause();
	}
	void ForStopPause()
	{
		Time.timeScale = 1;
		//TODO: reprendre musique
	}
}
