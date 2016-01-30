using UnityEngine;
using System.Collections;

public class GamePaused : MonoBehaviour {
	public GameObject[] SwitchWhenPaused ;

	// Use this for initialization
	void Start () 
	{
		foreach(GameObject ToPause in SwitchWhenPaused)
		{
			ToPause.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PauseEnable()
	{
		foreach(GameObject ToPause in SwitchWhenPaused)
		{
		ToPause.SetActive(true);
		}
		SetPause();
	}
	void SetPause()
	{
		Time.timeScale = 0;
		//TODO: Pause musique
	}
}
