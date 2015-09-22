using UnityEngine;
using System.Collections;

public class NoGravCharaController : Singleton<NoGravCharaController> {

	public float speed;
	public float deceleration;
	public Vector3 additonalGravity;
	bool noKeyPressed;
	
	void OnEnable () {

	}

	void movement() {

		Vector3 velocity = new Vector3 ();
		Vector3 movement;
		
		noKeyPressed = true;
		if (Input.GetKey (KeyCode.W)) {
			movement = new Vector3(0,0,1);
			velocity += movement;
			noKeyPressed = false;
		}
		
		if (Input.GetKey (KeyCode.A)) {
			movement = new Vector3(-1,0,0);
			velocity += movement;
			noKeyPressed = false;
		}
		
		if (Input.GetKey (KeyCode.S)) {
			movement = new Vector3(0,0,-1);
			velocity += movement;
			noKeyPressed = false;
		}
		
		if (Input.GetKey (KeyCode.D)) {
			movement = new Vector3(1,0,0);
			velocity += movement;
			noKeyPressed = false;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			WallManager.instance.Reset();
		}
		
		rigidbody.AddRelativeForce(speed*velocity + additonalGravity);
		if (noKeyPressed || Input.GetKey(KeyCode.Space)) { //has to be last
			rigidbody.velocity = rigidbody.velocity * deceleration;
		}

	}

	void Update () {

		movement ();
	}
}
