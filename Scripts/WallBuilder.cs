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

		WallManager.instance.onNewDimensions += updateDim;
		setupAndBuild ();
	}

	void setupAndBuild () {

		xAmt = WallManager.instance.xAmt;
		yAmt = WallManager.instance.yAmt;
		zAmt = WallManager.instance.zAmt;
		tile = WallManager.instance.tile;
		tileLen = WallManager.tileDimensions.x;
		
		GameObject[][,] walls = createWalls ();
		GameObject[,] ceiling = new GameObject[xAmt,zAmt];
		GameObject[,] floor = new GameObject[xAmt, zAmt];
		createCeilingAndFloor (out ceiling, out floor);
		WallManager.instance.setupPaths (walls, ceiling, floor);
	}

	GameObject[][,] createWalls() {

		GameObject[][,] walls = new GameObject[4][,];

		Quaternion locRot = Quaternion.Euler (-90, 0, 0); //face forward then rotate
		Quaternion worldRot = Quaternion.Euler (0, 0, 0);
		walls[0] = createWall(worldRot, locRot, xAmt, yAmt, tileLen * zAmt/2f);
	
		locRot = Quaternion.Euler (-90, 90, 0);
		worldRot = Quaternion.Euler (0, 90, 0);
		walls[1] = createWall(worldRot, locRot, zAmt, yAmt, tileLen * xAmt/2f);

		locRot = Quaternion.Euler (-90, 180, 0); 
		worldRot = Quaternion.Euler (0, 0, 0);
		walls[2] = createWall(worldRot, locRot, xAmt, yAmt, -tileLen * zAmt/2f);

		locRot = Quaternion.Euler (-90, 270, 0); 
		worldRot = Quaternion.Euler (0, 90, 0);
		walls[3] = createWall(worldRot, locRot, zAmt, yAmt, -tileLen * xAmt/2f);

		return walls;
	}

	void createCeilingAndFloor(out GameObject[,] ceiling, out GameObject[,] floor) {

		Quaternion locRot = Quaternion.Euler (180, 0, 0);
		Quaternion worldRot = Quaternion.Euler (-90, 0, 0);
		ceiling = createWall(worldRot, locRot, xAmt, zAmt, yAmt * tileLen /2f);

		locRot = Quaternion.Euler (0, 0, 0);
		worldRot = Quaternion.Euler (-90, 0, 0);
		floor = createWall(worldRot, locRot, xAmt, zAmt, -yAmt * tileLen /2f);;
	}

	GameObject[,] createWall (Quaternion worldRotation, Quaternion localRotation, int width, int height, float depthShift = 0, float widthShift = 0, float heightShift = 0) {//return wall array

		GameObject[,] wall = new GameObject[width, height];

		float initX = -tileLen / 2f * (width - 1);
		float initY = -tileLen / 2f * (height - 1);
		float x = initX + widthShift;
		float y = initY + heightShift;
		float z = depthShift;
		Vector3 position = new Vector3 (x, y, z);

		for (int h = 0; h < height; h++) {
			for (int w = 0; w < width; w++ ) {
				GameObject obj = GameObject.Instantiate (tile, worldRotation * position , localRotation) as GameObject;
				wall[w,h] = obj;
				obj.renderer.material.color = WallManager.instance.normalColor;
				position .x += tileLen;
			}
			position .x = initX;
			position .y += tileLen;
		}
		return wall;
	}

	void deleteWalls() {

		foreach (GameObject[,] wall in WallManager.instance.pathAroundY)
			foreach (GameObject tile in wall)
				GameObject.Destroy(tile);
		foreach (GameObject tile in WallManager.instance.pathAroundX[1]) //ceiling
			GameObject.Destroy(tile);
		foreach (GameObject tile in WallManager.instance.pathAroundX[3]) //floor
			GameObject.Destroy(tile);
	}

	public void rebuildWalls () {

		deleteWalls ();
		setupAndBuild ();
		notifyNewWall ();
	}

	void updateDim(int x, int y, int z) {
		
		xAmt = x;
		yAmt = y;
		zAmt = z;
	}
}
