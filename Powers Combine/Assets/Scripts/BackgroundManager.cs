using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

	public static BackgroundManager instance;

	public Sprite mapBackground;
	public Sprite introBackground;
	public Sprite winBackground;
	public Sprite winPunchBackground;
	public Sprite winFinalBackground;
	public Sprite loseBackground;


	private bool isWinning;
	private float winTime;
	
	public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
		Debug.Log ("Creating Background Manager");
		
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("Only one copy of gamemanager allowed!");
		}

		
	}

	// Use this for initialization
	void Start () {
		this.showIntro();
		this.isWinning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isWinning) {
			if ( (Time.time - this.winTime) < 1 ) {
				this.showWin();
			} else if ( (Time.time - this.winTime) < 3) {
				this.showWinPunch();
			} else {
				this.showWinFinal();
				this.isWinning = false;
			}
		}
	}

	public void showMap () {
		this.spriteRenderer.sprite = mapBackground;
	}

	public void showIntro () {
		this.spriteRenderer.sprite = introBackground;
	}

	public void showWin () {
		this.spriteRenderer.sprite = winBackground;
		if (!isWinning) {
			this.winTime = Time.time;
			isWinning = true;
		}
	}

	private void showWinPunch () {
		this.spriteRenderer.sprite = winPunchBackground;
	}

	private void showWinFinal () {
		this.spriteRenderer.sprite = winFinalBackground;
	}

	public void showLose () {
		this.spriteRenderer.sprite = loseBackground;
	}
}
