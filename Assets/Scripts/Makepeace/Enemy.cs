using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public HexTile tile;
	public Stats enemy_stats;

	public int Status;
	public float movement;
	public int special;
	public List<float> att_range;
	public List<Unit> attackablePlayers;
	private Unit attackablePlayer;
	public Animator animEnemy;
	public float currRotation;

	private List<HexTile> movePath;

	void Start() {
		enemy_stats = this.GetComponentInChildren<Stats> ();
		Status = 0;
	}

	public void InitPosition () {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
		movement = this.GetComponentInChildren<Stats> ().Mov;
	}

	public void MoveEnemy() {
		attackablePlayer = null;

		FindPossiblePlayers ();
		Debug.Log (attackablePlayers);

		for (int i = 0; i < attackablePlayers.Count; i++) {
			if (attackablePlayer == null || attackablePlayer.char_stats.cHP >= attackablePlayers [i].char_stats.cHP) {
				attackablePlayer = attackablePlayers [i];
			}
		}

		if (attackablePlayer != null) {
			GameManager.instance.activeEnemy = this;
			attackablePlayers = new List<Unit> ();
			FindAttackablePlayers (this.tile);

			if (attackablePlayers.Contains (attackablePlayer)) {
				animEnemy.Play("Idle");
				GameManager.instance.activePlayer = attackablePlayer;
				GameManager.instance.InitiateBattle (this.tile, attackablePlayer.tile);

				Status = 0;
				GameManager.instance.State = 0;
				GameManager.instance.prevState = 2;

				//Call Battle Animation Scene
				GameManager.instance.LoadScene("BattleView");

				return;
			} else {
				this.tile.character = null;
				MoveTowardsPlayer (attackablePlayer);
				Status = 2;
				animEnemy.Play("Walk");
			}
		} else {
			Status = 0;
		}
	}

	private void FindPossiblePlayers() {
		List<Unit> attackablePlayers = new List<Unit>();

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
			if (viewTile.character == null || viewTile == this.tile) {
				FindAttackablePlayers (viewTile);
			}

			if (viewTile.E_Tile != null && (viewTile.E_Tile.character == null || viewTile.E_Tile.character.tag == "Enemy") && viewTile.E_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.E_Tile.mov_dis != -1 && viewTile.E_Tile.mov_dis > viewTile.mov_dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.mov_dis == -1) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
					visitedTile.Add (viewTile.E_Tile);
				}
			}

			if (viewTile.W_Tile != null && (viewTile.W_Tile.character == null || viewTile.W_Tile.character.tag == "Enemy") && viewTile.W_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.W_Tile.mov_dis != -1 && viewTile.W_Tile.mov_dis > viewTile.mov_dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.mov_dis == -1) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
					visitedTile.Add (viewTile.W_Tile);
				}
			}

			if (viewTile.SE_Tile != null && (viewTile.SE_Tile.character == null || viewTile.SE_Tile.character.tag == "Enemy") && viewTile.SE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SE_Tile.mov_dis != -1 && viewTile.SE_Tile.mov_dis > viewTile.mov_dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.mov_dis == -1) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
					visitedTile.Add (viewTile.SE_Tile);
				}
			}

			if (viewTile.SW_Tile != null && (viewTile.SW_Tile.character == null || viewTile.SW_Tile.character.tag == "Enemy") && viewTile.SW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SW_Tile.mov_dis != -1 && viewTile.SW_Tile.mov_dis > viewTile.mov_dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.mov_dis == -1) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
					visitedTile.Add (viewTile.SW_Tile);
				}
			}

			if (viewTile.NE_Tile != null && (viewTile.NE_Tile.character == null || viewTile.NE_Tile.character.tag == "Enemy") && viewTile.NE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NE_Tile.mov_dis != -1 && viewTile.NE_Tile.mov_dis > viewTile.mov_dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.mov_dis == -1) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
					visitedTile.Add (viewTile.NE_Tile);
				}
			}

			if (viewTile.NW_Tile != null && (viewTile.NW_Tile.character == null || viewTile.NW_Tile.character.tag == "Enemy") && viewTile.NW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NW_Tile.mov_dis != -1 && viewTile.NW_Tile.mov_dis > viewTile.mov_dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.mov_dis == -1) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
					visitedTile.Add (viewTile.NW_Tile);
				}
			}

			viewTile = null;
		}
	}

	private void FindAttackablePlayers(HexTile origin) {
		att_range = this.GetComponentInChildren<Weapon> ().Rng;
		GameManager.instance.ResetTileAttDis ();
		GameManager.instance.ResetTileParticles ();
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		origin.att_dis = 0;

		visitedTile.Add (origin);

		while (visitedTile.Count > 0) {
			viewTile = null;
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.att_dis < viewTile.att_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.E_Tile.character != null && viewTile.E_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.E_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.E_Tile.character.GetComponentInChildren<Unit> ());
						}
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
					if (viewTile.att_dis + 1 == att_range [i] && viewTile.W_Tile.character != null && viewTile.W_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.W_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.W_Tile.character.GetComponentInChildren<Unit> ());
						}
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
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NE_Tile.character != null && viewTile.NE_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.NE_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.NE_Tile.character.GetComponentInChildren<Unit> ());
						}
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
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NW_Tile.character != null && viewTile.NW_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.NW_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.NW_Tile.character.GetComponentInChildren<Unit> ());
						}
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
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SE_Tile.character != null && viewTile.SE_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.SE_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.SE_Tile.character.GetComponentInChildren<Unit> ());
						}
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
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SW_Tile.character != null && viewTile.SW_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.SW_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.SW_Tile.character.GetComponentInChildren<Unit> ());
						}
					}
				}

				if (viewTile.SW_Tile.att_dis == -1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SW_Tile);
				} else if (viewTile.SW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}
		}
	}

	public void FindMoveTiles() {
		List<Unit> attackablePlayers = new List<Unit>();

		GameManager.instance.ResetTileMovDis ();
		GameManager.instance.ResetTileParticles ();
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

			if (viewTile.character == null || viewTile == this.tile) {
				FindAttackableTiles (viewTile);
			}

			if (viewTile.E_Tile != null && (viewTile.E_Tile.character == null || viewTile.E_Tile.character.tag == "Enemy") && viewTile.E_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.E_Tile.mov_dis != -1 && viewTile.E_Tile.mov_dis > viewTile.mov_dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.mov_dis == -1) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
					visitedTile.Add (viewTile.E_Tile);

					if (!movePath.Contains (viewTile.E_Tile)) {
						movePath.Add (viewTile.E_Tile);
					}

					ParticleSystem partSys = viewTile.E_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
					if (partSys.startColor != new Color32 (0, 120, 255, 255)) {
						partSys.startColor = new Color32 (0, 120, 255, 255);
					}
				}
			}

			if (viewTile.W_Tile != null && (viewTile.W_Tile.character == null || viewTile.W_Tile.character.tag == "Enemy") && viewTile.W_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.W_Tile.mov_dis != -1 && viewTile.W_Tile.mov_dis > viewTile.mov_dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.mov_dis == -1) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
					visitedTile.Add (viewTile.W_Tile);

					if (!movePath.Contains (viewTile.W_Tile)) {
						movePath.Add (viewTile.W_Tile);
					}

					ParticleSystem partSys = viewTile.W_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
					if (partSys.startColor != new Color32 (0, 120, 255, 255)) {
						partSys.startColor = new Color32 (0, 120, 255, 255);
					} 
				}
			}

			if (viewTile.SE_Tile != null && (viewTile.SE_Tile.character == null || viewTile.SE_Tile.character.tag == "Enemy") && viewTile.SE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SE_Tile.mov_dis != -1 && viewTile.SE_Tile.mov_dis > viewTile.mov_dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.mov_dis == -1) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
					visitedTile.Add (viewTile.SE_Tile);

					if (!movePath.Contains (viewTile.SE_Tile)) {
						movePath.Add (viewTile.SE_Tile);
					}

					ParticleSystem partSys = viewTile.SE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
					if (partSys.startColor != new Color32 (0, 120, 255, 255)) {
						partSys.startColor = new Color32 (0, 120, 255, 255);
					} 
				}
			}

			if (viewTile.SW_Tile != null && (viewTile.SW_Tile.character == null || viewTile.SW_Tile.character.tag == "Enemy") && viewTile.SW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SW_Tile.mov_dis != -1 && viewTile.SW_Tile.mov_dis > viewTile.mov_dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.mov_dis == -1) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
					visitedTile.Add (viewTile.SW_Tile);

					if (!movePath.Contains (viewTile.SW_Tile)) {
						movePath.Add (viewTile.SW_Tile);
					}

					ParticleSystem partSys = viewTile.SW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
					if (partSys.startColor != new Color32 (0, 120, 255, 255)) {
						partSys.startColor = new Color32 (0, 120, 255, 255);
					} 
				}
			}

			if (viewTile.NE_Tile != null && (viewTile.NE_Tile.character == null || viewTile.NE_Tile.character.tag == "Enemy") && viewTile.NE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NE_Tile.mov_dis != -1 && viewTile.NE_Tile.mov_dis > viewTile.mov_dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.mov_dis == -1) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
					visitedTile.Add (viewTile.NE_Tile);

					if (!movePath.Contains (viewTile.NE_Tile)) {
						movePath.Add (viewTile.NE_Tile);
					}

					ParticleSystem partSys = viewTile.NE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
					if (partSys.startColor != new Color32 (0, 120, 255, 255)) {
						partSys.startColor = new Color32 (0, 120, 255, 255);
					} 
				}
			}

			if (viewTile.NW_Tile != null && (viewTile.NW_Tile.character == null || viewTile.NW_Tile.character.tag == "Enemy") && viewTile.NW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NW_Tile.mov_dis != -1 && viewTile.NW_Tile.mov_dis > viewTile.mov_dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.mov_dis == -1) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
					visitedTile.Add (viewTile.NW_Tile);

					if (!movePath.Contains (viewTile.NW_Tile)) {
						movePath.Add (viewTile.NW_Tile);
					}

					ParticleSystem partSys = viewTile.NW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
					if (partSys.startColor != new Color32 (0, 120, 255, 255)) {
						partSys.startColor = new Color32 (0, 120, 255, 255);
					} 
				}
			}
				
			viewTile = null;
		}

		PlayTileParticles ();
	}

	private void PlayTileParticles () {
		foreach (HexTile tile in GameManager.instance.tileL) {
			if (movePath.Contains(tile) && tile != this.tile) {
				tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Play ();
			}
		}
	}

	private void FindAttackableTiles (HexTile origin) {
		att_range = this.GetComponentInChildren<Weapon> ().Rng;
		GameManager.instance.ResetTileAttDis ();
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		origin.att_dis = 0;

		visitedTile.Add (origin);

		while (visitedTile.Count > 0) {
			viewTile = null;
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.att_dis < viewTile.att_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray () [att_range.Count - 1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i]) {
						if (!movePath.Contains (viewTile.E_Tile)) {
							movePath.Add (viewTile.E_Tile);
							ParticleSystem partSys = viewTile.E_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
							partSys.startColor = new Color32 (255, 0, 0, 255);	
						}				 
					}
				}

				if (viewTile.E_Tile.att_dis == -1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.E_Tile);
				} else if (viewTile.E_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.W_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray () [att_range.Count - 1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i]) {
						if (!movePath.Contains (viewTile.W_Tile)) {
							movePath.Add (viewTile.W_Tile);
							ParticleSystem partSys = viewTile.W_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
							partSys.startColor = new Color32 (255, 0, 0, 255);
						}
					}
				}

				if (viewTile.W_Tile.att_dis == -1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.W_Tile);
				} else if (viewTile.W_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray () [att_range.Count - 1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i]) {
						if (!movePath.Contains (viewTile.NE_Tile)) {
							movePath.Add (viewTile.NE_Tile);
							ParticleSystem partSys = viewTile.NE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
							partSys.startColor = new Color32 (255, 0, 0, 255);
						}
					}
				}

				if (viewTile.NE_Tile.att_dis == -1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NE_Tile);
				} else if (viewTile.NE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray () [att_range.Count - 1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i]) {
						if (!movePath.Contains (viewTile.NW_Tile)) {
							movePath.Add (viewTile.NW_Tile);
							ParticleSystem partSys = viewTile.NW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
							partSys.startColor = new Color32 (255, 0, 0, 255);
						}
					}
				}

				if (viewTile.NW_Tile.att_dis == -1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NW_Tile);
				} else if (viewTile.NW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray () [att_range.Count - 1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i]) {
						if (!movePath.Contains (viewTile.SE_Tile)) {
							movePath.Add (viewTile.SE_Tile);
							ParticleSystem partSys = viewTile.SE_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
							partSys.startColor = new Color32 (255, 0, 0, 255);
						}
					}
				}

				if (viewTile.SE_Tile.att_dis == -1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SE_Tile);
				} else if (viewTile.SE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray () [att_range.Count - 1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i]) {
						if (!movePath.Contains (viewTile.SW_Tile)) {
							movePath.Add (viewTile.SW_Tile);
							ParticleSystem partSys = viewTile.SW_Tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ();
							partSys.startColor = new Color32 (255, 0, 0, 255);
						}
					}
				}

				if (viewTile.SW_Tile.att_dis == -1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SW_Tile);
				} else if (viewTile.SW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}
		}
	}

	private void MoveTowardsPlayer(Unit player) {
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
			attackablePlayers = new List<Unit> ();
			if (viewTile.character == null || viewTile == this.tile) {
				FindAttackablePlayers (viewTile);
			}
			if(attackablePlayers.Contains(player)) {
				break;
			}

			if (viewTile.E_Tile != null && (viewTile.E_Tile.character == null || viewTile.E_Tile.character.tag == "Enemy") && viewTile.E_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.E_Tile.mov_dis != -1 && viewTile.E_Tile.mov_dis > viewTile.mov_dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.mov_dis == -1) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
					viewTile.E_Tile.parent = viewTile;
					visitedTile.Add (viewTile.E_Tile);
				}
			}

			if (viewTile.W_Tile != null && (viewTile.W_Tile.character == null || viewTile.W_Tile.character.tag == "Enemy") && viewTile.W_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.W_Tile.mov_dis != -1 && viewTile.W_Tile.mov_dis > viewTile.mov_dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.mov_dis == -1) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
					viewTile.W_Tile.parent = viewTile;
					visitedTile.Add (viewTile.W_Tile);
				}
			}

			if (viewTile.SE_Tile != null && (viewTile.SE_Tile.character == null || viewTile.SE_Tile.character.tag == "Enemy") && viewTile.SE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SE_Tile.mov_dis != -1 && viewTile.SE_Tile.mov_dis > viewTile.mov_dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.mov_dis == -1) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
					viewTile.SE_Tile.parent = viewTile;
					visitedTile.Add (viewTile.SE_Tile);
				}
			}

			if (viewTile.SW_Tile != null && (viewTile.SW_Tile.character == null || viewTile.SW_Tile.character.tag == "Enemy") && viewTile.SW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SW_Tile.mov_dis != -1 && viewTile.SW_Tile.mov_dis > viewTile.mov_dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.mov_dis == -1) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
					viewTile.SW_Tile.parent = viewTile;
					visitedTile.Add (viewTile.SW_Tile);
				}
			}

			if (viewTile.NE_Tile != null && (viewTile.NE_Tile.character == null || viewTile.NE_Tile.character.tag == "Enemy") && viewTile.NE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NE_Tile.mov_dis != -1 && viewTile.NE_Tile.mov_dis > viewTile.mov_dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.mov_dis == -1) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
					viewTile.NE_Tile.parent = viewTile;
					visitedTile.Add (viewTile.NE_Tile);
				}
			}

			if (viewTile.NW_Tile != null && (viewTile.NW_Tile.character == null || viewTile.NW_Tile.character.tag == "Enemy") && viewTile.NW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NW_Tile.mov_dis != -1 && viewTile.NW_Tile.mov_dis > viewTile.mov_dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.mov_dis == -1) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
					viewTile.NW_Tile.parent = viewTile;
					visitedTile.Add (viewTile.NW_Tile);
				}
			}

			viewTile = null;
		}
			
		while (true) {
			if (viewTile.parent != this.tile) {
				movePath.Add (viewTile);
				viewTile = viewTile.parent;
			} else {
				viewTile.parent = this.tile;
				MoveTile (viewTile);
				break;
			}
		}
	}

	void MoveTile(HexTile nextTile) {
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
		this.tile = nextTile;

		if (movePath.Count == 0) {
			this.tile.character = this.gameObject;
		}
		movement--;
	}

	void Update(){
		if (Status == 2 && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				if (movePath.Count > 0) {
					HexTile nextTile = movePath[movePath.Count - 1];
					movePath.RemoveAt (movePath.Count - 1);
					MoveTile (nextTile);
				} else {
					animEnemy.Play("Idle");
					this.transform.Rotate(new Vector3(0.0f, -currRotation, 0.0f));
					currRotation = 0;
					GameManager.instance.activePlayer = attackablePlayer;
					GameManager.instance.InitiateBattle (this.tile, attackablePlayer.tile);

					Status = 0;

					GameManager.instance.State = 0;
					GameManager.instance.prevState = 2;

					//Call Battle Animation Scene
					GameManager.instance.LoadScene("BattleView");

					return;
				}            
			}        
		}
	}

	public void DontDestroy() {
		DontDestroyOnLoad (this);
	}

	public void Death() {
		Item[] dropItems = this.GetComponents<Item> ();
		if (dropItems != null) {
			foreach (Item item in dropItems) {
				GameManager.instance.InvUI.GetComponent<InventoryManager> ().AddItem (item.iName, 1);
			}
		}

		GameManager.instance.enemyL.Remove (this);
		Destroy (this.gameObject);
	}

	public void ResetMovement() {
		movement = enemy_stats.Mov;
	}
}
