using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class in charge of moving charge projectile
/// based on charge amount.
/// </summary>
public class ChargableProjectileMovement : MonoBehaviour {

	void Start () {

		direction = transform.forward;
		initPos = transform.position;
	}

	public float minSpeed, maxSpeed;
	private float speed;
	public void Init (float lerp) {

		speed = Mathf.Lerp (minSpeed, maxSpeed, lerp);
	}

	private Vector3 direction;
	private Vector3 initPos;
	public float maxDistance;
	void Update () {
		
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
