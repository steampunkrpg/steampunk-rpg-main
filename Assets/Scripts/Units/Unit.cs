using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public HexTile tile;
	public bool Active;
	public bool moving;

	public GameObject char_class;
	public Stats char_stats;
	public float movement;

	void Awake() {
		char_stats = this.GetComponentInChildren<Stats> ();
		Active = false;
	}

	public void InitPosition() {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
		movement = char_stats.MOV;
	}

	bool ValidMove(int dir) {
		switch (dir) {
		case 1:
			return (tile.NW_Tile != null && tile.NW_Tile.character == null && movement >= tile.NW_Tile.mov_cost);
		case 2:
			return (tile.NE_Tile != null && tile.NE_Tile.character == null&& movement >= tile.NE_Tile.mov_cost);
		case 3:
			return (tile.E_Tile != null && tile.E_Tile.character == null && movement >= tile.E_Tile.mov_cost);
		case 4:
			return (tile.SE_Tile != null && tile.SE_Tile.character == null && movement >= tile.SE_Tile.mov_cost);
		case 5:
			return (tile.SW_Tile != null && tile.SW_Tile.character == null && movement >= tile.SW_Tile.mov_cost);
		case 6:
			return (tile.W_Tile != null && tile.W_Tile.character == null && movement >= tile.W_Tile.mov_cost);
		default:
			return false;
		}
	}

	public void Move(int dir) {
		bool validMove = ValidMove (dir);
		if (validMove && movement != 0) {
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

			moving = true;
			movement = movement - tile.mov_cost;
		}

		if (movement == 0) {
			Active = false;
		}
	}

	void Update() {
		if ((Active || moving) && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				moving = false;
			}
		}
	}

	public void DontDestroy() {
		DontDestroyOnLoad (this);
	}

	public void Death() {
		Destroy (this);
	}

	public void ResetMovement() {
		movement = char_stats.MOV;
	}
}
