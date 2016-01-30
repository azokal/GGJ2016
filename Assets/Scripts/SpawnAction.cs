using UnityEngine;
using System.Collections;

public class SpawnAction : MonoBehaviour {
	public GameObject action;
	public float interval;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = 0f;
		//Instantiate(action, this.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
