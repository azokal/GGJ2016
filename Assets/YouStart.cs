using UnityEngine;
using System.Collections;

public class YouStart : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per framedffd
	void Update () {
		Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
		var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
		foreach (var hit in hits) {
			if (hit.name == "Action") {
				
				var ma = hit.gameObject.GetComponent<MovingAction>();
				hit.gameObject.GetComponent<MovingAction>().fadeIn = true;
				hit.gameObject.GetComponent<MovingAction>().currentTimeToFade = hit.gameObject.GetComponent<MovingAction>().fadingTime;
				
			}
		}
	}
}
