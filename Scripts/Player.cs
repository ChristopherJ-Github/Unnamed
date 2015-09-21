using UnityEngine;
using System.Collections;

public class Player : Singleton<Player> {

	[HideInInspector] public int score;
	[HideInInspector] public Quaternion rotation;
	[HideInInspector] public Vector3 position;
	public CollisionDetection collisionDetection;

	void Start () {

		rotation = transform.rotation;
		position = transform.position;
	}
	
	void Update () {

		rotation = transform.rotation; 
		position = transform.position;
	}
}
