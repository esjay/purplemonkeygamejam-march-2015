using UnityEngine;
using System.Collections;

public class PersonManager : MonoBehaviour {
	// Our speed is in between these values to create more chaos.
	public float maxSpeed;
	public float minSpeed;
	private float speed;

	// Our speed is in between these values to create more chaos.
	public Vector2 goalLocation;

	// Use this for initialization
	void Start () {
		findNewGoal ();
		speed = Random.Range (minSpeed, maxSpeed);

	}
	
	// Update is called once per frame
	void Update () {
		// Do animation updates here.
	}

	void FixedUpdate () {
		if (GetComponent<Rigidbody2D>().position == goalLocation) {
			findNewGoal();
		}

		Vector2 movementVector = Vector2.MoveTowards (GetComponent<Rigidbody2D>().position, goalLocation, speed);
//		float x = Input.GetAxis ("Player" + this.playerNumber + "_AimHorizontal");
//		float y = Input.GetAxis ("Player" + this.playerNumber + "_AimVertical");
//		distance = Mathf.Sqrt (Mathf.Pow (y, 2) + Mathf.Pow (-x, 2));
//		direction = Mathf.Atan2 (y, -x);
//		
//		//		Debug.Log (x + ", " + y);
//		if (Mathf.Abs(x) > 0.01 || Mathf.Abs (y) > 0.01) {
			
		GetComponent<Rigidbody2D>().MovePosition ( movementVector );

	}

//	public void OnCollisionEnter2D(Collision2D collision) {
//		Debug.Log ("HEY");
//		if (collision.gameObject.name == "Projectile") {
//			Destroy(this);
//		}
//	}
//	public void OnTriggerEnter( Collider2D other) {
//		Debug.Log ("HEY2");
//		if (other.gameObject.name == "Projectile") {
//			Destroy(this);
//		}
//	}
	private void findNewGoal () {
		this.goalLocation = GameManager.findRandomPointOnMap ();
		transform.rotation = Quaternion.Euler (0f, 0f, Mathf.Atan2 (-goalLocation.x, goalLocation.y) * Mathf.Rad2Deg);
	}

}
