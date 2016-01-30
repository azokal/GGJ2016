using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValidatorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private int combo = 1;
	private int serie = 0;
	private int score = 0;

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

	// Update is called once per frame
	void Update () {
		Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
		var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
		foreach (var hit in hits) {
			if (hit.name == "Action") {
				string ci = "";
				if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
					ci = GameObject.Find("GestureManager").GetComponent<GestureManager>().getGesture();
				else
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


	void Combo(){
		var spawnAction = GameObject.Find ("Spawn Action").GetComponent<SpawnAction> ();
		if (serie < 10){
			combo = 1;
			spawnAction.difficulte = SpawnAction.enDifficulte.facile;
		}
		if (serie >= 10 && combo < 20){
			combo = 2;
			spawnAction.difficulte = SpawnAction.enDifficulte.moyen;
		}
		if (serie >= 20){
			combo = 4;
			spawnAction.difficulte = SpawnAction.enDifficulte.difficile;
		}
	}

	void functionDebug(){
		Debug.Log ("Combo : "+ combo);
		Debug.Log ("Série : "+ serie);
		Debug.Log ("Score : "+ score);
		Debug.Log ("Difficulté : "+ GameObject.Find ("Spawn Action").GetComponent<SpawnAction> ().difficulte);
	}
		
	}