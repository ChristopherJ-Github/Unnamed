using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChargableProjectileMovement : MonoBehaviour {

	public float minSpeed, maxSpeed;
	float speed;
	Vector3 movement;
	Vector3 direction;
	public float maxDistance;
	Vector3 initPos;
	public GameObject particles;

	void Start () {

		direction = transform.forward;
		initPos = transform.position;
	}

	public void init (float lerp) {

		speed = Mathf.Lerp (minSpeed, maxSpeed, lerp);
	}
	
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
