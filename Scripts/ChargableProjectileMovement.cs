using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class in charge of moving charge projectile
/// based on charge amount.
/// 
/// Directly after initiating a projectile with 
/// this script Init should be called to set the 
/// speed. After that it'll move and destroy
/// itself if it gets to far or hits on
/// object
/// </summary>
public class ChargableProjectileMovement : MonoBehaviour {

	void Start () {

		direction = transform.forward;
		initPos = transform.position;
	}

	public float minSpeed, maxSpeed;
	private float speed;
	public void Init (float chargeAmount) {

		speed = Mathf.Lerp (minSpeed, maxSpeed, chargeAmount);
	}

	void Update () {

		Move ();
	}

	private Vector3 direction;
	private Vector3 initPos;
	public float maxDistance;
	void Move () {

		transform.Translate (direction * speed * Time.deltaTime, Space.World);
		float distance = Vector3.Distance (initPos, transform.position);
		if (distance >= maxDistance) {
			GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision collision) {
		
		Destroy(gameObject);
	}
}
