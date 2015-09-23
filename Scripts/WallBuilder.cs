using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class that builds the walls and holds the array
/// that contains all of the tiles
/// </summary>
public class WallBuilder : Singleton<WallBuilder> {

	void OnEnable () {

		InitVariables ();
		BuildWalls ();
	}

	void InitVariables () {

		xAmt = WallManager.instance.xAmt;
		yAmt = WallManager.instance.yAmt;
		zAmt = WallManager.instance.zAmt;
		tileLen = WallManager.tileDimensions.x;
	}
	
	// Array of 6 2D arrays representing walls of tiles
	// indexs 4 and 5 represent the ceiling and floor respectively
	[HideInInspector] public Tile[][,] walls;
	/// <summary>
	/// Builds the walls and tiles and fills the walls array
	/// </summary>
	void BuildWalls () {

		walls = CreateWalls ();
		CreateCeilingAndFloor (out walls[4], out walls[5]);
	}

	private float tileLen;
	private int xAmt, yAmt, zAmt;
	Tile[][,] CreateWalls() {

		Tile[][,] walls = new Tile[6][,];
		Quaternion locRot = Quaternion.Euler (-90, 0, 0); //face forward then rotate
		Quaternion worldRot = Quaternion.Euler (0, 0, 0);
		walls[0] = CreateWall(worldRot, locRot, xAmt, yAmt, tileLen * zAmt/2f, 0);
		locRot = Quaternion.Euler (-90, 90, 0);
		worldRot = Quaternion.Euler (0, 90, 0);
		walls[1] = CreateWall(worldRot, locRot, zAmt, yAmt, tileLen * xAmt/2f, 1);
		locRot = Quaternion.Euler (-90, 180, 0); 
		worldRot = Quaternion.Euler (0, 0, 0);
		walls[2] = CreateWall(worldRot, locRot, xAmt, yAmt, -tileLen * zAmt/2f, 2);
		locRot = Quaternion.Euler (-90, 270, 0); 
		worldRot = Quaternion.Euler (0, 90, 0);
		walls[3] = CreateWall(worldRot, locRot, zAmt, yAmt, -tileLen * xAmt/2f, 3);
		return walls;
	}

	void CreateCeilingAndFloor(out Tile[,] ceiling, out Tile[,] floor) {

		Quaternion locRot = Quaternion.Euler (180, 0, 0);
		Quaternion worldRot = Quaternion.Euler (-90, 0, 0);
		ceiling = CreateWall(worldRot, locRot, xAmt, zAmt, yAmt * tileLen /2f, 4);
		locRot = Quaternion.Euler (0, 0, 0);
		worldRot = Quaternion.Euler (-90, 0, 0);
		floor = CreateWall(worldRot, locRot, xAmt, zAmt, -yAmt * tileLen /2f, 5);
	}

	/// <summary>
	/// Creates a wall of tiles.
	/// </summary>
	/// <returns>A 2D array of tiles.</returns>
	/// <param name="worldRotation">Rotation of the wall.</param>
	/// <param name="localRotation">Local rotation of each tile.</param>
	/// <param name="width">Width of the wall.</param>
	/// <param name="height">Height of the wall.</param>
	/// <param name="depthShift">Depth shift.</param>
	/// <param name="wallIndex">Wall index in walls array.</param>
	Tile[,] CreateWall (Quaternion worldRotation, Quaternion localRotation, int width, int height, float depthShift, int wallIndex) {//return wall array

		Tile[,] wall = new Tile[width, height];
		float initX = -tileLen / 2f * (width - 1);
		float initY = -tileLen / 2f * (height - 1);
		float x = initX;
		float y = initY;
		float z = depthShift;
		Vector3 position = new Vector3 (x, y, z);
		for (int h = 0; h < height; h++) {
			for (int w = 0; w < width; w++ ) {
				GameObject obj = GameObject.Instantiate (WallManager.instance.tile, worldRotation * position , localRotation) as GameObject;
				Tile tileInstance = obj.GetComponent<Tile>();
				tileInstance.wallIndex = wallIndex;
				tileInstance.wallPosition.x = w;
				tileInstance.wallPosition.y = h;
				wall[w,h] = tileInstance;
				position .x += tileLen;
			}
			position .x = initX;
			position .y += tileLen;
		}
		return wall;
	}
}
