using UnityEngine;
using System.Collections;

/// <summary>
/// Weapon that can be charged by holding 
/// down the fire button
/// </summary>
public class ChargeWeapon : MonoBehaviour {

	void Update () {

		GetInput ();
	}

	public float chargeSpeed;
	[HideInInspector] public float charge;
	/// <summary>
	/// Charge based on how long the fire
	/// button is being held
	/// </summary>
	void GetInput () {

		if (Input.GetMouseButton(0)) {
			charge = Mathf.Clamp(charge + (chargeSpeed * Time.deltaTime), 0f, 1f);
			if (charge == 1)
				Shoot();
		} else { 
			if (charge > 0)
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
		Color color = Color.Lerp (lowCol, highCol, charge);	
		projectileDamage.Init (charge, color);
		projectileMovement.Init (charge);
		Recoil ();
		audio.PlayOneShot (fireSound, 1);
		charge = 0;
	}

	/// <summary>
	/// Knockback player based on charge amount
	/// </summary>
	void Recoil () {

		Vector3 knockBackDir = -Player.instance.transform.forward;
		knockBackDir.y = 0;
		float knockback = Mathf.Lerp (0, 120, charge);
		Player.instance.knockback.knockBack (knockBackDir * knockback, knockback);
	}
}
