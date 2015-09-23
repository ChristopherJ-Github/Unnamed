using UnityEngine;
using System.Collections;

/// <summary>
/// Generic damager class that allows enemies
/// to gain damage from various types of damagers
/// </summary>
public class Damager : MonoBehaviour {
	
	public virtual Vector3 GetKnockback (out float magnitude) {

		magnitude = 0;
		return new Vector3 (0,0,0); 
	}

	public float damage;
	public string shooter;
	public float knockback;
	public GameObject particles;
}
