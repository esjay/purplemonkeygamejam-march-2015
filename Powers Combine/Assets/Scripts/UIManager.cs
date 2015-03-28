using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	// UI Elements
	public UITimer timer;
	public GUIText scoreText;

	void Awake () {
		Debug.Log ("Creating UI Manager");
		
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("Only one copy of gamemanager allowed!");
		}
		timer.GetComponent<GUIText>().enabled = false;
		scoreText.enabled = false;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setScore (int score) {
		scoreText.text = "Lives Changed: " + score;
	}

	public void startNewLevel () {
		timer.resetTimer ();
		timer.GetComponent<GUIText>().enabled = true;
		scoreText.enabled = true;
		timer.startTimer();
	}

	public void endLevel () {
		timer.stopTimer ();
		timer.GetComponent<GUIText>().enabled = false;
		scoreText.enabled = false;
	}
}
