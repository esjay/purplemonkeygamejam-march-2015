using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	// UI Elements
	public GUIText scoreText;

	void Awake () {
		Debug.Log ("Creating UI Manager");
		
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("Only one copy of ui manager allowed!");
		}
		scoreText.enabled = false;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setScoreTextWithScore (int score) {
		scoreText.text = score.ToString();
	}

	public void startNewLevel () {
		scoreText.enabled = true;
	}

	public void endLevel () {
		scoreText.enabled = false;
	}
}
