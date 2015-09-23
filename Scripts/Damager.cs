using UnityEngine;
using System.Collections;

/// <summary>
/// Generic damager class that allows enemies
/// to gain damage from various types of damagers
/// </summary>
public class Damager : MonoBehaviour {

	public float damage;
	public string shooter;
	public float knockback;
	public GameObject particles;
}
