using UnityEngine;
using System.Collections;
using System;

public class WallManager : Singleton<WallManager>{

	public GameObject tile;
	public static Vector3 tileDimensions;
	public GameObject[][,] pathAroundY;
	public GameObject[][,] pathAroundX;
	public GameObject[][,] pathAroundZ;
	public int xAmt, yAmt, zAmt; //amount of tiles along each axis
	public delegate void dimensionHandler(int x, int y, int z);
	public event dimensionHandler onNewDimensions;
	public delegate void changeColor (Color color, float trans, bool rand);
	public event changeColor OnNewColor;

	public Color normalColor;
	public Color activeColor;

	void OnEnable () {

		InitTileLengths ();
	}

	void InitTileLengths () {

		GameObject tile = Instantiate (this.tile) as GameObject;
		tileDimensions = tile.collider.bounds.extents * 2;
		Destroy (tile);
	}

	public void setupPaths(GameObject[][,] walls, GameObject[,] ceiling, GameObject[,] floor) {

		pathAroundX = setupPath (walls, pathAroundX, xAmt, yAmt, xAmt, zAmt);
		pathAroundY = setupPath (walls, pathAroundY, xAmt, yAmt, zAmt, yAmt); 
		pathAroundZ = setupPath (walls, pathAroundZ, xAmt, zAmt, zAmt, yAmt);

		Array.Copy (ceiling, pathAroundX [1], xAmt * zAmt); 
		Array.Copy (floor, pathAroundX [3], xAmt * zAmt);
		
		Array.Copy (ceiling, pathAroundZ [0], xAmt * zAmt);
		Array.Copy (floor, pathAroundZ [2], xAmt * zAmt);
	}

	GameObject[][,] setupPath(GameObject[][,] walls, GameObject[][,] path, int width1, int height1, int width2, int height2) {
		
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
	void Update () {
	
	}

	public Vector2 randomizeVector(int xSpeed, int ySpeed) {

		int sign;
		if (UnityEngine.Random.Range(0, 100) < 50)
			sign = -1;
		else 
			sign = 1;
		return new Vector2(xSpeed * sign, ySpeed * sign);
	}

	public void updateDimensions (int x, int y, int z) {
		
		xAmt = x;
		yAmt = y;
		zAmt = z;
		if (onNewDimensions != null)
			onNewDimensions(x,y,z);
	}

	public void triggerNewColor(Color color, float trans, bool rand) {
		
		if (OnNewColor != null)
			OnNewColor(color, trans, rand);
	}

}
