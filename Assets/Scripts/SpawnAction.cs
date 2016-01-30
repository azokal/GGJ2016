using UnityEngine;
using System.Collections;

public class SpawnAction : MonoBehaviour {
	public GameObject[] actions;
	public float interval = 1f;
	private float timer = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0f) {
			var o = Instantiate(actions[Random.Range(0,actions.Length)], this.transform.position, Quaternion.identity);
			o.name = "Action";
			timer = interval;
		}

		timer -= Time.deltaTime;
	}
}
