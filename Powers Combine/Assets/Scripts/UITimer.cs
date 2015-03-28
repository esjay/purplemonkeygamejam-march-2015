using UnityEngine;
using System.Collections;

public class UITimer : MonoBehaviour {

	private float startTime;
	private float remainingTime;
	private float roundTime;

	private bool isTimerOn;


	// Use this for initialization
	void Start () {
		roundTime = 85.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimerOn) {
			remainingTime = roundTime - (Time.time - startTime);

			if (remainingTime <= 0) {
				stopTimer();
				remainingTime = 0.0f;
				this.updateWithTime( remainingTime );
				GameManager.instance.showLoseScreen();
			} 

			this.updateWithTime( remainingTime );
		}
	}

	public void resetTimer () {
		updateWithTime (roundTime);
	}

	public void startTimer () {
		this.isTimerOn = true;
		this.startTime = Time.time;
		Debug.Log ("Time started at " + startTime);
	}

	public void stopTimer() {
		this.isTimerOn = false;
	}

	private void updateWithTime (float theTimeToUse) {
		this.GetComponent<GUIText>().text = theTimeToUse.ToString("F2");
	}

}
