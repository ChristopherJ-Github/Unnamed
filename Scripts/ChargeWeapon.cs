using UnityEngine;
using System.Collections;

/// <summary>
/// Weapon that can be charged by holding 
/// down the fire button
/// 
/// Charging functionality is currently not
/// used.
/// </summary>
public class ChargeWeapon : MonoBehaviour {

	void Update () {

		GetInput ();
	}

	public float chargeSpeed;
	[HideInInspector] public float chargeAmount;
	/// <summary>
	/// Charge based on how long the fire
	/// button is being held
	/// </summary>
	void GetInput () {

		if (Input.GetMouseButton(0)) {
			chargeAmount = Mathf.Clamp(chargeAmount + (chargeSpeed * Time.deltaTime), 0f, 1f);
			if (chargeAmount == 1)
				Shoot();
		} else { 
			if (chargeAmount > 0)
				Shoot();
		}
	}

	public Color lowCol, highCol;
	public AudioSource audioSource;
	public AudioClip fireSound;
	public Transform forward;
	public GameObject projectile;
	void Shoot () {

		GameObject projectileInst = (GameObject)Instantiate (projectile, forward.position, forward.rotation);
		ChargableProjectileDamage projectileDamage = projectileInst.GetComponent<ChargableProjectileDamage> ();
		ChargableProjectileMovement projectileMovement = projectileInst.GetComponent<ChargableProjectileMovement> ();
		Color color = Color.Lerp (lowCol, highCol, chargeAmount);	
		projectileDamage.Init (chargeAmount, color);
		projectileMovement.Init (chargeAmount);
		Recoil ();
		audio.PlayOneShot (fireSound, 1);
		chargeAmount = 0;
	}

	/// <summary>
	/// Knockback player based on charge amount
	/// </summary>
	void Recoil () {

		Vector3 knockBackDir = -Player.instance.transform.forward;
		knockBackDir.y = 0;
		float knockback = Mathf.Lerp (0, 120, chargeAmount);
		Player.instance.knockback.Knockback (knockBackDir * knockback, knockback);
	}
}
