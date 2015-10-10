using UnityEngine;
using System.Collections;

/// <summary>
/// Class that automatically moves tiles 
/// unrelated to player interaction
/// 
/// In intervals a set of randomly selected 
/// tiles are moved back into their initial
/// position.
/// </summary>
public class AutomaticWallMovement : Singleton<AutomaticWallMovement> {
	
	void Start () {
	
		StartCoroutine (CountDown ());
	}

	/// <summary>
	/// Countdown until another set of tiles should
	/// be moved
	/// </summary>
	IEnumerator CountDown () {

		float timer = 0.7f;
		do {
			timer -= Time.deltaTime;
			yield return null;
		} while (timer > 0);
		StartCoroutine (CountDown ());
		MoveRandomTiles ();
	}
	
	void MoveRandomTiles () {

		int tilesToMove = 100;
		Tile randomTile = null;
		for (int tile = 0; tile < tilesToMove; tile ++) {
			randomTile = MoveRandomTile(false);
		}
		WallManager.instance.PlaySlamSound(randomTile.transform.position);
	}
	
	Tile MoveRandomTile (bool playSound) {

		int wallIndex = Random.Range(0, 6);
		int wallLength = WallManager.instance.walls[wallIndex].GetLength (0);
		int wallWidth = WallManager.instance.walls [wallIndex].GetLength (1);
		int indexX = Random.Range(0, wallLength);
		int indexY = Random.Range(0, wallWidth);
		Tile tile = WallManager.instance.walls [wallIndex][indexX, indexY];
		tile.Move (playSound);
		return tile;
	}
}
