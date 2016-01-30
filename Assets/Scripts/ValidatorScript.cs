using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValidatorScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}

	string SimpleUpCheck() {
		if (Input.GetKey(KeyCode.Alpha1))
			return "SimpleUp";
		return "";
	}

	string SimpleDownCheck() {
		if (Input.GetKey(KeyCode.Alpha2))
			return "SimpleDown";
		return "";
	}

	string SimpleLeftCheck() {
		if (Input.GetKey(KeyCode.Alpha3))
			return "SimpleLeft";
		return "";
	}
	
	string SimpleRightCheck() {
		if (Input.GetKey(KeyCode.Alpha4))
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
					var ci = CheckInput();
					if (ci != "") {
						if (ci == hit.gameObject.GetComponent<ActionType>().eventType)
						Destroy(hit.gameObject);
					}
				}
		}
	}
}
