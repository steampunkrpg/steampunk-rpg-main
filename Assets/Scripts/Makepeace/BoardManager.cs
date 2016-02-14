﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	// Variable that "stores" hexagon
	public HexTile HexTile;

	// Set grid width / height
	private int gridWidth = 10;
	private int gridHeight = 10;

	// hexagon tile width / height
	private float hexWidth;
	private float hexHeight;

	// set height and width for tiles
	void setSizes()	
	{
		hexWidth = HexTile.GetComponent<Renderer>().bounds.size.x;
		hexHeight = HexTile.GetComponent<Renderer>().bounds.size.z;
	}

	// Center hex grid (put first hexagon at (0, 0, 0))
	Vector3 calcStartPos()
	{
		Vector3 startPos;
		//start in top left corner
		startPos = new Vector3(-hexWidth * gridWidth / 2f + hexWidth / 2, 0,
			gridHeight / 2f * hexHeight - hexHeight / 2);
		return startPos;
	}

	// Change hex grid coordinates to game coordinates
	public Vector3 worldCoordinate(Vector2 gridPos)
	{
		// Position of first hexagon
		Vector3 startPos = calcStartPos();
		//Every other row offset by half width of tile
		float offset = 0;
		if (gridPos.y % 2 != 0)
		{
			offset = hexWidth / 2;
		}
		float x = startPos.x + offset + gridPos.x * hexWidth;
		// Every new line is offset in z by .75 of hexagon height
		float z = startPos.z - gridPos.y * hexHeight * .75f;
		return new Vector3(x, 0, z);
	}

	// create all the hexagon tiles
	void createGrid()
	{
		// Game object that acts as "parent" to all hexagon tiles
		GameObject hexGridParent = new GameObject("HexGrid");
		int i = 1;
		// create da grid
		for (float x = 0; x < gridHeight; x++)
		{
			for (float y = 0; y < gridWidth; y++)
			{
				// clone hexagon              
				HexTile hextile = Instantiate(HexTile);
				// position in grid
				Vector2 gridPos = new Vector2(x, y);
				hextile.transform.position = worldCoordinate(gridPos);
				hextile.transform.parent = hexGridParent.transform;
				hextile.transform.name = "HexTile [" + y + "," + x + "]";
				hextile.GetComponent<HexTile> ().pos = new float[] { y, x };
				i++;
			}
		}
	}
		
	public void SetupScene () {
		setSizes();
		createGrid();
	}
}
