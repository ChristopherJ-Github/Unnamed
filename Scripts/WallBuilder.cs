using UnityEngine;
using System.Collections;
using System;

public class WallBuilder : Singleton<WallBuilder> {

	GameObject tile;
	float tileLen;
	int xAmt, yAmt, zAmt; //amount of tiles along each axis
	public delegate void newWallNotifier ();
	public event newWallNotifier OnNewWall;

	void notifyNewWall() {

		if (OnNewWall != null)
			OnNewWall();
	}

	void OnEnable () {

		InitVariables ();
		BuildsWalls ();
	}

	void InitVariables () {

		xAmt = WallManager.instance.xAmt;
		yAmt = WallManager.instance.yAmt;
		zAmt = WallManager.instance.zAmt;
		tile = WallManager.instance.tile;
		tileLen = WallManager.tileDimensions.x;
	}

	[HideInInspector] public Tile[][,] walls;
	[HideInInspector] public Tile[,] ceiling, floor;
	void BuildsWalls () {

		walls = createWalls ();
		ceiling = new Tile[xAmt,zAmt];
		floor = new Tile[xAmt, zAmt];
		CreateCeilingAndFloor (out ceiling, out floor);
	}

	Tile[][,] createWalls() {

		Tile[][,] walls = new Tile[4][,];

		Quaternion locRot = Quaternion.Euler (-90, 0, 0); //face forward then rotate
		Quaternion worldRot = Quaternion.Euler (0, 0, 0);
		walls[0] = CreateWall(worldRot, locRot, xAmt, yAmt, tileLen * zAmt/2f);
	
		locRot = Quaternion.Euler (-90, 90, 0);
		worldRot = Quaternion.Euler (0, 90, 0);
		walls[1] = CreateWall(worldRot, locRot, zAmt, yAmt, tileLen * xAmt/2f);

		locRot = Quaternion.Euler (-90, 180, 0); 
		worldRot = Quaternion.Euler (0, 0, 0);
		walls[2] = CreateWall(worldRot, locRot, xAmt, yAmt, -tileLen * zAmt/2f);

		locRot = Quaternion.Euler (-90, 270, 0); 
		worldRot = Quaternion.Euler (0, 90, 0);
		walls[3] = CreateWall(worldRot, locRot, zAmt, yAmt, -tileLen * xAmt/2f);

		return walls;
	}

	void CreateCeilingAndFloor(out Tile[,] ceiling, out Tile[,] floor) {

		Quaternion locRot = Quaternion.Euler (180, 0, 0);
		Quaternion worldRot = Quaternion.Euler (-90, 0, 0);
		ceiling = CreateWall(worldRot, locRot, xAmt, zAmt, yAmt * tileLen /2f);

		locRot = Quaternion.Euler (0, 0, 0);
		worldRot = Quaternion.Euler (-90, 0, 0);
		floor = CreateWall(worldRot, locRot, xAmt, zAmt, -yAmt * tileLen /2f);;
	}

	Tile[,] CreateWall (Quaternion worldRotation, Quaternion localRotation, int width, int height, float depthShift = 0, float widthShift = 0, float heightShift = 0) {//return wall array

		Tile[,] wall = new Tile[width, height];

		float initX = -tileLen / 2f * (width - 1);
		float initY = -tileLen / 2f * (height - 1);
		float x = initX + widthShift;
		float y = initY + heightShift;
		float z = depthShift;
		Vector3 position = new Vector3 (x, y, z);

		for (int h = 0; h < height; h++) {
			for (int w = 0; w < width; w++ ) {
				GameObject obj = GameObject.Instantiate (tile, worldRotation * position , localRotation) as GameObject;
				Tile tileInstance = obj.GetComponent<Tile>();
				wall[w,h] = tileInstance;
				position .x += tileLen;
			}
			position .x = initX;
			position .y += tileLen;
		}
		return wall;
	}
}
