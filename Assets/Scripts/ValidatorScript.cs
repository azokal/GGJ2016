using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValidatorScript : MonoBehaviour {

	// Use this for initialization

	public int combo = 1;
	public int maxSerie = 0;
	public int serie = 0;
	public int score = 0;
	public float animationTime = 0.5f;
	public float tmpGood;
	public float tmpBad;
	private float tmpMinions;
	public ParticleSystem goodParticle;
	public ParticleSystem badParticle;
	private string animToLaunch;

	public AudioClip[] soundArray = new AudioClip[25];
	public AudioClip[] failArray = new AudioClip[2];

	void Start () {
		tmpGood = animationTime;
		tmpBad = animationTime;
	}

	string SimpleUpCheck() {
		if (Input.GetKey (KeyCode.UpArrow)) {
			animToLaunch = "Up";
			return "SimpleUp";
		}
		return "";
	}

	string SimpleDownCheck() {
		if (Input.GetKey (KeyCode.DownArrow)) {
			animToLaunch = "Down";
			return "SimpleDown";
		}
		return "";
	}

	string SimpleLeftCheck() {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			animToLaunch = "Left";
			return "SimpleLeft";
		}
		return "";
	}
	
	string SimpleRightCheck() {
		if (Input.GetKey (KeyCode.RightArrow)) {
			animToLaunch = "Right";
			return "SimpleRight";
		}
		return "";
	}

	string DoubleUpCheck() {
		if (Input.GetKey (KeyCode.Alpha5)) {
			animToLaunch = "doubleTop";
			return "DoubleUp";
		}
		return "";
	}
	
	string DoubleDownCheck() {
		if (Input.GetKey (KeyCode.Alpha6)) {
			animToLaunch = "doubleDown";
			return "DoubleDown";
		}
		return "";
	}

	string InvertUpCheck() {
		if (Input.GetKey (KeyCode.Alpha7)) {
			animToLaunch = "invertUp";
			return "InvertUp";
		}
		return "";
	}
	
	string InvertDownCheck() {
		if (Input.GetKey (KeyCode.Alpha8)) {
			animToLaunch = "invertDown";
			return "InvertDown";
		}
		return "";
	}

	string TurnLeftCheck() {
		if (Input.GetKey (KeyCode.Alpha9)) {
			animToLaunch = "upDown";
			return "TurnLeft";
		}
		return "";
	}
	
	string TurnRightCheck() {
		if (Input.GetKey (KeyCode.Alpha0)) {
			animToLaunch = "downUp";
			return "TurnRight";
		}
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

	void ActionPlayed(string ci) {
		if (ci == "SimpleUp")
			animToLaunch = "Up";
		if (ci == "SimpleDown")
			animToLaunch = "Down";
		if (ci == "SimpleLeft")
			animToLaunch = "Left";
		if (ci == "SimpleRight")
			animToLaunch = "Right";
		if (ci == "DoubleUp")
			animToLaunch = "doubleTop";
		if (ci == "DoubleDown")
			animToLaunch = "doubleDown";
		if (ci == "InvertUp")
			animToLaunch = "downUp";
		if (ci == "InvertDown")
			animToLaunch = "upDown";

		Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
		var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
		foreach (var hit in hits) {
			if (hit.name == "Action") {
				if (ci != "") {
					if (ci == hit.gameObject.GetComponent<ActionType>().eventType) {
						serie += 1;
						Combo();
						score += 1 * combo;

						var ma = hit.gameObject.GetComponent<MovingAction>();
						hit.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						hit.gameObject.GetComponent<MovingAction>().fadeIn = false;
						hit.gameObject.GetComponent<MovingAction>().currentTimeToFade = hit.gameObject.GetComponent<MovingAction>().fadingTime;
						//Destroy(hit.gameObject);
						tmpGood = 0f;
						goodParticle.Play();
						GameObject[] charaMultiples = GameObject.FindGameObjectsWithTag("Chara");
						foreach(GameObject chara in charaMultiples){
							chara.GetComponent<Animator>().Play(animToLaunch);
						}
						if(!GameObject.Find ("Charas").GetComponent<AudioSource>().isPlaying){
							GameObject.Find ("Charas").GetComponent<AudioSource>().clip = soundArray[Random.Range (0, 25)];
							GameObject.Find ("Charas").GetComponent<AudioSource>().Play ();
						}
					}else{
						serie = 0;
						Combo();
						tmpBad = 0f;

						var ma = hit.gameObject.GetComponent<MovingAction>();
						hit.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						hit.gameObject.GetComponent<MovingAction>().fadeIn = false;
						hit.gameObject.GetComponent<MovingAction>().currentTimeToFade = hit.gameObject.GetComponent<MovingAction>().fadingTime;
						//Destroy(hit.gameObject);
						badParticle.Play();
						GameObject.Find ("Charas").GetComponent<AudioSource>().clip = failArray[Random.Range (0, 2)];
						GameObject.Find ("Charas").GetComponent<AudioSource>().Play ();
						GameObject[] charaMultiples = GameObject.FindGameObjectsWithTag("Chara");
						foreach(GameObject chara in charaMultiples){
							chara.GetComponent<Animator>().Play("Fail");
						}
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "button"){
			serie = 0;
			Combo ();
			tmpBad = 0f;
			var ma = collider.gameObject.GetComponent<MovingAction>();
			collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			collider.gameObject.GetComponent<MovingAction>().fadeIn = false;
			collider.gameObject.GetComponent<MovingAction>().currentTimeToFade = collider.gameObject.GetComponent<MovingAction>().fadingTime;
			//Destroy(collider.gameObject);
			badParticle.Play ();
		}
	}

	// Update is called once per frame
	void Update () {
		animGoodBar ();
		animBadBar ();
		if (!(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)) {
			Vector2 pos = new Vector2 (this.transform.position.x, this.transform.position.y);
			var hits = Physics2D.OverlapCircleAll(pos, 0.1f);
			foreach (var hit in hits) {
				if (hit.name == "Action") {
					string ci = "";
					ci = CheckInput();
					if (ci != "") {
						if (ci == hit.gameObject.GetComponent<ActionType>().eventType) {
							serie += 1;
							Combo();
							score += 1 * combo;

							var ma = hit.gameObject.GetComponent<MovingAction>();
							hit.gameObject.GetComponent<BoxCollider2D>().enabled = false;
							hit.gameObject.GetComponent<MovingAction>().fadeIn = false;
							hit.gameObject.GetComponent<MovingAction>().currentTimeToFade = hit.gameObject.GetComponent<MovingAction>().fadingTime;

							tmpGood = 0f;
							//functionDebug();
							goodParticle.Play();

							GameObject[] charaMultiples = GameObject.FindGameObjectsWithTag("Chara");
							foreach(GameObject chara in charaMultiples){
								chara.GetComponent<Animator>().Play(animToLaunch);
							}

							if(!GameObject.Find ("Charas").GetComponent<AudioSource>().isPlaying){
								GameObject.Find ("Charas").GetComponent<AudioSource>().clip = soundArray[Random.Range (0, 25)];
								GameObject.Find ("Charas").GetComponent<AudioSource>().Play ();
//								GameObject[] charaMultiples = GameObject.FindGameObjectsWithTag("Chara");
//								foreach(GameObject chara in charaMultiples){
//									chara.GetComponent<Animator>().Play(animToLaunch);
//								}
							}
						}else{
							serie = 0;
							Combo();
							tmpBad = 0f;
							//functionDebug();

							var ma = hit.gameObject.GetComponent<MovingAction>();
							hit.gameObject.GetComponent<BoxCollider2D>().enabled = false;
							hit.gameObject.GetComponent<MovingAction>().fadeIn = false;
							hit.gameObject.GetComponent<MovingAction>().currentTimeToFade = hit.gameObject.GetComponent<MovingAction>().fadingTime;

							badParticle.Play();
							GameObject.Find ("Charas").GetComponent<AudioSource>().clip = failArray[Random.Range (0, 2)];
							GameObject.Find ("Charas").GetComponent<AudioSource>().Play ();
							GameObject[] charaMultiples = GameObject.FindGameObjectsWithTag("Chara");
							foreach(GameObject chara in charaMultiples){
								chara.GetComponent<Animator>().Play("Fail");
							}
						}
					}
				}
			}
		}
	}

	public void Combo(){
		if (serie > maxSerie)
			maxSerie = serie;
		var spawnAction = GameObject.Find ("Spawn Action").GetComponent<SpawnAction> ();
		if (serie < 15){
			if(combo==2){
				//minions2to1();
			}
			combo = 1;
			spawnAction.difficulte = SpawnAction.enumDifficulte.facile;
		}
		if (serie >= 15 && combo < 40){
			if(combo==1){
				//minions1to2();
				combo = 2;
				spawnAction.difficulte = SpawnAction.enumDifficulte.moyen;
			}else if(combo==3){
				//minions3to2();
				combo = 2;
				spawnAction.difficulte = SpawnAction.enumDifficulte.moyen;
			}
		}
		if (serie >= 40){
			if(combo==2){
				//minions2to3();
			}
			combo = 4;
			spawnAction.difficulte = SpawnAction.enumDifficulte.difficile;
		}
	}

	/*public void minions1to2(){
		if (tmpMinions < animationTime){
			if (tmpMinions < animationTime/2)
				GameObject.Find("Chara5").GetComponentsInChildren<SpriteRenderer>().color = new Color (1, 1, 1, tmpGood/(animationTime/2));
			else
				GameObject.Find("Barre_Good").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, (animationTime - tmpGood) / (animationTime/2));
			
			tmpGood += Time.deltaTime;
		}
	}

	public void minions2to3(){

	}

	public void minions2to1(){

	}

	public void minions3to2(){

	}*/

	void functionDebug(){
		Debug.Log ("Combo : "+ combo);
		Debug.Log ("Série : "+ serie);
		Debug.Log ("Score : "+ score);
		Debug.Log ("Difficulté : "+ GameObject.Find ("Spawn Action").GetComponent<SpawnAction> ().difficulte);
	}
		
	void animGoodBar(){
		if (tmpGood < animationTime){
			if (tmpGood < animationTime/2)
				GameObject.Find("Barre_Good").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, tmpGood/(animationTime/2));
			else
				GameObject.Find("Barre_Good").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, (animationTime - tmpGood) / (animationTime/2));

			tmpGood += Time.deltaTime;
		}
	}

	void animBadBar(){
		if (tmpBad < animationTime){
			if (tmpBad < animationTime/2)
				GameObject.Find("Barre_Bad").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, tmpBad/(animationTime/2));
			else
				GameObject.Find("Barre_Bad").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, (animationTime - tmpBad) / (animationTime/2));
			
			tmpBad += Time.deltaTime;
		}
	}
}