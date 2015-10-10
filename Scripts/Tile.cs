using UnityEngine;
using System.Collections;

/// <summary>
/// Class that controls the specific behaviour of
/// each tile in the walls.
/// 
/// When a tile is hit it triggers surrounding tiles
/// in a square area to move randomly either inwards
/// or outwards.
/// </summary>
public class Tile : MonoBehaviour {
	
	void Start () {

		originalPosition = transform.position;
		WallManager.instance.OnReset += ResetPosition;
	}
	
	void OnCollisionEnter (Collision collision) {

		if (collision.gameObject.tag == "Projectile") {
			MoveNearBlocks ();
		}
	}

	public int wallIndex;
	public Vector2 wallPosition;
	/// <summary>
	/// Move all tiles in a square area around this one
	/// </summary>
	void MoveNearBlocks () {

		int spread = 2;
		int wallPosX = (int)wallPosition.x;
		int wallPosY = (int)wallPosition.y;
		for (int x = wallPosX - spread; x <= wallPosX + spread; x++) {
			if (x < 0 || x >= WallManager.instance.walls[wallIndex].GetLength(0))
				continue;
			for (int y = wallPosY + spread; y >= wallPosY - spread; y--) {
				if (y < 0 || y >= WallManager.instance.walls[wallIndex].GetLength(1))
					continue;
				Tile tile = WallManager.instance.walls[wallIndex][x,y];
				if (tile == this) 
					Move(true);
				else
					tile.Move(false);
			}
		}
	}

	/// <summary>
	/// Move tile randomly up or down
	/// </summary>
	public void Move (bool playSound, float? distance = null) {

		StopAllCoroutines ();
		float _distance = distance ?? Random.Range (-3f, 1f);
		StartCoroutine (Move (_distance));
		if (playSound)
			WallManager.instance.PlaySlamSound (transform.position);
	}
	
	public float moveTime; 
	public float totalDistance; 
	public Vector3 originalPosition; 
	/// <summary>
	/// Move tile based on distance
	/// </summary>
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

	/// <summary>
	/// Set position of tile based on distance from intial position
	/// and returns true if it can't move any further
	/// </summary>
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

		ResetPosition (false);
	}

	/// <summary>
	/// Move position back to default one
	/// </summary>
	public void ResetPosition (bool playSound) {

		StopAllCoroutines ();
		Move (playSound, -totalDistance);
	}
}
