using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GestureInfo {

	public int TouchId{ get; set; }

	public Vector3 CurrentScreenPosition{ get; set; }
	public Vector3 StartScreenPosition{ get; set; }

	public Vector3 DeltaDragDistance{ get; set;	}

	public float DeltaDurationTime{ get; set; }

	public bool IsDown { get; set; }

	public bool IsUp { get; set; }

	public bool IsDrag { get; set; }
}

public class GestureManager : MonoBehaviour {

	List<GestureInfo> gestureInfo = new List<GestureInfo> ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var ret = getGesture ();
		if (ret != "") {
			GameObject.Find ("Validator").SendMessage ("ActionPlayed", ret, SendMessageOptions.DontRequireReceiver);
		}
	}

	float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		
		// angle in [-179,180]
		float signed_angle = angle * sign;
		
		return signed_angle;
	}

	string doubleGesture() {
		string ret0 = "";
		string ret1 = "";

		if (gestureInfo [0].StartScreenPosition.x < gestureInfo [1].StartScreenPosition.x) {
			ret1 = simpleGesture (gestureInfo [1]);
			ret0 = simpleGesture (gestureInfo [0]);
		} else {
			ret0 = simpleGesture (gestureInfo [1]);
			ret1 = simpleGesture (gestureInfo [0]);
		}
		if (ret0 == "SimpleUp" && ret1 == "SimpleUp")
			return "DoubleUp";
		if (ret0 == "SimpleDown" && ret1 == "SimpleDown")
			return "DoubleDown";
		if (ret0 == "SimpleDown" && ret1 == "SimpleUp")
			return "InvertUp";
		if (ret1 == "SimpleDown" && ret0 == "SimpleUp")
			return "InvertDown";
		return "";
	}

	string simpleGesture(GestureInfo gi) {

		var targetDir = (gi.CurrentScreenPosition - gi.StartScreenPosition).normalized;
		var forward = Vector3.up;

		var angle = SignedAngleBetween(targetDir, forward, forward);


		if (targetDir.y > 0 && angle > 0 && angle <= 20) {
			return "SimpleUp";
		}
		if (targetDir.y < 0 && angle > 0 && angle > 160) {
			return "SimpleDown";
		}
		if (targetDir.x > 0 && angle <= 110 && angle >= 70) {
			return "SimpleRight";
		}
		if (targetDir.x < 0 && angle <= 110 && angle >= 70) {
			return "SimpleLeft";
		}

		return "";

	}

	public int getNbInput() {
		int test = 0;
		for (int i = 0; i < gestureInfo.Count; i++) {
			if (gestureInfo[i].IsUp)
			test++;
		}
		return test;
	}

	public string getGesture() {

		if (Input.touchCount == 0) {
			var nb = getNbInput ();
			
			if (nb == 1) {
				var ret = simpleGesture(gestureInfo[0]);
				gestureInfo.Clear ();
				return ret;
			}
			else if (nb == 2) {
				var ret = doubleGesture();
				gestureInfo.Clear ();
				return ret;
			}
		}

		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.touches [i];
			
			switch (touch.phase) {
				case TouchPhase.Began:
					GestureInfo gi = new GestureInfo ();
					gi.TouchId = touch.fingerId;
					gi.CurrentScreenPosition = touch.position;
					gi.StartScreenPosition = touch.position;
					gi.DeltaDragDistance = touch.deltaPosition;
					gi.DeltaDurationTime = touch.deltaTime;
					gi.IsDown = true;
					gi.IsUp = false;
					gi.IsDrag = false;
					gestureInfo.Add(gi);
				break;
				case TouchPhase.Moved:
					for (int j = 0; j < gestureInfo.Count; j++) {
						if (touch.fingerId == gestureInfo [j].TouchId) {
							gestureInfo [j].CurrentScreenPosition = touch.position;
							gestureInfo [j].DeltaDragDistance = touch.deltaPosition;
							gestureInfo [j].DeltaDurationTime = touch.deltaTime;
							gestureInfo [j].IsDrag = true;
							break;
						}
					}
				break;
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					for (int j = 0; j < gestureInfo.Count; j++) {
						if (touch.fingerId == gestureInfo [j].TouchId) {
							gestureInfo [j].CurrentScreenPosition = touch.position;
							gestureInfo [j].DeltaDragDistance = touch.deltaPosition;
							gestureInfo [j].DeltaDurationTime = touch.deltaTime;
							gestureInfo [j].IsUp = true;
							break;
						}
					}
				break;
			}
		}
		return ""; 
	}
}
