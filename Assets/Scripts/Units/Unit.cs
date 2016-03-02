using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Unit : MonoBehaviour {
	public HexTile tile;
	public int Status;

	public GameObject char_class;
	public Stats har_stats;

	public float movement;
	public List<float> att_range;
	public int dis;

	void Awake() {
		this.GetComponentInChildren<ParticleSystem> ().Stop (true);
		Status = 0;
	}

	public void InitPosition() {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
		movement = this.GetComponentInChildren<Stats>().Mov;
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
		GameManager.instance.ResetTileDis ();
		float totalMov = movement;
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
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
		att_range = this.GetComponentInChildren<Weapon> ().Rng;
		GameManager.instance.ResetTileDis ();
		GameManager.instance.ResetTilePar ();
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.dis < viewTile.dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.E_Tile.character != null && viewTile.E_Tile.character.tag == "Enemy") {
						viewTile.E_Tile.character.transform.Find ("Particle").gameObject.SetActive (true);
					}
				}

				if (viewTile.E_Tile.dis == -1) {
					viewTile.E_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.E_Tile);
				} else if (viewTile.E_Tile.dis > viewTile.dis + 1) {
					viewTile.E_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.W_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.W_Tile.character != null && viewTile.W_Tile.character.tag == "Enemy") {
						viewTile.W_Tile.character.transform.Find ("Particle").gameObject.SetActive (true);
					}
				}

				if (viewTile.W_Tile.dis == -1) {
					viewTile.W_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.W_Tile);
				} else if (viewTile.W_Tile.dis > viewTile.dis + 1) {
					viewTile.W_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.NE_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.NE_Tile.character != null && viewTile.NE_Tile.character.tag == "Enemy") {
						viewTile.NE_Tile.character.transform.Find ("Particle").gameObject.SetActive (true);
					}
				}

				if (viewTile.NE_Tile.dis == -1) {
					viewTile.NE_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.NE_Tile);
				} else if (viewTile.NE_Tile.dis > viewTile.dis + 1) {
					viewTile.NE_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.NW_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.NW_Tile.character != null && viewTile.NW_Tile.character.tag == "Enemy") {
						viewTile.NW_Tile.character.transform.Find ("Particle").gameObject.SetActive (true);
					}
				}

				if (viewTile.NW_Tile.dis == -1) {
					viewTile.NW_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.NW_Tile);
				} else if (viewTile.NW_Tile.dis > viewTile.dis + 1) {
					viewTile.NW_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.SE_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.SE_Tile.character != null && viewTile.SE_Tile.character.tag == "Enemy") {
						viewTile.SE_Tile.character.transform.Find ("Particle").gameObject.SetActive (true);
					}
				}

				if (viewTile.SE_Tile.dis == -1) {
					viewTile.SE_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.SE_Tile);
				} else if (viewTile.SE_Tile.dis > viewTile.dis + 1) {
					viewTile.SE_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.SW_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.SW_Tile.character != null && viewTile.SW_Tile.character.tag == "Enemy") {
						viewTile.SW_Tile.character.transform.Find ("Particle").gameObject.SetActive (true);
					}
				}

				if (viewTile.SW_Tile.dis == -1) {
					viewTile.SW_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.SW_Tile);
				} else if (viewTile.SW_Tile.dis > viewTile.dis + 1) {
					viewTile.SW_Tile.dis = viewTile.dis + 1;
				}
			}

			viewTile = null;
		}
	}

	public void possibleInteract() {
		att_range = this.GetComponentInChildren<Weapon> ().Rng;
		GameManager.instance.ResetTileDis ();
		GameManager.instance.ResetTilePar ();
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.dis < viewTile.dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.E_Tile.character != null && viewTile.E_Tile.character.tag == "Unit") {
						viewTile.E_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.E_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.E_Tile.dis == -1) {
					viewTile.E_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.E_Tile);
				} else if (viewTile.E_Tile.dis > viewTile.dis + 1) {
					viewTile.E_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.W_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.W_Tile.character != null && viewTile.W_Tile.character.tag == "Unit") {
						viewTile.W_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.W_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.W_Tile.dis == -1) {
					viewTile.W_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.W_Tile);
				} else if (viewTile.W_Tile.dis > viewTile.dis + 1) {
					viewTile.W_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.NE_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.NE_Tile.character != null && viewTile.NE_Tile.character.tag == "Unit") {
						viewTile.NE_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.NE_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.NE_Tile.dis == -1) {
					viewTile.NE_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.NE_Tile);
				} else if (viewTile.NE_Tile.dis > viewTile.dis + 1) {
					viewTile.NE_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.NW_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.NW_Tile.character != null && viewTile.NW_Tile.character.tag == "Unit") {
						viewTile.NW_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.NW_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.NW_Tile.dis == -1) {
					viewTile.NW_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.NW_Tile);
				} else if (viewTile.NW_Tile.dis > viewTile.dis + 1) {
					viewTile.NW_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.SE_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.SE_Tile.character != null && viewTile.SE_Tile.character.tag == "Unit") {
						viewTile.SE_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.SE_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.SE_Tile.dis == -1) {
					viewTile.SE_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.SE_Tile);
				} else if (viewTile.SE_Tile.dis > viewTile.dis + 1) {
					viewTile.SE_Tile.dis = viewTile.dis + 1;
				}
			}

			if (viewTile.SW_Tile != null && viewTile.dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.dis + 1 == att_range[i] && viewTile.SW_Tile.character != null && viewTile.SW_Tile.character.tag == "Unit") {
						viewTile.SW_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.SW_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.SW_Tile.dis == -1) {
					viewTile.SW_Tile.dis = viewTile.dis + 1;
					visitedTile.Add (viewTile.SW_Tile);
				} else if (viewTile.SW_Tile.dis > viewTile.dis + 1) {
					viewTile.SW_Tile.dis = viewTile.dis + 1;
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

			Status = 2;
			movement = movement - tile.mov_cost;
		}
	}

	void Update() {
		if (Status == 2 && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				if (movement == 0) {
					GameManager.instance.ResetTilePar ();
					Status = 0;
				} else {
					GameManager.instance.ResetTilePar ();
					Status = 1;
					possibleMoves ();
				}
			}
		}

		if (Status == 4) {
			Death ();
		}
	}

	public void DontDestroy() {
		DontDestroyOnLoad (this);
	}

	private void Death() {
		GameManager.instance.playerL.Remove (this);
		Destroy (this.gameObject);
	}

	public void ResetMovement() {
		movement = this.GetComponentInChildren<Stats>().Mov;
	}
}
