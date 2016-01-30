using UnityEngine;
using System.Collections;

public class SpawnAction : MonoBehaviour {
	public GameObject[] actionsFaciles;
	public GameObject[] actionsMoyennes;
	public GameObject[] actionsDifficiles;
	private GameObject[] actionsActuelles;

	private float timer = 0f;

	public enum enDifficulte{facile,moyen,difficile};
	public enDifficulte difficulte = enDifficulte.facile;

	public float vitesseFacile = 5f;
	public float vitesseMoyen = 10f;
	public float vitesseDifficile= 15f;

	public float intervalFacile = 1f;
	public float intervalMoyen = 0.5f;
	public float intervalDifficile = 0.3f;
	private float intervalActuel;

	// Use this for initialization
	void Start () {
		actionsActuelles = actionsFaciles;
		intervalActuel = intervalFacile;
	}
	
	// Update is called once per frame
	void Update () {
		switch (difficulte) {
		case enDifficulte.facile:
			MovingAction.vitesse = vitesseFacile;
			actionsActuelles = actionsFaciles;
			intervalActuel = intervalFacile;
			break;
		case enDifficulte.moyen:
			MovingAction.vitesse = vitesseMoyen;
			actionsActuelles = actionsMoyennes;
			intervalActuel = intervalMoyen;
			break;
		case enDifficulte.difficile:
			MovingAction.vitesse = vitesseDifficile;
			actionsActuelles = actionsDifficiles;
			intervalActuel = intervalDifficile;
			break;
		}

		if (timer <= 0f) {
			var o = Instantiate (actionsActuelles [Random.Range (0, actionsActuelles.Length)], this.transform.position, Quaternion.identity);
			o.name = "Action";
			timer = intervalActuel;
		}

		timer -= Time.deltaTime;
	}
}
