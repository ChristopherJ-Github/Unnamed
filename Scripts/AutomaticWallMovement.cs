using UnityEngine;
using System.Collections;

public class AutomaticWallMovement : Singleton<AutomaticWallMovement> {
	
	void Start () {
	
		StartCoroutine (CountDown ());
	}


	IEnumerator CountDown () {

		float timer = 0.3f;
		while (timer > 0) {
			timer -= Time.deltaTime;
			yield return null;
		}
		StartCoroutine (CountDown ());
		MoveRandomTile ();
	}

	void MoveRandomTile () {

		int wallIndex = Random.Range(0, 6);
		int indexX = Random.Range(0, WallManager.instance.xAmt);
		int indexY = Random.Range(0, WallManager.instance.yAmt);
		Tile tile = WallManager.instance.walls [wallIndex][indexX, indexY];
		tile.Move ();
	}

}
