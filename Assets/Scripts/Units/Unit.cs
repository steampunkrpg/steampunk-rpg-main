using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
	public HexTile tile;
	public int Status;

	public GameObject char_class;
	public Text statsView;
	public Stats char_stats;
	public float movement;
	public List<float> att_range;
    public Animator animUnit;
    public float currRotation;
	public List<HexTile> movePath;

	void Awake() {
		this.GetComponentInChildren<ParticleSystem> ().Stop (true);
		char_stats = this.GetComponentInChildren<Stats> ();
		Status = 0;
        animUnit.Play("Idle");
		movePath = new List<HexTile> ();
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
		GameManager.instance.ResetTileMovDis ();
		float totalMov = movement;
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.mov_dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.mov_dis < viewTile.mov_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.E_Tile.character == null && viewTile.E_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.E_Tile.mov_dis != -1 && viewTile.E_Tile.mov_dis > viewTile.mov_dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.mov_dis == -1) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
					viewTile.E_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					visitedTile.Add (viewTile.E_Tile);
				}
			}

			if (viewTile.W_Tile != null && viewTile.W_Tile.character == null && viewTile.W_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.W_Tile.mov_dis != -1 && viewTile.W_Tile.mov_dis > viewTile.mov_dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.mov_dis == -1) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
					viewTile.W_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					visitedTile.Add (viewTile.W_Tile);
				}
			}

			if (viewTile.SE_Tile != null && viewTile.SE_Tile.character == null && viewTile.SE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SE_Tile.mov_dis != -1 && viewTile.SE_Tile.mov_dis > viewTile.mov_dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.mov_dis == -1) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
					viewTile.SE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					visitedTile.Add (viewTile.SE_Tile);
				}
			}

			if (viewTile.SW_Tile != null && viewTile.SW_Tile.character == null && viewTile.SW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SW_Tile.mov_dis != -1 && viewTile.SW_Tile.mov_dis > viewTile.mov_dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.mov_dis == -1) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
					viewTile.SW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					visitedTile.Add (viewTile.SW_Tile);
				}
			}

			if (viewTile.NE_Tile != null && viewTile.NE_Tile.character == null && viewTile.NE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NE_Tile.mov_dis != -1 && viewTile.NE_Tile.mov_dis > viewTile.mov_dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.mov_dis == -1) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
					viewTile.NE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					visitedTile.Add (viewTile.NE_Tile);
				}
			}

			if (viewTile.NW_Tile != null && viewTile.NW_Tile.character == null && viewTile.NW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NW_Tile.mov_dis != -1 && viewTile.NW_Tile.mov_dis > viewTile.mov_dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.mov_dis == -1) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
					viewTile.NW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					visitedTile.Add (viewTile.NW_Tile);
				}
			}

			viewTile = null;
		}
	}

	public void possibleAttack() {
		att_range = this.GetComponentInChildren<Weapon> ().Rng;
		GameManager.instance.ResetTileAttDis ();
		GameManager.instance.ResetTileParticles ();
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.att_dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.att_dis < viewTile.att_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.E_Tile.character == null || viewTile.E_Tile.character.tag == "Enemy")) {
						viewTile.E_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (255, 0, 0, 255);
						viewTile.E_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.E_Tile.character != null && viewTile.E_Tile.character.tag == "Enemy") {
						viewTile.E_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
					}
				}

				if (viewTile.E_Tile.att_dis == -1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.E_Tile);
				} else if (viewTile.E_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.W_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.W_Tile.character == null || viewTile.W_Tile.character.tag == "Enemy")) {
						viewTile.W_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (255, 0, 0, 255);
						viewTile.W_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range [i] && viewTile.W_Tile.character != null && viewTile.W_Tile.character.tag == "Enemy") {
						viewTile.W_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
					}
				}

				if (viewTile.W_Tile.att_dis == -1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.W_Tile);
				} else if (viewTile.W_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.NE_Tile.character == null || viewTile.NE_Tile.character.tag == "Enemy")) {
						viewTile.NE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (255, 0, 0, 255);
						viewTile.NE_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NE_Tile.character != null && viewTile.NE_Tile.character.tag == "Enemy") {
						viewTile.NE_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
					}
				}

				if (viewTile.NE_Tile.att_dis == -1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NE_Tile);
				} else if (viewTile.NE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.NW_Tile.character == null || viewTile.NW_Tile.character.tag == "Enemy")) {
						viewTile.NW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (255, 0, 0, 255);
						viewTile.NW_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NW_Tile.character != null && viewTile.NW_Tile.character.tag == "Enemy") {
						viewTile.NW_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
					}
				}

				if (viewTile.NW_Tile.att_dis == -1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NW_Tile);
				} else if (viewTile.NW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.SE_Tile.character == null || viewTile.SE_Tile.character.tag == "Enemy")) {
						viewTile.SE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (255, 0, 0, 255);
						viewTile.SE_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SE_Tile.character != null && viewTile.SE_Tile.character.tag == "Enemy") {
						viewTile.SE_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
					}
				}

				if (viewTile.SE_Tile.att_dis == -1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SE_Tile);
				} else if (viewTile.SE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.SW_Tile.character == null || viewTile.SW_Tile.character.tag == "Enemy")) {
						viewTile.SW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (255, 0, 0, 255);
						viewTile.SW_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SW_Tile.character != null && viewTile.SW_Tile.character.tag == "Enemy") {
						viewTile.SW_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
					}
				}

				if (viewTile.SW_Tile.att_dis == -1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SW_Tile);
				} else if (viewTile.SW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			viewTile = null;
		}
	}

	public void possibleInteract() {
		att_range = this.GetComponentInChildren<Weapon> ().Rng;
		GameManager.instance.ResetTileAttDis ();
		GameManager.instance.ResetTileParticles ();
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.att_dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.att_dis < viewTile.att_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.E_Tile.character == null || viewTile.E_Tile.character.tag == "Unit")) {
						viewTile.E_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (0, 255, 0, 255);
						viewTile.E_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.E_Tile.character != null && viewTile.E_Tile.character.tag == "Unit") {
						viewTile.E_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.E_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.E_Tile.att_dis == -1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.E_Tile);
				} else if (viewTile.E_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.W_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.W_Tile.character == null || viewTile.W_Tile.character.tag == "Unit")) {
						viewTile.W_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (0, 255, 0, 255);
						viewTile.W_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.W_Tile.character != null && viewTile.W_Tile.character.tag == "Unit") {
						viewTile.W_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.W_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.W_Tile.att_dis == -1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.W_Tile);
				} else if (viewTile.W_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.NE_Tile.character == null || viewTile.NE_Tile.character.tag == "Unit")) {
						viewTile.NE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (0, 255, 0, 255);
						viewTile.NE_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NE_Tile.character != null && viewTile.NE_Tile.character.tag == "Unit") {
						viewTile.NE_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.NE_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.NE_Tile.att_dis == -1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NE_Tile);
				} else if (viewTile.NE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.NW_Tile.character == null || viewTile.NW_Tile.character.tag == "Unit")) {
						viewTile.NW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (0, 255, 0, 255);
						viewTile.NW_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NW_Tile.character != null && viewTile.NW_Tile.character.tag == "Unit") {
						viewTile.NW_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.NW_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.NW_Tile.att_dis == -1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NW_Tile);
				} else if (viewTile.NW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.SE_Tile.character == null || viewTile.SE_Tile.character.tag == "Unit")) {
						viewTile.SE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (0, 255, 0, 255);
						viewTile.SE_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SE_Tile.character != null && viewTile.SE_Tile.character.tag == "Unit") {
						viewTile.SE_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.SE_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.SE_Tile.att_dis == -1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SE_Tile);
				} else if (viewTile.SE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && (viewTile.SW_Tile.character == null || viewTile.SW_Tile.character.tag == "Unit")) {
						viewTile.SW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().startColor = new Color32 (0, 255, 0, 255);
						viewTile.SW_Tile.transform.Find("Possible_Move").GetComponent<ParticleSystem> ().Play ();
					}
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SW_Tile.character != null && viewTile.SW_Tile.character.tag == "Unit") {
						viewTile.SW_Tile.character.GetComponentInChildren<ParticleSystem> ().Play (true);
						viewTile.SW_Tile.character.GetComponentInChildren<ParticleSystem> ().startColor = new Color (0, 255, 0);
					}
				}

				if (viewTile.SW_Tile.att_dis == -1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SW_Tile);
				} else if (viewTile.SW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
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
				this.transform.Rotate (new Vector3 (0.0f, 135.0f, 0.0f));
				currRotation = 135.0f;
				break;
			case 2:
				tile.NE_Tile.character = this.gameObject;
				tile = tile.NE_Tile;
				this.transform.Rotate (new Vector3 (0.0f, -135.0f, 0.0f));
				currRotation = -135.0f;
				break;
			case 3:
				tile.E_Tile.character = this.gameObject;
				tile = tile.E_Tile;
				this.transform.Rotate (new Vector3 (0.0f, -90.0f, 0.0f));
				currRotation = -90.0f;
				break;
			case 4:
				tile.SE_Tile.character = this.gameObject;
				tile = tile.SE_Tile;
				this.transform.Rotate (new Vector3 (0.0f, -45.0f, 0.0f));
				currRotation = -45.0f;
				break;
			case 5:
				tile.SW_Tile.character = this.gameObject;
				tile = tile.SW_Tile;
				this.transform.Rotate (new Vector3 (0.0f, 45.0f, 0.0f));
				currRotation = 45.0f;
				break;
			case 6:
				tile.W_Tile.character = this.gameObject;
				tile = tile.W_Tile;
				this.transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
				currRotation = 90.0f;
				break;
			default:
				break;
			}

			Status = 2;
            animUnit.Play("Walk");
			movement = movement - tile.mov_cost;
		}
	}

	public void MoveTo (HexTile desTile) {
		GameManager.instance.ResetTileMovDis ();
		GameManager.instance.ResetTileParent ();
		float totalMov = movement;
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();
		movePath = new List<HexTile> ();

		this.tile.mov_dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.mov_dis < viewTile.mov_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.E_Tile.character == null && viewTile.E_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.E_Tile.mov_dis != -1 && viewTile.E_Tile.mov_dis > viewTile.mov_dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.mov_dis == -1) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
					viewTile.E_Tile.parent = viewTile;
					visitedTile.Add (viewTile.E_Tile);
					if (viewTile.E_Tile == desTile) {
						break;
					}
				}
			}

			if (viewTile.W_Tile != null && viewTile.W_Tile.character == null && viewTile.W_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.W_Tile.mov_dis != -1 && viewTile.W_Tile.mov_dis > viewTile.mov_dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.mov_dis == -1) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
					viewTile.W_Tile.parent = viewTile;
					visitedTile.Add (viewTile.W_Tile);
					if (viewTile.W_Tile == desTile) {
						break;
					}
				}
			}

			if (viewTile.SE_Tile != null && viewTile.SE_Tile.character == null && viewTile.SE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SE_Tile.mov_dis != -1 && viewTile.SE_Tile.mov_dis > viewTile.mov_dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.mov_dis == -1) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
					viewTile.SE_Tile.parent = viewTile;
					visitedTile.Add (viewTile.SE_Tile);
					if (viewTile.SE_Tile == desTile) {
						break;
					}
				}
			}

			if (viewTile.SW_Tile != null && viewTile.SW_Tile.character == null && viewTile.SW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SW_Tile.mov_dis != -1 && viewTile.SW_Tile.mov_dis > viewTile.mov_dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.mov_dis == -1) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
					viewTile.SW_Tile.parent = viewTile;
					visitedTile.Add (viewTile.SW_Tile);
					if (viewTile.SW_Tile == desTile) {
						break;
					}
				}
			}

			if (viewTile.NE_Tile != null && viewTile.NE_Tile.character == null && viewTile.NE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NE_Tile.mov_dis != -1 && viewTile.NE_Tile.mov_dis > viewTile.mov_dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.mov_dis == -1) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
					viewTile.NE_Tile.parent = viewTile;
					visitedTile.Add (viewTile.NE_Tile);
					if (viewTile.NE_Tile == desTile) {
						break;
					}
				}
			}

			if (viewTile.NW_Tile != null && viewTile.NW_Tile.character == null && viewTile.NW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NW_Tile.mov_dis != -1 && viewTile.NW_Tile.mov_dis > viewTile.mov_dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.mov_dis == -1) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
					viewTile.NW_Tile.parent = viewTile;
					visitedTile.Add (viewTile.NW_Tile);
					if (viewTile.NW_Tile == desTile) {
						break;
					}
				}
			}

			viewTile = null;
		}

		viewTile = desTile;
		while (true) {
			if (viewTile.parent != this.tile) {
				movePath.Add (viewTile);
				viewTile = viewTile.parent;
			} else {
				viewTile.parent = this.tile;
				MoveTile (viewTile);
				Status = 2;
				animUnit.Play("Walk");
				break;
			}
		}
	}

	public void MoveTile (HexTile nextTile) {
		float newRotation = 0.0f;
		if (this.tile.parent == null) {
			if (this.tile.E_Tile == nextTile) {
				newRotation = -90.0f;
			} else if (this.tile.W_Tile == nextTile) {
				newRotation = 90.0f;
			} else if (this.tile.NE_Tile == nextTile) {
				newRotation = -150.0f;
			} else if (this.tile.NW_Tile == nextTile) {
				newRotation = 150.0f;
			} else if (this.tile.SE_Tile == nextTile) {
				newRotation = -30.0f;
			} else if (this.tile.SW_Tile == nextTile) {
				newRotation = 30.0f;
			}
		} else {
			if (this.tile.parent.E_Tile == this.tile) {
				if (nextTile == this.tile.E_Tile) {
					newRotation = 0.0f;
				} else if (nextTile == this.tile.W_Tile) {
					newRotation = 180.0f;
				} else if (nextTile == this.tile.NE_Tile) {
					newRotation = -60.0f;
				} else if (nextTile == this.tile.NW_Tile) {
					newRotation = -120.0f;
				} else if (nextTile == this.tile.SE_Tile) {
					newRotation = 60.0f;
				} else if (nextTile == this.tile.SW_Tile) {
					newRotation = 120.0f;
				} 
			} else if (this.tile.parent.W_Tile == this.tile) {
				if (nextTile == this.tile.E_Tile) {
					newRotation = 180.0f;
				} else if (nextTile == this.tile.W_Tile) {
					newRotation = 0.0f;
				} else if (nextTile == this.tile.NE_Tile) {
					newRotation = 120.0f;
				} else if (nextTile == this.tile.NW_Tile) {
					newRotation = 60.0f;
				} else if (nextTile == this.tile.SE_Tile) {
					newRotation = -120.0f;
				} else if (nextTile == this.tile.SW_Tile) {
					newRotation = -60.0f;
				} 
			} else if (this.tile.parent.NE_Tile == this.tile) {
				if (nextTile == this.tile.E_Tile) {
					newRotation = -120.0f;
				} else if (nextTile == this.tile.W_Tile) {
					newRotation = 60.0f;
				} else if (nextTile == this.tile.NE_Tile) {
					newRotation = 0.0f;
				} else if (nextTile == this.tile.NW_Tile) {
					newRotation = -60.0f;
				} else if (nextTile == this.tile.SE_Tile) {
					newRotation = 120.0f;
				} else if (nextTile == this.tile.SW_Tile) {
					newRotation = 180.0f;
				} 
			} else if (this.tile.parent.NW_Tile == this.tile) {
				if (nextTile == this.tile.E_Tile) {
					newRotation = 120.0f;
				} else if (nextTile == this.tile.W_Tile) {
					newRotation = -60.0f;
				} else if (nextTile == this.tile.NE_Tile) {
					newRotation = 60.0f;
				} else if (nextTile == this.tile.NW_Tile) {
					newRotation = 0.0f;
				} else if (nextTile == this.tile.SE_Tile) {
					newRotation = 180.0f;
				} else if (nextTile == this.tile.SW_Tile) {
					newRotation = -120.0f;
				} 
			} else if (this.tile.parent.SE_Tile == this.tile) {
				if (nextTile == this.tile.E_Tile) {
					newRotation = -60.0f;
				} else if (nextTile == this.tile.W_Tile) {
					newRotation = 120.0f;
				} else if (nextTile == this.tile.NE_Tile) {
					newRotation = -120.0f;
				} else if (nextTile == this.tile.NW_Tile) {
					newRotation = 180.0f;
				} else if (nextTile == this.tile.SE_Tile) {
					newRotation = 0.0f;
				} else if (nextTile == this.tile.SW_Tile) {
					newRotation = 60.0f;
				} 
			} else if (this.tile.parent.SW_Tile == this.tile) {
				if (nextTile == this.tile.E_Tile) {
					newRotation = -120.0f;
				} else if (nextTile == this.tile.W_Tile) {
					newRotation = 60.0f;
				} else if (nextTile == this.tile.NE_Tile) {
					newRotation = 120.0f;
				} else if (nextTile == this.tile.NW_Tile) {
					newRotation = 180.0f;
				} else if (nextTile == this.tile.SE_Tile) {
					newRotation = -60.0f;
				} else if (nextTile == this.tile.SW_Tile) {
					newRotation = 0.0f;
				} 
			}
		}

		this.transform.Rotate (new Vector3 (0.0f, newRotation, 0.0f));
		currRotation += newRotation;
		this.tile.character = null;
		this.tile = nextTile;
		this.tile.character = this.gameObject;
		movement--;
	}

	void Update() {
		if (Status == 2 && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {

			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				if (movePath.Count > 0) {
					HexTile nextTile = movePath [movePath.Count - 1];
					movePath.RemoveAt (movePath.Count - 1);
					MoveTile (nextTile);
				} else {
					Status = 1;
					GameManager.instance.PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
					GameManager.instance.PlayerUI.GetComponent<PlayerUI> ().UpdateUI (char_stats);
					GameManager.instance.ResetTileParticles ();
					animUnit.Play ("Idle");
					this.transform.Rotate (new Vector3 (0.0f, -currRotation, 0.0f));
					currRotation = 0;
				}
			}
		}
	}

	public void DontDestroy() {
		DontDestroyOnLoad (this);
	}

	public void Death() {
		GameManager.instance.playerL.Remove (this);
		Destroy (this.gameObject);
	}

	public void ResetMovement() {
		movement = this.GetComponentInChildren<Stats>().Mov;
	}
}
