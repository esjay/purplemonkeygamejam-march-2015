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

		GetComponent<Rigidbody2D>().MovePosition ( movementVector );

	}

	public void OnCollisionEnter2D(Collision2D collision) {

	}

	private void findNewGoal () {
		this.goalLocation = GameManager.findRandomPointOnMap ();
	}

}
