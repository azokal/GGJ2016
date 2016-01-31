using UnityEngine;
using System.Collections;

public class MovingAction : MonoBehaviour {
	
	public float fadingTime = 0.5f;
	public float currentTimeToFade = 0f;
	
	public bool fadeIn = true;
	
	public static float vitesse = 5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var t = this.transform.position;
		t.x -= vitesse * Time.deltaTime;
		this.transform.position	 = t;

		if (currentTimeToFade > 0) {
			var a = currentTimeToFade / fadingTime;

			if (fadeIn == false) {
				this.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1,  a);
			}
			else {
				this.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1,  (fadingTime-currentTimeToFade) / fadingTime);
			}

			currentTimeToFade -= Time.deltaTime;
		}
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
