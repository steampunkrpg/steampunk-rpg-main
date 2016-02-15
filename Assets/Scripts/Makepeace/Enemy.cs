using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public HexTile tile;

	public bool moving = false;
	public bool Active = false;

	void Start() {
		tile = null;
	}

	public void InitPosition () {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
	}

	public void MoveEnemy() {
		int dir;
		moving = true;
		Active = false;
		tile.character = null;

		if (tile.pos[1] % 2 == 1) {
			dir = 6;
		} else {
			dir = 3;
		}

		switch (dir) {
		case 1:
			tile.NW_Tile.character = this.gameObject;
			tile = tile.NW_Tile;
			break;
		case 2:
			tile.NE_Tile.character = this.gameObject;
			tile = tile.NE_Tile;
			break;
		case 3:
			tile.E_Tile.character = this.gameObject;
			tile = tile.E_Tile;
			break;
		case 4:
			tile.SE_Tile.character = this.gameObject;
			tile = tile.SE_Tile;
			break;
		case 5:
			tile.SW_Tile.character = this.gameObject;
			tile = tile.SW_Tile;
			break;
		case 6:
			tile.W_Tile.character = this.gameObject;
			tile = tile.W_Tile;
			break;
		default:
			break;
		}
	}

	void Update(){
		if (moving && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				moving = false;
			}
		}
	}
}
