using UnityEngine;
using System.Collections;

public class GestureManager : MonoBehaviour {
	public enum SwipeDirection{
		Up,
		Down,
		Right,
		Left
	}
	
	public static event Action<SwipeDirection> Swipe;
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
		if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0){
			if (swiping == false){
				swiping = true;
				lastPosition = Input.GetTouch(0).position;
				return "";
			}
			else{
				if (!eventSent) {
					if (Swipe != null) {
						Vector2 direction = Input.GetTouch(0).position - lastPosition;
						
						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							if (direction.x > 0)
								return "SimpleRight";
							else
								return "SimpleLeft";
						}
						else{
							if (direction.y > 0)
								return "SimpleUp";
							else
								return "SimpleDown";;
						}
						
						eventSent = true;
					}
				}
			}
		}
		else{
			swiping = false;
			eventSent = false;
		}
	}

	string getGesture() {
		if (Input.touchCount == 0) 
			return "";

		if (Input.touchCount == 1)
			return simpleGesture();


	}
}
