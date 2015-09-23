using UnityEngine;
using System.Collections;

/// <summary>
/// Player controller that doesn't use high gravity.
/// </summary>
public class NoGravCharaController : Singleton<NoGravCharaController> {

	void Update () {
		
		GetInput ();
	}
	
	void GetInput() {
	
		Vector3 velocity = GetPlayerMovement ();
		MovePlayer (velocity);
		CheckForReset ();
	}

	public Vector3 additonalGravity; 
	private bool noKeyPressed;
	Vector3 GetPlayerMovement () {

		Vector3 velocity = new Vector3 ();
		noKeyPressed = true;
		if (Input.GetKey (KeyCode.W)) {
			velocity += new Vector3(0,0,1);
			noKeyPressed = false;
		}
		if (Input.GetKey (KeyCode.A)) {
			velocity += new Vector3(-1,0,0);
			noKeyPressed = false;
		}
		if (Input.GetKey (KeyCode.S)) {
			velocity += new Vector3(0,0,-1);
			noKeyPressed = false;
		}
		if (Input.GetKey (KeyCode.D)) {
			velocity += new Vector3(1,0,0);
			noKeyPressed = false;
		}
		return velocity;
	}
	
	public float speed;
	public float deceleration;
	void MovePlayer (Vector3 velocity) {

		rigidbody.AddRelativeForce(speed * velocity + additonalGravity);
		if (noKeyPressed || Input.GetKey(KeyCode.Space)) { 
			rigidbody.velocity = rigidbody.velocity * deceleration;
		}
	}

	void CheckForReset () {

		if (Input.GetKeyDown(KeyCode.R)) {
			WallManager.instance.Reset();
		}
	}
}
