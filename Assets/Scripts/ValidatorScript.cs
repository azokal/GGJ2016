using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValidatorScript : MonoBehaviour {

	// Use this for initialization

	private int combo = 1;
	private int serie = 0;
	private int score = 0;
	public float animationTime = 0.5f;
	private float tmpGood;
	private float tmpBad;
	public ParticleSystem goodParticle;
	public ParticleSystem badParticle;

	void Start () {
		tmpGood = animationTime;
		tmpBad = animationTime;
	}

	string SimpleUpCheck() {

		if (Input.GetKey(KeyCode.UpArrow))
			return "SimpleUp";
		return "";
	}

	string SimpleDownCheck() {
		if (Input.GetKey(KeyCode.DownArrow))
			return "SimpleDown";
		return "";
	}

	string SimpleLeftCheck() {
		if (Input.GetKey(KeyCode.LeftArrow))
			return "SimpleLeft";
		return "";
	}
	
	string SimpleRightCheck() {
		if (Input.GetKey(KeyCode.RightArrow))
			return "SimpleRight";
		return "";
	}

	string DoubleUpCheck() {
		if (Input.GetKey(KeyCode.Alpha5))
			return "DoubleUp";
		return "";
	}
	
	string DoubleDownCheck() {
		if (Input.GetKey(KeyCode.Alpha6))
			return "DoubleDown";
		return "";
	}

	string InvertUpCheck() {
		if (Input.GetKey(KeyCode.Alpha7))
			return "InvertUp";
		return "";
	}
	
	string InvertDownCheck() {
		if (Input.GetKey(KeyCode.Alpha8))
			return "InvertDown";
		return "";
	}

	string TurnLeftCheck() {
		if (Input.GetKey(KeyCode.Alpha9))
			return "TurnLeft";
		return "";
	}
	
	string TurnRightCheck() {
		if (Input.GetKey(KeyCode.Alpha0))
			return "TurnRight";
		return "";
	}
	
	string CheckInput() {
		var id = SimpleUpCheck ();
		if (id == "")
			id = SimpleDownCheck ();
		if (id == "")
			id = SimpleLeftCheck ();
		if (id == "")
			id = SimpleRightCheck ();
		if (id == "")
			id = DoubleUpCheck ();
		if (id == "")
			id = DoubleDownCheck ();
		if (id == "")
			id = InvertUpCheck ();
		if (id == "")
			id = InvertDownCheck ();
		if (id == "")
			id = TurnLeftCheck ();
		if (id == "")
			id = TurnRightCheck ();
		return id;
	}

	void ActionPlayed(string 
	                  ci) {
		Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
		var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
		foreach (var hit in hits) {
			if (hit.name == "Action") {
				if (ci != "") {
					if (ci == hit.gameObject.GetComponent<ActionType>().eventType) {
						serie += 1;
						Combo();
						score += 1 * combo;
						Destroy(hit.gameObject);
						tmpGood = 0f;
						//functionDebug();
						goodParticle.Play();
					}else{
						serie = 0;
						Combo();
						tmpBad = 0f;
						//functionDebug();
						Destroy(hit.gameObject);
						badParticle.Play();
					}
				}else{
				// You didn't push

				}

			}
		}
	}

	void OnTriggerEnter(Collider button) {
		Destroy(button.gameObject);
	}

	// Update is called once per frame
	void Update () {
		animGoodBar ();
		animBadBar ();
		if (!(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)) {
			Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
			var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
			foreach (var hit in hits) {
				if (hit.name == "Action") {
					string ci = "";
					ci = CheckInput();
					if (ci != "") {
						if (ci == hit.gameObject.GetComponent<ActionType>().eventType) {
							serie += 1;
							Combo();
							score += 1 * combo;
							Destroy(hit.gameObject);
							functionDebug();
						}else{
							serie = 0;
							Combo();
							functionDebug();
						}
					}
				}
			}
		}

	}

	void Combo(){
		var spawnAction = GameObject.Find ("Spawn Action").GetComponent<SpawnAction> ();
		if (serie < 15){
			combo = 1;
			spawnAction.difficulte = SpawnAction.enumDifficulte.facile;
		}
		if (serie >= 15 && combo < 40){
			combo = 2;
			spawnAction.difficulte = SpawnAction.enumDifficulte.moyen;
		}
		if (serie >= 40){
			combo = 4;
			spawnAction.difficulte = SpawnAction.enumDifficulte.difficile;
		}
	}

	void functionDebug(){
		Debug.Log ("Combo : "+ combo);
		Debug.Log ("Série : "+ serie);
		Debug.Log ("Score : "+ score);
		Debug.Log ("Difficulté : "+ GameObject.Find ("Spawn Action").GetComponent<SpawnAction> ().difficulte);
	}
		
	void animGoodBar(){
		if (tmpGood < animationTime){
			if (tmpGood < animationTime/2)
				GameObject.Find("Barre_Good").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, tmpGood/(animationTime/2));
			else
				GameObject.Find("Barre_Good").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, (animationTime - tmpGood) / (animationTime/2));

			tmpGood += Time.deltaTime;
		}

	}

	void animBadBar(){
		if (tmpBad < animationTime){
			if (tmpBad < animationTime/2)
				GameObject.Find("Barre_Bad").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, tmpBad/(animationTime/2));
			else
				GameObject.Find("Barre_Bad").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, (animationTime - tmpBad) / (animationTime/2));
			
			tmpBad += Time.deltaTime;
		}
		
	}

}