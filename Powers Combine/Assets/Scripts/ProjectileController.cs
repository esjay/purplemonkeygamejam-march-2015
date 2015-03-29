using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public float direction;
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
//		this.x = transform.rotation.z;
//		this.y = transform.rotation.w;
		Vector2 movement = new Vector2 (2 * Mathf.Cos (direction),
		                                2 * Mathf.Sin (direction));

//		Vector2 vector = new Vector2 (x*10, y*10);
//		Debug.Log (transform.rotation.w + " " +
//		           transform.rotation.x + " " +
//		           transform.rotation.y + " " +
//		           transform.rotation.z);
////		GetComponent<Rigidbody2D> ().Ad
//		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (50, 50));
		GetComponent<Rigidbody2D> ().velocity = movement * 2;
//		GetComponent<Rigidbody2D> ().rotation = 90;
	}
}
