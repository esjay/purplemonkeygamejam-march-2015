using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public AudioClip music;
	public AudioClip chaseMusic;
	public AudioClip loseMusic;
	public AudioClip winMusic;
	

	public AudioSource audioSource;

	private bool isPaused;

	private int counterForNextClip;

	// Use this for initialization
	void Awake () {
		Debug.Log ("Creating Sound Manager");
		
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("Only one copy of gamemanager allowed!");
		}

		this.isPaused = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (this.audioSource.isPlaying) {
			// do nothing
		} else if (!isPaused) {
			this.counterForNextClip++;
			if (this.counterForNextClip == 1) {
				// play second track
				this.audioSource.clip = this.chaseMusic;
				this.audioSource.Play ();
			} else if (this.counterForNextClip == 0) {
				this.audioSource.Play ();
			}

			// Do nothing if no more sound is available.

		}
	}

	public void playLoseMusic() {
		this.isPaused = true;
		this.audioSource.Stop ();
		this.audioSource.clip = loseMusic;
		this.audioSource.Play ();
	}

	public void playWinMusic() {
		this.isPaused = true;
		this.audioSource.Stop ();
		this.audioSource.clip = winMusic;
		this.audioSource.Play ();
	}
	
	public void pause() {
	}
	
	public void unpause() {

	}
	
	public void startNewLevel() {
		this.audioSource.Stop ();
		this.counterForNextClip = 0;
		this.audioSource.clip = music;
		this.isPaused = false;
		this.audioSource.Play ();

	}
	
	public void endLevel() {
		this.isPaused = true;
		this.audioSource.Stop ();
	}
}



