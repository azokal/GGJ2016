using UnityEngine;
using System.Collections;

public class SpawnAction : MonoBehaviour {
	public GameObject action;
	public float interval = 1f;
	private float timer = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0f) {
			Instantiate(action, this.transform.position, Quaternion.identity);
			timer = interval;
		}

		timer -= Time.deltaTime;
	}
}
