using UnityEngine;
using System.Collections;

public class TestHexTile : MonoBehaviour {

	public TestHexTile NE_Tile;
	public TestHexTile NW_Tile;
	public TestHexTile E_Tile;
	public TestHexTile W_Tile;
	public TestHexTile SE_Tile;
	public TestHexTile SW_Tile;
	public float[] pos;
	public float mov_cost = 1;

	public GameObject character;

	public int SpawnP;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void FindNeighbors() {
		float x = pos [1];
		float y = pos [0];
		string tileName;

		if ((pos [0] % 2) == 0) {
			y--;
			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [0] != 0 && pos [1] != 0) {
				NW_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}
			
			x++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [0] != 0) {
				NE_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}
			
			y++;
			x++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9) {
				E_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}

			y++;
			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			SE_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();

			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 0) {
				SW_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}

			y--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 0) {
				W_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}
			
			x++;
		} else {
			y--;
			tileName = "HexTile [" + y + "," + x + "]";
			NW_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();

			x++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9) {
				NE_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}

			y++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9) {
				E_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}

			y++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9 && pos[0] != 9) {
				SE_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}

			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [0] != 9) {
				SW_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}

			y--;
			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 0) {
				W_Tile = GameObject.Find (tileName).GetComponent<TestHexTile> ();
			}

			x++;
		}
	}
}
