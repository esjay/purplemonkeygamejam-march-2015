using UnityEngine;
using System.Collections;

public class SiloScript : MonoBehaviour {

	private int nextUpdate;
	private bool isVisible = false;
	public int minWait = 30 * 5;
	public int maxWait = 30 * 8;

	// Use this for initialization
	void Start () {
		this.nextUpdate = Time.frameCount + this.randomOffset ();
		this.hide ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int currentFrame;
		currentFrame = Time.frameCount;
		if (this.isVisible) {
			this.hide();
		}
		if (currentFrame >= this.nextUpdate) {
			this.flash ();
		}
		if (!GameManager.instance.gameStarted) {
			this.hide ();
		}
	}

	void flash () {
		Debug.Log ("Flash");
		this.nextUpdate = Time.frameCount + randomOffset ();
		this.show ();
	}

	int randomOffset () {
		return Random.Range (this.minWait, this.maxWait);
	}

	void show () {
		this.isVisible = true;
		this.GetComponent<Renderer>().enabled = true;
		Debug.Log ("HERE");
	}

	void hide () {
		this.isVisible = false;
		this.GetComponent<Renderer>().enabled = false;
	}
}
