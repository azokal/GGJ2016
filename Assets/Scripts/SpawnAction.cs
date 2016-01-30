using UnityEngine;
using System.Collections;

public class SpawnAction : MonoBehaviour {
	public GameObject[] actionsFaciles;
	public GameObject[] actionsMoyennes;
	public GameObject[] actionsDifficiles;
	private GameObject[] actionsActuelles;

	public float interval = 1f;
	private float timer = 0f;

	public enum enDifficulte{facile,moyen,difficile};
	public enDifficulte difficulte = enDifficulte.facile;
	public float vitesseFacile = 5f;
	public float vitesseMoyen = 10f;
	public float vitesseDifficile= 15f;

	// Use this for initialization
	void Start () {
		actionsActuelles = actionsFaciles;
	}
	
	// Update is called once per frame
	void Update () {
		switch (difficulte) {
		case enDifficulte.facile:
			MovingAction.vitesse = 5f;
			actionsActuelles = actionsFaciles;
			break;
		case enDifficulte.moyen:
			MovingAction.vitesse = 10f;
			actionsActuelles = actionsMoyennes;
			break;
		case enDifficulte.difficile:
			MovingAction.vitesse = 15f;
			actionsActuelles = actionsDifficiles;
			break;
		}

		if (timer <= 0f) {
			var o = Instantiate (actionsActuelles [Random.Range (0, actionsActuelles.Length)], this.transform.position, Quaternion.identity);
			o.name = "Action";
			timer = interval;
		}

		timer -= Time.deltaTime;
	}
}
