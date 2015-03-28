using UnityEngine;
using System.Collections;

public class WalkCycleScript : MonoBehaviour {

	private Animator animator;
	private Vector2 lastPos;
	private int currentDirection = 0;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		lastPos = new Vector2 (GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y);
	}
	
	void FixedUpdate() {
		doMovement ();
	}

	void doMovement () {
		Vector2 currentPos, currentVector;
		
		currentPos = GetComponent<Rigidbody2D>().position;
		currentVector = new Vector2 (lastPos.x - currentPos.x, lastPos.y - currentPos.y);
		this.lastPos.x = currentPos.x;
		this.lastPos.y = currentPos.y;
		float currentDistance = currentVector.magnitude;
		
		bool isMoving = false;
		if (currentDistance > 0.01) {
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
		
		setAnimationState (this.currentDirection, isMoving);
	}
	
	void setAnimationState (int direction, bool isMoving) {
		animator.SetInteger("Direction", direction);
		animator.SetBool ("IsMoving", isMoving);
	}
}
