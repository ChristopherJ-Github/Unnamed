using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class SmoothMouseLook : MonoBehaviour {
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;

	public delegate void stateHandler();
	stateHandler state;
	float speed;
	public float deceleration;

	void Start () {
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		Screen.lockCursor = true;
		state = moving;
	}

	void moving () {

		if (Input.GetAxis("Mouse X") == 0) {
			state = decelerating;
			decelerating ();
			return;
		}
		speed = Input.GetAxis ("Mouse X") * sensitivityX;
		transform.Rotate(0, speed, 0);

	}

	void decelerating () {

		if (Input.GetAxis("Mouse X") != 0) {
			state = moving;
			moving ();
			return;
		}               
		speed *= deceleration;
		speed = Mathf.Clamp (speed, 0, speed);
		transform.Rotate(0, speed, 0);
		Debug.Log (speed);

		if (speed == 0) 
			state = moving;
	}
	
	void Update () {

		state();
	}
}