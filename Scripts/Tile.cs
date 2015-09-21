using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	void Start () {

		originalPosition = transform.position;
	}
	
	void OnCollisionEnter (Collision collision) {

		MoveNearBlocks ();
	}

	void MoveNearBlocks () {

	}

	public void Move () {

		StopAllCoroutines ();
		StartCoroutine (Move (2));
	}
	
	public float moveTime;
	public float totalDistance;
	public Vector3 originalPosition;
	IEnumerator Move (float distance) {

		Vector3 initPosition = transform.position;
		float currentDistance = 0;
		float timer = 0;
		while (timer < moveTime) {
			timer += Time.deltaTime;
			float distanceNorm = Mathf.InverseLerp(0, moveTime, timer);
			totalDistance -= currentDistance;
			currentDistance = distanceNorm * distance;
			totalDistance += currentDistance;
			bool boundaryHit = SetPosition (initPosition, currentDistance);
			if (boundaryHit)
				break;
			yield return null;
		}
	}

	bool SetPosition (Vector3 initPosition, float currentDistance) {

		if (totalDistance > WallManager.tileDimensions.y) {
			transform.position = -WallManager.tileDimensions.y * transform.up + originalPosition;
			totalDistance = WallManager.tileDimensions.y;
			return true;
		} else {
			transform.position = -currentDistance * transform.up + initPosition;
			return false;
		}
	}
}
