using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class that contains information about the room such as
/// tile dimensions and array containing all tiles
/// </summary>
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

	public delegate void resetNotifier ();
	public event resetNotifier OnReset;
	/// <summary>
	/// Reset room to original state
	/// </summary>
	public void Reset() {

		if (OnReset != null)
			OnReset();
	}

	// Array of 6 2D arrays representing walls of tiles
	// indexs 4 and 5 represent the ceiling and floor respectively
	[HideInInspector] public Tile[][,] walls;
	public int xAmt, yAmt, zAmt; //amount of tiles along each axis
}
