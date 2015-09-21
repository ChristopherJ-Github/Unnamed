using UnityEngine;
using System.Collections;

public class PlayerKnockback : MonoBehaviour {
	
	[Tooltip("Divded with the magnitude of the knockback force to get the length")]
	public float timeDivider;

	public void knockBack (Vector3 velocity, float magnitude) {

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
