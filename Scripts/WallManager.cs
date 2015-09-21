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

	public GameObject[][,] pathAroundY;
	public GameObject[][,] pathAroundX;
	public GameObject[][,] pathAroundZ;
	public int xAmt, yAmt, zAmt; //amount of tiles along each axis
	public void SetupPaths(GameObject[][,] walls, GameObject[,] ceiling, GameObject[,] floor) {

		pathAroundX = SetupPath (walls, pathAroundX, xAmt, yAmt, xAmt, zAmt);
		pathAroundY = SetupPath (walls, pathAroundY, xAmt, yAmt, zAmt, yAmt); 
		pathAroundZ = SetupPath (walls, pathAroundZ, xAmt, zAmt, zAmt, yAmt);

		Array.Copy (ceiling, pathAroundX [1], xAmt * zAmt); 
		Array.Copy (floor, pathAroundX [3], xAmt * zAmt);
		
		Array.Copy (ceiling, pathAroundZ [0], xAmt * zAmt);
		Array.Copy (floor, pathAroundZ [2], xAmt * zAmt);
	}

	GameObject[][,] SetupPath(GameObject[][,] walls, GameObject[][,] path, int width1, int height1, int width2, int height2) {
		
		path = new GameObject[4][,];
		for (int wall = 0; wall < 4; wall++) {

			int width = width1;
			width1 = width2;
			width2 = width;
			int height = height1;
			height1 = height2;
			height2 = height;

			path[wall] = new GameObject[width,height];
			int maxDim = walls[wall].GetLength(0) * walls[wall].GetLength(1);
			Array.Copy (walls[wall], path[wall],Mathf.Clamp(width * height, 0, maxDim));
		}
		return path;
	}

	public Vector2 RandomizeVector(int xSpeed, int ySpeed) {

		int sign;
		if (UnityEngine.Random.Range(0, 100) < 50)
			sign = -1;
		else 
			sign = 1;
		return new Vector2(xSpeed * sign, ySpeed * sign);
	}

}
