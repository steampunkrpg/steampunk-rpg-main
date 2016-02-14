using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public HexTile tile;
	public bool Active;

	void Start() {
		this.transform.position = tile.transform.position;
		this.transform.Translate(new Vector3 (0.0f, 0.5f, 0.0f));

		Active = true;
	}

	bool ValidMove(int dir) {
		switch (dir) {
		case 1:
			return (tile.NE_Tile != null && tile.character != null);
		case 2:
			return (tile.NW_Tile != null && tile.character != null);
		case 3:
			return (tile.E_Tile != null && tile.character != null);
		case 4:
			return (tile.SE_Tile != null && tile.character != null);
		case 5:
			return (tile.SW_Tile != null && tile.character != null);
		case 6:
			return (tile.W_Tile != null && tile.character != null);
		default:
			return false;
		}
	}

	public void Move(int dir) {
		if (ValidMove (dir)) {
			tile.character = null;

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

			Active = false;
		}
	}

	void Update() {
		if (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
		}
	}
}
