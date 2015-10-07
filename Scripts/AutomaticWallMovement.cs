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

		float timer = 5f;
		do {
			timer -= Time.deltaTime;
			yield return null;
		} while (timer > 0);
		StartCoroutine (CountDown ());
		MoveRandomTiles ();
	}
	
	void MoveRandomTiles () {

		for (int tile = 0; tile < 100; tile ++) {
			MoveRandomTile(false);
		}
	}
	
	void MoveRandomTile (bool playSound) {

		int wallIndex = Random.Range(0, 6);
		int indexX = Random.Range(0, WallManager.instance.xAmt);
		int indexY = Random.Range(0, WallManager.instance.yAmt);
		Tile tile = WallManager.instance.walls [wallIndex][indexX, indexY];
		tile.ResetPosition ();
	}
}
