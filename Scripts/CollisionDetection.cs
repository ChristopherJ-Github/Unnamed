using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDetection : MonoBehaviour {

	public float health = 100f;
	public float coolDown;
	private bool invincible; 
	public List<int> threatLayers;
	[HideInInspector] 
	public float fullHealth;
	private bool notified;
	[HideInInspector]
	public PlayerKnockback knockback;

	public delegate void deathNotifier ();
	public event deathNotifier OnDeath;
	public event deathNotifier OnHit;

	public bool debug;

	void Start () {
	
		fullHealth = health;
		knockback = gameObject.GetComponent<PlayerKnockback> ();
		invincible = false;
		foreach (int threatLayer in threatLayers)
			Physics.IgnoreLayerCollision (gameObject.layer, threatLayer, false);
	}

	void OnTriggerEnter (Collider _collider) {
		
		Damager damager = _collider.gameObject.GetComponent<Damager> ();
		if (damager != null) {
			
			if (damager.shooter == gameObject.tag)
				return;

			float magnitude;
			Vector3 _knockback = damager.GetKnockback(out magnitude);
			updateHealth(-damager.damage, _knockback, magnitude);

			if (_collider.tag == "Projectile")
				Destroy(_collider.gameObject);
		}
	}

	void OnCollisionEnter (Collision collision) {
		
		Damager damager = collision.gameObject.GetComponent<Damager> ();

		if (damager != null) {
			
			if (damager.shooter == gameObject.tag)
				return;

			float magnitude;
			Vector3 _knockback = damager.GetKnockback(out magnitude);
			updateHealth(-damager.damage, _knockback, magnitude);

			if (collision.gameObject.tag == "Projectile")
				Destroy(collision.gameObject);
		}
	}

	public void updateHealth (float toAdd, Vector3 knockbackVelocity, float knockbackMagnitude) {

		if (invincible)
			return;

		health += toAdd;
		health = Mathf.Clamp (health, 0, fullHealth);

		if (debug)
			Debug.Log(health);
		notifyHit ();
		if (audio.clip != null)
			audio.Play ();

		if (health <= 0 && !notified) {
			notified = true;
			notifyDeath();
		}

		if (knockback != null)
			knockback.knockBack(knockbackVelocity, knockbackMagnitude);

		StartCoroutine (InvincibilityMode ());
	}

	IEnumerator InvincibilityMode () {

		foreach (int threatLayer in threatLayers)
			Physics.IgnoreLayerCollision (gameObject.layer, threatLayer, true);
		invincible = true;
		yield return new WaitForSeconds (coolDown);

		foreach (int threatLayer in threatLayers)
			Physics.IgnoreLayerCollision (gameObject.layer, threatLayer, false);
		invincible = false;

	}

	public void Heal (float toAdd) {

		health += toAdd;
		health = Mathf.Clamp (health, 0, fullHealth);
	}


	public void notifyDeath() {
		
		if (OnDeath != null) {
			OnDeath();
		}
	}

	public void notifyHit() {
		
		if (OnHit != null) {
			OnHit();
		}
	}

}
