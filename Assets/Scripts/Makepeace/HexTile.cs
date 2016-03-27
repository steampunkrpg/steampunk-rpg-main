using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour {

	public HexTile NE_Tile;
	public HexTile NW_Tile;
	public HexTile E_Tile;
	public HexTile W_Tile;
	public HexTile SE_Tile;
	public HexTile SW_Tile;
	public float[] pos;
	public float mov_cost = 1;
	public float mov_dis = -1;
	public float att_dis = -1;
	public float terrainBonus = .1F;

	public GameObject character;

	public HexTile parent;

	public int SpawnP;

    public HexTile[] neighbors = new HexTile[6];

    void Start () {

	}
	
	void Update () {
	
	}

    public HexTile GetNeighbor(int tile)
    {
        // NE NW W SW SE E
        return neighbors[tile];
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
				NW_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}
			
			x++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [0] != 0) {
				NE_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}
			
			y++;
			x++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9) {
				E_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}

			y++;
			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			SE_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();

			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 0) {
				SW_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}

			y--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 0) {
				W_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}
			
			x++;
		} else {
			y--;
			tileName = "HexTile [" + y + "," + x + "]";
			NW_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();

			x++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9) {
				NE_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}

			y++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9) {
				E_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}

			y++;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 9 && pos[0] != 9) {
				SE_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}

			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [0] != 9) {
				SW_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}

			y--;
			x--;
			tileName = "HexTile [" + y + "," + x + "]";
			if (pos [1] != 0) {
				W_Tile = GameObject.Find (tileName).GetComponent<HexTile> ();
			}

			x++;
		}

        neighbors[0] = NE_Tile;
        neighbors[1] = NW_Tile;
        neighbors[2] = W_Tile;
        neighbors[3] = SW_Tile;
        neighbors[4] = SE_Tile;
        neighbors[5] = E_Tile;
    }
}
