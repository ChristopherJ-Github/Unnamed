using UnityEngine;
using System.Collections;

public class ChargableProjectileDamage : Damager {

	public float minDamage, maxDamage;
	public float minScale, maxScale;
	
	public void init (float lerp, Color color) {

		damage = Mathf.Lerp (minDamage, maxDamage, lerp);
		transform.localScale = transform.localScale * Mathf.Lerp (minScale, maxScale, lerp);
		renderer.material.color = color;
	}
}
