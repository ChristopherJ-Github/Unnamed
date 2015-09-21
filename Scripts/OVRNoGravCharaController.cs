using UnityEngine;
using System.Collections;

public class OVRNoGravCharaController : Singleton<OVRNoGravCharaController> {

	public Transform directionReference;
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
			movement = directionReference.forward;
			velocity += movement;
			noKeyPressed = false;
		}
		
		if (Input.GetKey (KeyCode.A)) {
			movement = -directionReference.right;
			velocity += movement;
			noKeyPressed = false;
		}
		
		if (Input.GetKey (KeyCode.S)) {
			movement = -directionReference.forward;
			velocity += movement;
			noKeyPressed = false;
		}
		
		if (Input.GetKey (KeyCode.D)) {
			movement = directionReference.right;
			velocity += movement;
			noKeyPressed = false;
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
