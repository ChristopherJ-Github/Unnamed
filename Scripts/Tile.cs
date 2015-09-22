using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	void Start () {

		originalPosition = transform.position;
		WallManager.instance.OnReset += ResetPosition;
	}
	
	void OnCollisionEnter (Collision collision) {

		MoveNearBlocks ();
	}

	public int wallIndex;
	public Vector2 wallPosition;
	void MoveNearBlocks () {

		int spread = 2;
		int wallPosX = (int)wallPosition.x;
		int wallPosY = (int)wallPosition.y;
		for (int x = wallPosX - spread; x <= wallPosX + spread; x++) {
			if (x < 0 || x >= WallBuilder.instance.walls[wallIndex].GetLength(0))
				continue;
			for (int y = wallPosY + spread; y >= wallPosY - spread; y--) {
				if (y < 0 || y >= WallBuilder.instance.walls[wallIndex].GetLength(1))
					continue;
				WallBuilder.instance.walls[wallIndex][x,y].Move();
			}
		}
	}

	public void Move () {

		StopAllCoroutines ();
		float distance = Random.Range (-3f, 1f);
		StartCoroutine (Move (distance));
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

		if (totalDistance > WallManager.tileDimensions.y/2f) {
			transform.position = WallManager.tileDimensions.y/2f * transform.up + originalPosition;
			totalDistance = WallManager.tileDimensions.y/2f;
			return true;
		} else if (totalDistance < -WallManager.tileDimensions.y/2f){
			transform.position = -WallManager.tileDimensions.y/2f * transform.up + originalPosition;
			totalDistance = -WallManager.tileDimensions.y/2f;
			return true;
		} else {
			transform.position = currentDistance * transform.up + initPosition;
			return false;
		}
	}

	void ResetPosition () {

		StopAllCoroutines ();
		StartCoroutine (Move (-totalDistance));
	}
}
