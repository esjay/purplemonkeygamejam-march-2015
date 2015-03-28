using UnityEngine;
using System.Collections;

public class FistScript : MonoBehaviour {
	
	public AudioClip hitSound;
	
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Werewolf(Clone)") {
			GameManager.instance.showWinScreen ();
		} else {
			if (GameManager.instance.gameStarted) {
				this.audioSource.clip = this.hitSound;
				this.audioSource.Play ();
			}
		}
	}
}
