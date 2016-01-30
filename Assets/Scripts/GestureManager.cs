using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GestureManager : MonoBehaviour {


	public enum SwipeDirection{
		Up,
		Down,
		Right,
		Left
	}

	private bool swiping = false;
	private bool eventSent = false;
	private Vector2 lastPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	string simpleGesture() {
		GameObject.Find ("Debug Text").GetComponent<Text> ().text = "SimpleInput";
		if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0){
			if (swiping == false){
				swiping = true;
				lastPosition = Input.GetTouch(0).position;
				return "";
			}
			else{
				if (!eventSent) {
					Vector2 direction = Input.GetTouch(0).position - lastPosition;
					
					if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
						eventSent = true;	
						if (direction.x > 0)
							return "SimpleRight";
						else
							return "SimpleLeft";
					}
					else{
						eventSent = true;
						if (direction.y > 0)
							return "SimpleUp";
						else
							return "SimpleDown";
					}
				}
			}
		}
		else{
			swiping = false;
			eventSent = false;
		}
		return "";
	}

	public string getGesture() {
		if (Input.touchCount == 0) 
			return "";

		if (Input.touchCount == 1)
			return simpleGesture();

		return "";
	}
}
