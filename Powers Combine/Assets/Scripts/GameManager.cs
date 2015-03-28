using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool gameStarted;


	// Modifiable information
	public float widthOfLevel;
	public float heightOfLevel;
	public int numberOfPeople;

	public List<GameObject> peopleRefs;
	public GameObject player;
	public GameObject werewolf;

	// Various variables
	private float timeSinceLevelChange;

	// Game Rules
	private int currentScore;

	// Prefabs.
	public GameObject playerPrefab;
	public GameObject personWomanPrefab;
	public GameObject personPrefab;
	public GameObject werewolfPrefab;

	public void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("Only one copy of gamemanager allowed!");
		}
		this.gameStarted = false;
		this.timeSinceLevelChange = Time.time;
		this.currentScore = 0;
	}


	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		if (!this.gameStarted 
		    && Input.GetKeyDown (KeyCode.Space)
		    && (Time.time - this.timeSinceLevelChange) > 5.0f) {
			this.startNewLevel();
		}
	}



	private void startNewLevel (){
		this.clearScreen ();

		this.spawnPlayer();
		this.spawnPeople ();
		this.spawnWerewolf ();

		SoundManager.instance.startNewLevel ();
		UIManager.instance.startNewLevel ();
		BackgroundManager.instance.showMap ();
		this.gameStarted = true;
	}

	private void endLevel () {
		this.clearScreen ();
		SoundManager.instance.endLevel ();
		UIManager.instance.endLevel ();
		timeSinceLevelChange = Time.time;
		this.gameStarted = false;
	}

	private void clearScreen () {
		while (this.peopleRefs.Count > 0) {
			GameObject person = this.peopleRefs [0];
			this.peopleRefs.RemoveAt (0);
			Destroy (person);
		}

		Destroy (this.player);
		Destroy (this.werewolf);
	}


	public void showWinScreen () {
		this.endLevel ();
		this.currentScore++;
		BackgroundManager.instance.showWin ();
		UIManager.instance.setScore(this.currentScore);
		SoundManager.instance.playWinMusic ();
	}

	public void showLoseScreen () {
		this.endLevel ();
		BackgroundManager.instance.showLose ();
		SoundManager.instance.playLoseMusic ();
	}



	private void spawnPeople() {
		Debug.Log ("Spawning " + this.numberOfPeople + " people.");
		for (int i = 0; i < this.numberOfPeople; ++i) {
			GameObject person;
			int random = Random.Range(0,2);
			if (random == 0) {
				person = (GameObject)Instantiate(personPrefab);
			}
			else {
				person = (GameObject)Instantiate(personWomanPrefab);
			}
			this.peopleRefs.Add(person);
			person.GetComponent<Rigidbody2D>().position = findRandomPointOnMap();
		}
	}

	private void spawnPlayer() {
		Debug.Log("Spawning player!");

		this.player = (GameObject)Instantiate(playerPrefab);
		this.player.GetComponent<Rigidbody2D>().position = new Vector2(0,-1.5f);

	}

	private void spawnWerewolf () {
		Debug.Log ("Spawning Werewolf");

		this.werewolf = (GameObject)Instantiate (werewolfPrefab);
		this.werewolf.GetComponent<Rigidbody2D>().position = findRandomPointOnMap ();
	}


	public static Vector2 findRandomPointOnMap() {
		return new Vector2(Random.Range(-GameManager.instance.widthOfLevel, GameManager.instance.widthOfLevel),
		                   Random.Range(-GameManager.instance.heightOfLevel + 1.2f, GameManager.instance.heightOfLevel - 1.1f));
	}
}


