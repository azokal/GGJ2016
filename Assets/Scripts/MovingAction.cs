using UnityEngine;
using System.Collections;

public class MovingAction : MonoBehaviour {
	public float vitesse = 100f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var t = this.transform.position;
		t.x -= vitesse * Time.deltaTime;
		this.transform.position	 = t;
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
