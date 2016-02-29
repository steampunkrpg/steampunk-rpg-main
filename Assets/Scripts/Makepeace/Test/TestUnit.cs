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
	public float att_range = 1;
	public int dis;

	void Awake() {
		char_stats = this.GetComponentInChildren<Stats> ();
		this.GetComponentInChildren<ParticleSystem> ().Stop (true);
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
		TestGameManager.instanceT.ResetTileDis ();
		TestGameManager.instanceT.ResetTilePar ();
		float totalMov = movement;
		TestHexTile viewTile = null;
		List<TestHexTile> visitedTile = new List<TestHexTile> ();

		this.tile.dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (TestHexTile tile in visitedTile) {
				if (viewTile == null || tile.dis < viewTile.dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.E_Tile.character == null && viewTile.E_Tile.mov_cost + viewTile.dis <= totalMov) {
				if (viewTile.E_Tile.dis != -1 && viewTile.E_Tile.dis > viewTile.dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.dis = viewTile.dis + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.dis == -1) {
					viewTile.E_Tile.dis = viewTile.dis + viewTile.E_Tile.mov_cost;
					viewTile.E_Tile.transform.Find ("Possible_Move").gameObject.SetActive (true);
					visitedTile.Add (viewTile.E_Tile);
				}
			}

			if (viewTile.W_Tile != null && viewTile.W_Tile.character == null && viewTile.W_Tile.mov_cost + viewTile.dis <= totalMov) {
				if (viewTile.W_Tile.dis != -1 && viewTile.W_Tile.dis > viewTile.dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.dis = viewTile.dis + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.dis == -1) {
					viewTile.W_Tile.dis = viewTile.dis + viewTile.W_Tile.mov_cost;
					viewTile.W_Tile.transform.Find ("Possible_Move").gameObject.SetActive (true);
					visitedTile.Add (viewTile.W_Tile);
				}
			}

			if (viewTile.SE_Tile != null && viewTile.SE_Tile.character == null && viewTile.SE_Tile.mov_cost + viewTile.dis <= totalMov) {
				if (viewTile.SE_Tile.dis != -1 && viewTile.SE_Tile.dis > viewTile.dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.dis = viewTile.dis + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.dis == -1) {
					viewTile.SE_Tile.dis = viewTile.dis + viewTile.SE_Tile.mov_cost;
					viewTile.SE_Tile.transform.Find ("Possible_Move").gameObject.SetActive (true);
					visitedTile.Add (viewTile.SE_Tile);
				}
			}

			if (viewTile.SW_Tile != null && viewTile.SW_Tile.character == null && viewTile.SW_Tile.mov_cost + viewTile.dis <= totalMov) {
				if (viewTile.SW_Tile.dis != -1 && viewTile.SW_Tile.dis > viewTile.dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.dis = viewTile.dis + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.dis == -1) {
					viewTile.SW_Tile.dis = viewTile.dis + viewTile.SW_Tile.mov_cost;
					viewTile.SW_Tile.transform.Find ("Possible_Move").gameObject.SetActive (true);
					visitedTile.Add (viewTile.SW_Tile);
				}
			}

			if (viewTile.NE_Tile != null && viewTile.NE_Tile.character == null && viewTile.NE_Tile.mov_cost + viewTile.dis <= totalMov) {
				if (viewTile.NE_Tile.dis != -1 && viewTile.NE_Tile.dis > viewTile.dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.dis = viewTile.dis + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.dis == -1) {
					viewTile.NE_Tile.dis = viewTile.dis + viewTile.NE_Tile.mov_cost;
					viewTile.NE_Tile.transform.Find ("Possible_Move").gameObject.SetActive (true);
					visitedTile.Add (viewTile.NE_Tile);
				}
			}

			if (viewTile.NW_Tile != null && viewTile.NW_Tile.character == null && viewTile.NW_Tile.mov_cost + viewTile.dis <= totalMov) {
				if (viewTile.NW_Tile.dis != -1 && viewTile.NW_Tile.dis > viewTile.dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.dis = viewTile.dis + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.dis == -1) {
					viewTile.NW_Tile.dis = viewTile.dis + viewTile.NW_Tile.mov_cost;
					viewTile.NW_Tile.transform.Find ("Possible_Move").gameObject.SetActive (true);
					visitedTile.Add (viewTile.NW_Tile);
				}
			}

			viewTile = null;
		}
	}

	public void possibleAttack() {
		TestGameManager.instanceT.ResetTileDis ();
		TestGameManager.instanceT.ResetTilePar ();
		TestHexTile viewTile = null;
		List<TestHexTile> visitedTile = new List<TestHexTile> ();

		this.tile.dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (TestHexTile tile in visitedTile) {
				if (viewTile == null || tile.dis < viewTile.dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.E_Tile.character == null && viewTile.E_Tile.mov_cost + 1 <= att_range) {
				if (viewTile.E_Tile.dis != -1 && viewTile.E_Tile.dis > viewTile.dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.dis = 1 + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.dis == -1) {
					viewTile.E_Tile.dis = 1 + viewTile.E_Tile.mov_cost;
					visitedTile.Add (viewTile.E_Tile);
				}
			}

			if (viewTile.W_Tile != null && viewTile.W_Tile.character == null && viewTile.W_Tile.mov_cost + 1 <= att_range) {
				if (viewTile.W_Tile.dis != -1 && viewTile.W_Tile.dis > viewTile.dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.dis = 1 + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.dis == -1) {
					viewTile.W_Tile.dis = 1 + viewTile.W_Tile.mov_cost;
					visitedTile.Add (viewTile.W_Tile);
				}
			}

			if (viewTile.SE_Tile != null && viewTile.SE_Tile.character == null && viewTile.SE_Tile.mov_cost + 1 <= att_range) {
				if (viewTile.SE_Tile.dis != -1 && viewTile.SE_Tile.dis > viewTile.dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.dis = 1 + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.dis == -1) {
					viewTile.SE_Tile.dis = 1 + viewTile.SE_Tile.mov_cost;
					visitedTile.Add (viewTile.SE_Tile);
				}
			}

			if (viewTile.SW_Tile != null && viewTile.SW_Tile.character == null && viewTile.SW_Tile.mov_cost + 1 <= att_range) {
				if (viewTile.SW_Tile.dis != -1 && viewTile.SW_Tile.dis > viewTile.dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.dis = 1 + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.dis == -1) {
					viewTile.SW_Tile.dis = 1 + viewTile.SW_Tile.mov_cost;
					visitedTile.Add (viewTile.SW_Tile);
				}
			}

			if (viewTile.NE_Tile != null && viewTile.NE_Tile.character == null && viewTile.NE_Tile.mov_cost + 1 <= att_range) {
				if (viewTile.NE_Tile.dis != -1 && viewTile.NE_Tile.dis > viewTile.dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.dis = 1 + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.dis == -1) {
					viewTile.NE_Tile.dis = 1 + viewTile.NE_Tile.mov_cost;
					visitedTile.Add (viewTile.NE_Tile);
				}
			}

			if (viewTile.NW_Tile != null && viewTile.NW_Tile.character == null && viewTile.NW_Tile.mov_cost + 1 <= att_range) {
				if (viewTile.NW_Tile.dis != -1 && viewTile.NW_Tile.dis > viewTile.dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.dis = 1 + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.dis == -1) {
					viewTile.NW_Tile.dis = 1 + viewTile.NW_Tile.mov_cost;
					visitedTile.Add (viewTile.NW_Tile);
				}
			}

			viewTile = null;
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
				possibleMoves ();
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
