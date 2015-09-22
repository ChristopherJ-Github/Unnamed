using UnityEngine;
using System.Collections;
using System;

public class WallManager : Singleton<WallManager>{

	void OnEnable () {

		InitTileLengths ();
	}

	public GameObject tile;
	public static Vector3 tileDimensions;
	void InitTileLengths () {

		GameObject tile = Instantiate (this.tile) as GameObject;
		tileDimensions = tile.collider.bounds.extents * 2;
		Destroy (tile);
	}
	
	public Vector2 RandomizeVector(int xSpeed, int ySpeed) {

		int sign;
		if (UnityEngine.Random.Range(0, 100) < 50)
			sign = -1;
		else 
			sign = 1;
		return new Vector2(xSpeed * sign, ySpeed * sign);
	}

	public delegate void resetNotifier ();
	public event resetNotifier OnReset;
	public void Reset() {

		if (OnReset != null)
			OnReset();
	}

	public int xAmt, yAmt, zAmt; //amount of tiles along each axis
}
