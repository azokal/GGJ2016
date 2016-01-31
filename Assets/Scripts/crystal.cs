using UnityEngine;
using System.Collections;

public class crystal : MonoBehaviour {
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up*rotationSpeed*Time.deltaTime);
	
	}
}
