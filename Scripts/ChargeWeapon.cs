using UnityEngine;
using System.Collections;

public class ChargeWeapon : MonoBehaviour {
	
	public float chargeSpeed;
	[HideInInspector] public float charge;
	public Color lowCol, highCol;
	float delay = 0.5f;
	float delayCounter = 0;
	public AudioSource audioSource;
	public AudioClip fireSound;
	public Transform forward;
	public GameObject projectile;
	
	void Start () {

	}

	void Update () {

		GetInput ();
	}
	
	void GetInput () {
		
		delayCounter -= Time.deltaTime;

		if (Input.GetMouseButton(0)) {

			charge = Mathf.Clamp(charge + (chargeSpeed * Time.deltaTime), 0f, 1f);
			if (charge == 1)
				Shoot();
			
		} else { 
			if (charge > 0)
				Shoot();
		}
		//Debug.Log (charge);
	}
	
	void Shoot () {

		Color color = Color.Lerp (lowCol, highCol, charge);	
		//WeaponManager.instance.ActivateShootCol (color);
		GameObject projectileInst = (GameObject)Instantiate (projectile, forward.position, forward.rotation);
		ChargableProjectileDamage projectileDamage = projectileInst.GetComponent<ChargableProjectileDamage> ();
		ChargableProjectileMovement projectileMovement = projectileInst.GetComponent<ChargableProjectileMovement> ();
		projectileDamage.init (charge, color);
		projectileMovement.init (charge);

		audio.PlayOneShot (fireSound, 1);
		Vector3 knockBackDir = -Player.instance.transform.forward;
		knockBackDir.y = 0;
		float knockback = Mathf.Lerp (0, 120, charge);
		Player.instance.collisionDetection.knockback.knockBack (knockBackDir * knockback, knockback);
		delayCounter = delay;
		charge = 0;
	}
}
