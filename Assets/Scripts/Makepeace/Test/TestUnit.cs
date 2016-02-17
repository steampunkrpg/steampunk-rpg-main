using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TestUnit : MonoBehaviour {
	public TestHexTile tile;
	public bool Active;
	public bool moving;

	public GameObject char_class;
	public Inventory inv;
	public Stats char_stats;
	public float movement;
	public int dis;

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

	public void possibleMoves() {
		float totalMov = char_stats.MOV;
		Queue<TestHexTile> tileQ = new Queue<TestHexTile> ();
		Queue<TestHexTile> tileCQ = new Queue<TestHexTile> ();

		tileQ.Enqueue (tile);

		while (tileQ.Count > 0) {
			TestHexTile current = tileQ.Dequeue ();
			tileCQ.Enqueue (current);

			if (current.NW_Tile != null && current.NW_Tile.character == null && totalMov >= tile.NW_Tile.mov_cost && !tileCQ.Contains(current.NW_Tile)) {
				totalMov -= tile.NW_Tile.mov_cost;
				tileQ.Enqueue (current.NW_Tile);
			} else if (current.NW_Tile != null && movement >= tile.NW_Tile.mov_cost && !tileCQ.Contains(current.NW_Tile)) {
				current.transform.Find ("NW_Wall").gameObject.SetActive (true);
			}

			if (current.NE_Tile != null && current.NE_Tile.character == null && totalMov >= tile.NE_Tile.mov_cost && !tileCQ.Contains(current.NE_Tile)) {
				totalMov -= tile.NE_Tile.mov_cost;
				tileQ.Enqueue (current.NE_Tile);
			} else if (current.NE_Tile != null && movement >= tile.NE_Tile.mov_cost && !tileCQ.Contains(current.NW_Tile)) {
				current.transform.Find ("NE_Wall").gameObject.SetActive (true);
			}
			/*
			if (current.NW_Tile != null && current.NW_Tile.character == null && totalMov >= tile.NW_Tile.mov_cost && tileCQ.Contains(current.NW_Tile)) {
				totalMov -= tile.NW_Tile.mov_cost;
				tileQ.Enqueue (current.NW_Tile);
			} else if (current.NW_Tile != null && movement >= tile.SW_Tile.mov_cost) {
				current.NW_Tile.transform.Find ("NW_Wall").gameObject.SetActive (true);
			}

			if (current.NW_Tile != null && current.NW_Tile.character == null && totalMov >= tile.NW_Tile.mov_cost && tileCQ.Contains(current.NW_Tile)) {
				totalMov -= tile.NW_Tile.mov_cost;
				tileQ.Enqueue (current.NW_Tile);
			} else if (current.NW_Tile != null && movement >= tile.SW_Tile.mov_cost) {
				current.NW_Tile.transform.Find ("NW_Wall").gameObject.SetActive (true);
			}

			if (current.NW_Tile != null && current.NW_Tile.character == null && totalMov >= tile.NW_Tile.mov_cost && tileCQ.Contains(current.NW_Tile)) {
				totalMov -= tile.NW_Tile.mov_cost;
				tileQ.Enqueue (current.NW_Tile);
			} else if (current.NW_Tile != null && movement >= tile.SW_Tile.mov_cost) {
				current.NW_Tile.transform.Find ("NW_Wall").gameObject.SetActive (true);
			}

			if (current.NW_Tile != null && current.NW_Tile.character == null && totalMov >= tile.NW_Tile.mov_cost && tileCQ.Contains(current.NW_Tile)) {
				totalMov -= tile.NW_Tile.mov_cost;
				tileQ.Enqueue (current.NW_Tile);
			} else if (current.NW_Tile != null && movement >= tile.SW_Tile.mov_cost) {
				current.NW_Tile.transform.Find ("NW_Wall").gameObject.SetActive (true);
			}*/
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
