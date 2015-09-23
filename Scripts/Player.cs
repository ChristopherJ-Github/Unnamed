using UnityEngine;
using System.Collections;

/// <summary>
/// Class that contains information about the player
/// </summary>
public class Player : Singleton<Player> {

	[HideInInspector] public Quaternion rotation;
	[HideInInspector] public Vector3 position;
	[HideInInspector] public PlayerKnockback knockback;
	void Start () {

		rotation = transform.rotation;
		position = transform.position;
		knockback = gameObject.GetComponent<PlayerKnockback> ();
	}
	
	void Update () {

		rotation = transform.rotation; 
		position = transform.position;
	}
}
