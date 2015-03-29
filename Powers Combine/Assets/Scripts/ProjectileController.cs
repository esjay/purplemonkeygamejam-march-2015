using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	private float x, y;

	// Use this for initialization
	void Start () {
		this.x = Random.insideUnitCircle.x;
		this.y = Random.insideUnitCircle.y;
	}

	void Update () {
//		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (50, 50));
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 vector = new Vector2 (x*10, y*10);
		Debug.Log (x + " " + y);
//		GetComponent<Rigidbody2D> ().Ad
//		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (50, 50));
		GetComponent<Rigidbody2D> ().velocity = vector;
//		GetComponent<Rigidbody2D> ().rotation = 90;
	}
}
