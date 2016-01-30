using UnityEngine;
using System.Collections;

public class SpawnAction : MonoBehaviour {
	public GameObject[] actions;
	public float interval = 1f;
	private float timer = 0f;

	public enum enDifficulte{facile,moyen,difficile};
	public enDifficulte difficulte = enDifficulte.facile;
	public float vitesseFacile = 5f;
	public float vitesseMoyen = 10f;
	public float vitesseDifficile= 15f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0f) {
			var o = Instantiate(actions[Random.Range(0,actions.Length)], this.transform.position, Quaternion.identity);
			o.name = "Action";
			timer = interval;

			switch(difficulte) {
				case enDifficulte.facile :
					MovingAction.vitesse = 5f;
					break;
				case enDifficulte.moyen:
					MovingAction.vitesse = 10f;
					break;
				case enDifficulte.difficile :
					MovingAction.vitesse = 15f;
					break;
			}
		}

		timer -= Time.deltaTime;
	}
}
