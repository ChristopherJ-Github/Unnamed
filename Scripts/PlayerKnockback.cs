using UnityEngine;
using System.Collections;

/// <summary>
/// Class that allows enemies and weapon recoil
/// to knock back the player
/// </summary>
public class PlayerKnockback : MonoBehaviour {
	
	[Tooltip("Divded with the magnitude of the knockback force to get the length")]
	public float timeDivider;
	public void Knockback (Vector3 velocity, float magnitude) {

		float time = magnitude / timeDivider;
		StartCoroutine(KnockBackRoutine(velocity, time));
	}

	IEnumerator KnockBackRoutine (Vector3 velocity, float time) {
		
		while (time > 0) {
			rigidbody.AddForce (velocity);
			time -= Time.deltaTime;
			yield return null;
		}
	}
}
