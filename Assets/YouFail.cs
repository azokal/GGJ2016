using UnityEngine;
using System.Collections;

public class YouFail : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
		var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
		foreach (var hit in hits) {
			if (hit.name == "Action") {
				var v = GameObject.Find("Validator").GetComponent<ValidatorScript>();
				v.serie = 0;
				v.Combo ();
				v.tmpBad = 0f;
				hit.GetComponent<BoxCollider2D>().enabled = false;
				//functionDebug();
				v.badParticle.Play ();
			}
		}
	}
}
