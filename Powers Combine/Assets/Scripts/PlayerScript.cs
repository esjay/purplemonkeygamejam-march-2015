using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int playerNumber;
	public Sprite firstPlayerSprite;
	public Sprite secondPlayerSprite;
	public float maxSpeed = 0.1f;
	private Animator animator;
	private Vector2 lastPos;
	private float currentDirection = 0.0f;
	private bool isPunching = false;
	private int punchingSince;
	private GameObject fist;

	public int punchDuration = 25;
	public int coolDownDuration = 35;
	public GameObject fistPrefab;
	public GameObject laserPrefab;
	public AudioClip swingSound;
	
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {

		GetComponent<SpriteRenderer> ().sprite = playerNumber == 1 ? 
			(Sprite)Instantiate (this.firstPlayerSprite) : (Sprite)Instantiate (this.secondPlayerSprite);
		animator = this.GetComponent<Animator>();
		lastPos = GetComponent<Rigidbody2D>().position;

		this.fist = (GameObject)Instantiate (fistPrefab);
		this.endPunch ();
	}
	
	void FixedUpdate() {
		doMovement ();
		doActions ();
		updateFistPos ();
		GameManager.instance.changeScoreByAmount (10);
	}

	void doMovement () {

		// Player Movement
//		Debug.Log ("Player" + this.playerNumber + "_Horizontal");
		float w = Input.GetAxis ("Player" + this.playerNumber + "_Horizontal");
		float h = Input.GetAxis ("Player" + this.playerNumber + "_Vertical");
//		Debug.Log (w + ", " + h);
		float distance = Mathf.Sqrt (Mathf.Pow (h, 2) + Mathf.Pow (w, 2));
		float direction = Mathf.Atan2 (h, w);
//		Debug.Log ("direction:" + direction);


		distance = Mathf.Min (distance, maxSpeed);
		Vector2 movement = new Vector2 (distance * Mathf.Cos (direction),
		                                distance * Mathf.Sin (direction));
		
		Vector2 next = GetComponent<Rigidbody2D>().position + movement;
		GetComponent<Rigidbody2D>().MovePosition(next);	
		
//		applyAnimationFromMovement (-w, -h);

		// Player Direction
		float x = Input.GetAxis ("Player" + this.playerNumber + "_AimHorizontal");
		float y = Input.GetAxis ("Player" + this.playerNumber + "_AimVertical");
		distance = Mathf.Sqrt (Mathf.Pow (y, 2) + Mathf.Pow (-x, 2));
		direction = Mathf.Atan2 (y, -x);

//		Debug.Log (x + ", " + y);
		if (Mathf.Abs(x) > 0.01 || Mathf.Abs (y) > 0.01) {
			transform.rotation = Quaternion.Euler (0f, 0f, Mathf.Atan2 (x, y) * Mathf.Rad2Deg);
			this.currentDirection = direction;
//			Debug.Log (transform.rotation);
		}
	}

	void doActions () {
		if (Input.GetKeyDown (KeyCode.Space)) {

			GameObject projectile = (GameObject)Instantiate(this.laserPrefab);
//			Vector3 offsetVector = new Vector3(GetComponent<Rigidbody2D>().ro
			projectile.transform.position = transform.position;

			Vector2 movement = new Vector2 (1 * Mathf.Cos (this.currentDirection),
			                                1 * Mathf.Sin (this.currentDirection));
			
			Vector2 next = GetComponent<Rigidbody2D>().position + movement;
			projectile.GetComponent<Rigidbody2D>().MovePosition(next);	
			


			projectile.transform.rotation = transform.rotation;
			projectile.GetComponent<ProjectileController>().direction = this.currentDirection;

			Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
//			Vector3 rotation = GetComponent<Rigidbody2D>().transform.rotation.;
//			offset.z = 0;

//			Vector3 movement = new Vector3 (1 * Mathf.Cos (this.currentDirection),
//			                                1 * Mathf.Sin (this.currentDirection),
//			                                0);
//			projectile.transform.position = projectile.transform.position + movement;// + offset*2;
//			Debug.Log (offsetVector.x + ", " + offsetVector.y);
//			Vector2 movement = new Vector2 (distance * Mathf.Cos (direction),
//			                                distance * Mathf.Sin (direction));
//			GetComponent<SpriteRenderer> ().sprite = playerNumber == 1 ? 
//				(Sprite)Instantiate (this.firstPlayerSprite) : (Sprite)Instantiate (this.secondPlayerSprite);
//			projectile.setDirection();
		}
	}

	void beginPunch () {
		this.fist.GetComponent<Rigidbody2D>().GetComponent<Collider2D>().enabled = true;
		this.audioSource.clip = this.swingSound;
		this.audioSource.Play ();
		//this.fist.renderer.enabled = true;
	}

	void endPunch () {
		this.fist.GetComponent<Rigidbody2D>().GetComponent<Collider2D>().enabled = false;
		this.fist.GetComponent<Renderer>().enabled = false;
	}

	// Keep for potential use later.
	void applyAnimationFromMovement (float w, float h) {
		Vector2 currentPos;
		
		currentPos = GetComponent<Rigidbody2D>().position;
		Vector2 currentVector;

		if (Mathf.Abs (w) > 0.1 || Mathf.Abs (h) > 0.1) {
			currentVector = new Vector2 (w, h);	
		} else {
			currentVector = new Vector2 (lastPos.x - currentPos.x, lastPos.y - currentPos.y);
		}
		this.lastPos = currentPos;
		
		float currentDistance = currentVector.magnitude;
		
		bool isMoving = false;
		if (currentDistance > 0.1) {
			isMoving = true;
		}
		
		bool isYDominant = Mathf.Abs (currentVector.y) > Mathf.Abs (currentVector.x);
		int direction;
		if (isYDominant) {
			if (currentVector.y > 0) {
				//direction = "north";
				direction = 0;
			} else {
				//direction = "south";
				direction = 2;
			}
		} else {
			if (currentVector.x > 0) {
				//direction = "east";
				direction = 1;
			} else {
				//direction = "west";
				direction = 3;
			}
		}
		
		if (isMoving) {
			this.currentDirection = direction;
		}
		
//		setAnimationState (this.currentDirection, isMoving);
	}

	void updateFistPos () {
		Vector2 nextPos = new Vector2(0,0);

		if (this.currentDirection == 0) {
			nextPos.x = this.GetComponent<Rigidbody2D>().position.x + 0;
			nextPos.y = this.GetComponent<Rigidbody2D>().position.y - 0.5f;
		} else if (this.currentDirection == 1) {
			nextPos.x = this.GetComponent<Rigidbody2D>().position.x - 0.25f;
			nextPos.y = this.GetComponent<Rigidbody2D>().position.y + 0;
		} else if (this.currentDirection == 2) {
			nextPos.x = this.GetComponent<Rigidbody2D>().position.x + 0;
			nextPos.y = this.GetComponent<Rigidbody2D>().position.y + 0.25f;
		} else if (this.currentDirection == 3) {
			nextPos.x = this.GetComponent<Rigidbody2D>().position.x + 0.25f;
			nextPos.y = this.GetComponent<Rigidbody2D>().position.y + 0;
		}

		this.fist.GetComponent<Rigidbody2D>().MovePosition (nextPos);
	}

	void setAnimationState (int direction, bool isMoving) {
		animator.SetInteger("Direction", direction);
		animator.SetBool ("IsMoving", isMoving);
	}

	void applyAnimationFromState (bool isPunching) {
		animator.SetBool ("IsPunching", isPunching);
	}
}
