using UnityEngine;
using System.Collections;

public class SpawnAction : MonoBehaviour {
	public GameObject[] actionsFaciles;
	public GameObject[] actionsMoyennes;
	public GameObject[] actionsDifficiles;
	private GameObject[] actionsActuelles;
	public GameObject scoreBoard;

	private float timer = 0f;

	public float DureeDeLaPartie = 60f;

	public enum enumDifficulte{facile,moyen,difficile};
	public enumDifficulte difficulte = enumDifficulte.facile;
	private enumDifficulte difficulteFramePrecedente = enumDifficulte.facile;

	public float vitesseFacile = 5f;
	public float vitesseMoyenne = 10f;
	public float vitesseDifficile = 15f;
	public float tempsTransitionDifficulte = 2f;
	private float tempsLerp = 0f;
	private bool enTransitionVitesse = false;
	private float vitesseDepart;

	public float intervalFacile = 1f;
	public float intervalMoyen = 0.5f;
	public float intervalDifficile = 0.3f;
	private float intervalActuel;

	public float tempoEnSecondes = 0.5f;
	public int espacementActionMin = 1;
	public int espacementActionMax = 4;

	// Use this for initialization
	void Start () {
		actionsActuelles = actionsFaciles;
		intervalActuel = intervalFacile;
	}
	
	// Update is called once per frame
	void Update () {
		if (difficulte != difficulteFramePrecedente) {
			enTransitionVitesse = true;
			vitesseDepart = MovingAction.vitesse;
		}

		if (enTransitionVitesse) {
			tempsLerp += (1 / tempsTransitionDifficulte) * Time.deltaTime;

			switch (difficulte) {
			case enumDifficulte.facile:
				MovingAction.vitesse = Mathf.Lerp (vitesseDepart, vitesseFacile, tempsLerp);
				actionsActuelles = actionsFaciles;
				intervalActuel = intervalFacile;
				break;
			case enumDifficulte.moyen:
				MovingAction.vitesse = Mathf.Lerp (vitesseDepart, vitesseMoyenne, tempsLerp);
				actionsActuelles = actionsMoyennes;
				intervalActuel = intervalMoyen;
				break;
			case enumDifficulte.difficile:
				MovingAction.vitesse = Mathf.Lerp (vitesseDepart, vitesseDifficile, tempsLerp);
				actionsActuelles = actionsDifficiles;
				intervalActuel = intervalDifficile;
				break;
			}

			if (tempsLerp >= 1f) {
				enTransitionVitesse = false;
			}
		} else {
			tempsLerp = 0f;
		}

		if (timer <= 0f && DureeDeLaPartie > 5f) {
			var o = Instantiate (actionsActuelles [Random.Range (0, actionsActuelles.Length)], this.transform.position, Quaternion.identity);
			o.name = "Action";
			timer = intervalActuel * tempoEnSecondes * Random.Range(espacementActionMin,espacementActionMax);
		}

		if (DureeDeLaPartie <= 0) {
			scoreBoard.SetActive(true);
		}

		timer -= Time.deltaTime;
		DureeDeLaPartie -= Time.deltaTime;

		difficulteFramePrecedente = difficulte;
	}
}
