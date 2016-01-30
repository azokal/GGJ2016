using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValidatorScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
		var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
		print (hits);
		foreach (var hit in hits) {
			if (Input.GetKey(KeyCode.Space))
				if (hit.name == "Action") {
					Destroy(hit.gameObject);
				}
		}
	}
}
