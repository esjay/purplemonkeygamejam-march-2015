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
	public float introDelay;

	public List<GameObject> peopleRefs;
	public GameObject player1;
	public GameObject player2;
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
		this.currentScore = 0;
	}


	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		if (!this.gameStarted 
		    && Input.GetKeyDown (KeyCode.Space)
		    && (Time.time - this.timeSinceLevelChange) > introDelay) {
			this.startNewLevel();
		}
	}

	private void startNewLevel (){
		this.clearScreen ();

		this.spawnPlayer(this.player1, 1);
		this.spawnPlayer(this.player2, 2);
//		this.spawnPeople ();

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
		// TODO: get rid of enemies.
		while (this.peopleRefs.Count > 0) {
			GameObject person = this.peopleRefs [0];
			this.peopleRefs.RemoveAt (0);
			Destroy (person);
		}

		Destroy (this.player1);
		Destroy (this.player2);
	}

	// Screen Transitions
	public void showWinScreen () {
		this.endLevel ();
		BackgroundManager.instance.showWin ();
		SoundManager.instance.playWinMusic ();
	}

	public void showLoseScreen () {
		this.endLevel ();
	}


	// TODO: change this to enemies. add this to "update fixed"
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

	private void spawnPlayer(GameObject player, int playerNumber) {
		Debug.Log("Spawning player" + playerNumber );

		if (player == null) {
			player = (GameObject)Instantiate(playerPrefab);
		}
		player.GetComponent<PlayerScript> ().playerNumber = playerNumber;
		player.GetComponent<Rigidbody2D>().position = new Vector2(0,-1.5f*playerNumber);

	}
	
	public static Vector2 findRandomPointOnMap() {
		return new Vector2(Random.Range(-GameManager.instance.widthOfLevel, GameManager.instance.widthOfLevel),
		                   Random.Range(-GameManager.instance.heightOfLevel + 1.2f, GameManager.instance.heightOfLevel - 1.1f));
	}


	public void changeScoreByAmount( int amount) 
	{
		this.currentScore += amount;
		UIManager.instance.setScoreTextWithScore (this.currentScore);
	}

}