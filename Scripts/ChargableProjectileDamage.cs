using UnityEngine;
using System.Collections;

/// <summary>
/// Class in charge of damaging enemies based 
/// on charge amount from ChargeWeapon.
/// Currently not in use
/// </summary>
public class ChargableProjectileDamage : Damager {

	public float minDamage, maxDamage;
	public float minScale, maxScale;
	public void Init (float chargeAmount, Color color) {

		damage = Mathf.Lerp (minDamage, maxDamage, chargeAmount);
		transform.localScale = transform.localScale * Mathf.Lerp (minScale, maxScale, chargeAmount);
		renderer.material.color = color;
	}
}
