using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float turnDelay = 0.1f;
	private PlayerKeyBoardInput playerInput;
	private XpGrowthRate xpGrowthRate;
	public GameObject LevelHUD;

	private Ray mouseRay;
	private RaycastHit hit;

	public List<HexTile> tileL;
	public List<Unit> playerL;
	public List<Enemy> enemyL;
	public Unit activePlayer = null;
	public Enemy activeEnemy = null;
	public Unit interactPlayer = null;

	public int State;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		tileL = new List<HexTile> ();
		playerL = new List<Unit> ();
		enemyL = new List<Enemy> ();

		xpGrowthRate = this.gameObject.GetComponent<XpGrowthRate> ();
		playerInput = this.gameObject.GetComponent<PlayerKeyBoardInput> ();

		State = 0;
	}

	public void InitGame() {
		enemyL = new List<Enemy> ();
		tileL = new List<HexTile> ();

		LevelHUD.SetActive (true);

		LoadLists ();
		State = 1;
	}

	void Update() {
		if (State == 1) {
			if (activePlayer == null || activePlayer.Status == 1) {
				PlayerSelect ();
			}

			if (activePlayer != null && activePlayer.Status == 1) {
				if (!activePlayer.GetComponentInChildren<ParticleSystem> ().isPlaying) {
					activePlayer.GetComponentInChildren<ParticleSystem> ().Play (true);
					activePlayer.possibleMoves ();
				}
			} else if (activePlayer != null && activePlayer.Status == 0) {
				activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
			}

			if (activePlayer != null && activePlayer.Status == 1) {
				playerInput.UnitAction (activePlayer);
			}

			if (activePlayer != null && activePlayer.Status == 5) {
				InteractSelect ();
				if (interactPlayer != null) {
					initiateInteraction (activePlayer.tile, interactPlayer.tile);
					ResetPlayerPar ();
					activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
					interactPlayer = null;
					activePlayer.Status = 0;
					if (activePlayer.GetComponentInChildren<Stats> ().Xp >= 100) {
						float[] lvStats = new float[8];
						string className = "";
						foreach (Transform child in activePlayer.transform) {
							if (child.tag == "Class") {
								className = child.name;
								break;
							}
						}

						lvStats = xpGrowthRate.GetGrowthRates (className);
						activePlayer.GetComponentInChildren<Stats> ().LevelUp (lvStats);
					}
				}
			}

			if (activePlayer != null && activePlayer.Status == 3) {
				EnemySelect ();
				if (activeEnemy != null) {
					initiateBattle (activePlayer.tile, activeEnemy.tile);
					ResetEnemyPar ();
					activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
					activeEnemy = null;

					if (activePlayer.GetComponentInChildren<Stats> ().cHP <= 0) {
						activePlayer.Status = 4;
					} else {
						if (activePlayer.GetComponentInChildren<Stats> ().Xp >= 100) {
							float[] lvStats = new float[8];
							string className = "";
							foreach (Transform child in activePlayer.transform) {
								if (child.tag == "Class") {
									className = child.name;
									break;
								}
							}

							lvStats = xpGrowthRate.GetGrowthRates (className);
							activePlayer.GetComponentInChildren<Stats> ().LevelUp (lvStats);
						}
						activePlayer.Status = 0;
						activePlayer = null;
					}
				}
			}

			bool all_done = true;
			for (int i = 0; i < playerL.Count; i++) {
				if (playerL [i].Status != 0) {
					all_done = false;
					break;
				}
			}

			if (all_done) {
				State = 3;
				activePlayer = null;

				foreach (Enemy enemy in enemyL) {
					enemy.Active = true;
					enemy.ResetMovement ();
				}
			}
		} else if (State == 2) {
			bool all_done = true;

			for (int i = 0; i < enemyL.Count; i++) {
				if (enemyL [i].Active) {
					bool aEnemyMoving = false;
					for (int j = 0; j < enemyL.Count; j++) {
						if (enemyL [j].moving) {
							aEnemyMoving = true;
						}
					}

					if (!aEnemyMoving) {
						//enemyL [i].MoveEnemy ();
					}
				}
					
				if (enemyL [i].Active || enemyL [i].moving) {
					//all_done = false;
				}
			}

			if (all_done) {
				State = 4;

				foreach (Unit player in playerL) {
					player.Status = 1;
					player.ResetMovement ();
				}
			}
		} else if (State == 3) {
			StartCoroutine (TimerEnumerator(3,2));
		} else if (State == 4) {
			StartCoroutine (TimerEnumerator(3,1));
		}

		if (activePlayer != null) {
			GameObject camera = GameObject.Find ("Main Camera");
			camera.transform.position = new Vector3 (activePlayer.transform.position.x, activePlayer.transform.position.y + 5, activePlayer.transform.position.z - 5);
		} else {
			playerInput.CameraAction ();
		}
	}
		
	void PlayerSelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Unit")) {
					if (activePlayer != null && activePlayer != hit.collider.gameObject.GetComponent<Unit>()) {
						activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
					}
					activePlayer = hit.collider.gameObject.GetComponent<Unit>();
				}
				if (hit.collider.tag.Equals ("Terrain") && activePlayer != null) {
					ResetTilePar ();
					activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
					activePlayer = null;
				}
			}
		}
	}

	void EnemySelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Enemy")) {
					if (hit.collider.gameObject.transform.Find ("Particle").gameObject.activeSelf) {
						activeEnemy = hit.collider.gameObject.GetComponent<Enemy> ();
					}
				}
				if (hit.collider.tag.Equals ("Terrain") || hit.collider.tag.Equals ("Unit")) {
					activePlayer.Status = 1;
					activePlayer.possibleMoves ();
					ResetEnemyPar ();
				}
			}
		}
	}

	void InteractSelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Unit") && hit.collider.gameObject != activePlayer.gameObject) {
					if (hit.collider.gameObject.transform.Find ("Particle").gameObject.activeSelf) {
						interactPlayer = hit.collider.gameObject.GetComponent<Unit> ();
					}
				}
				if (hit.collider.tag.Equals ("Terrain") || hit.collider.tag.Equals ("Enemy")) {
					activePlayer.Status = 1;
					activePlayer.possibleMoves ();
					ResetPlayerPar ();
				}
			}
		}
	}

	private void LoadLists() {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("GridTile");
		foreach (GameObject tile in tiles) {
			HexTile tempTile = tile.GetComponent<HexTile> ();
			tempTile.FindNeighbors ();
			tileL.Add (tempTile);	
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			enemyL.Add (enemy.GetComponent<Enemy> ());
		}

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Unit");
		foreach (GameObject player in players) {
			if (!playerL.Contains (player.GetComponent<Unit> ())) {
				playerL.Add (player.GetComponent<Unit> ());
			}
		}

		foreach (HexTile tile in tileL) {
			if (tile.GetComponent<HexTile> ().SpawnP == 1) {
				foreach (Unit player in playerL) {
					if (player.GetComponent<Unit> ().tile == null && tile.GetComponent<HexTile> ().character == null) {
						tile.GetComponent<HexTile> ().character = player.gameObject;
						player.GetComponent<Unit> ().tile = tile;
						player.GetComponent<Unit> ().InitPosition ();
						player.GetComponent<Unit>().Status = 1;
					}
				}
			} else if (tile.GetComponent<HexTile> ().SpawnP == 2) {
				foreach (Enemy enemy in enemyL) {
					if (enemy.GetComponent<Enemy> ().tile == null && tile.GetComponent<HexTile> ().character == null) {
						tile.GetComponent<HexTile> ().character = enemy.gameObject;
						enemy.GetComponent<Enemy> ().tile = tile;
						enemy.GetComponent<Enemy> ().InitPosition ();
					}
				}
			}
		}
	}

	public void AddPlayer(Unit player) {
		playerL.Add (player);
	}

	public void ResetTileDis() {
		foreach (HexTile tile in tileL) {
			tile.dis = -1;
		}
	}

	public void ResetTilePar() {
		foreach (HexTile tile in tileL) {
			tile.transform.Find ("Possible_Move").gameObject.SetActive (false);
		}
	}

	private void ResetEnemyPar() {
		foreach (Enemy enemy in enemyL) {
			enemy.transform.Find ("Particle").gameObject.SetActive (false);
		}
	}

	private void ResetPlayerPar() {
		foreach (Unit unit in playerL) {
			unit.GetComponentInChildren<ParticleSystem> ().Stop (true);
		}
	}

	IEnumerator TimerEnumerator(float secs, int nextState) {
		State = 0;
		yield return new WaitForSeconds (secs);
		State = nextState;
	}

	private void initiateInteraction (HexTile init, HexTile rec) {
		Weapon iWep = init.character.GetComponentInChildren<Weapon> ();
		Stats iStat = init.character.GetComponentInChildren<Stats> ();
		Stats rStat = rec.character.GetComponentInChildren<Stats> ();

		if (iWep.type == -1) {
			rStat.cHP += 10 + iStat.Mag;
			if (rStat.cHP > rStat.mHP) {
				rStat.cHP = rStat.mHP;
			}
			iStat.Xp += 11;
		}
	}

	private void initiateBattle (HexTile attacker, HexTile defender) {
		Weapon aWep = attacker.character.GetComponentInChildren<Weapon> ();
		Weapon dWep = defender.character.GetComponentInChildren<Weapon> ();
		Stats aStat = attacker.character.GetComponentInChildren<Stats> ();
		Stats dStat = defender.character.GetComponentInChildren<Stats> ();

		float a_as, d_as;
		if (aWep.Wt <= aStat.Str) {
			a_as = aStat.Spd;
		} else {
			a_as = aStat.Spd - (aWep.Wt - aStat.Str);
		}

		if (dWep.Wt <= dStat.Str) {
			d_as = dStat.Spd;
		} else {
			d_as = dStat.Spd - (dWep.Wt - dStat.Str);
		}
		 
		int[] repAtt = {0,0};
		if (a_as - d_as >= 4) {
			repAtt [0] = 1;
		} else if (d_as - a_as >= 4) {
			repAtt [1] = 1;
		}

		float a_hr, d_hr;
		a_hr = aWep.Hit + aStat.Skl * 2 + aStat.Lck /*+ Support Bonus + Biorythym Bonus*/;
		d_hr = dWep.Hit + dStat.Skl * 2 + aStat.Lck /*+ Support Bonus + Biorythym Bonus*/;

		float a_ev, d_ev;
		a_ev = a_as + aStat.Lck + attacker.terrainBonus /*+ Support Bonus + Biorythym Bonus*/;
		d_ev = d_as + dStat.Lck + defender.terrainBonus /*+ Support Bonus + Biorythym Bonus*/;

		float a_ac, d_ac;
		a_ac = a_hr - d_ev;
		if (a_ac < 0) {
			a_ac = 0;
		} else if (a_ac > 100) {
			a_ac = 100;
		}
		d_ac = d_hr - a_ev;
		if (d_ac < 0) {
			d_ac = 0;
		} else if (d_ac > 100) {
			d_ac = 100;
		}

		float a_ap, d_ap;
		if (aWep.type%2 == 0) {
			a_ap = aStat.Str + (aWep.Mt);
		} else {
			a_ap = aStat.Mag + (aWep.Mt);
		}
		if (dWep.type%2 == 0) {
			d_ap = dStat.Str + (dWep.Mt);
		} else {
			d_ap = dStat.Mag + (dWep.Mt);
		}

		float a_dp, d_dp;
		if (dWep.type%2 == 0) {
			a_dp = attacker.terrainBonus + aStat.Def;
		} else {
			a_dp = attacker.terrainBonus + aStat.Res;
		}
		if (aWep.type%2 == 0) {
			d_dp = defender.terrainBonus + dStat.Def;
		} else {
			d_dp = defender.terrainBonus + dStat.Res;
		}

		float a_dm, d_dm;
		a_dm = a_ap - d_dp;
		d_dm = d_ap - a_dp;
		if (a_dm < 0) {
			a_dm = 0;
		}
		if (d_dm < 0) {
			d_dm = 0;
		}

		float a_cr, d_cr;
		a_cr = aWep.Crit + aStat.Skl / 2 /*+ Bond Bonus + Class Critical*/;
		d_cr = dWep.Crit + dStat.Skl / 2 /*+ Bond Bonus + Class Critical*/;

		float a_ce, d_ce;
		a_ce = aStat.Lck /*+ Bond Bonus*/;
		d_ce = dStat.Lck /*+ Bond Bonus*/;

		float a_cc, d_cc;
		a_cc = a_cr - d_ce;
		d_cc = d_cr - a_ce;

		float a_xp, d_xp;

		float x = Random.Range(0,100);
		if (a_cc>=x) {
			a_dm = a_dm * 3;
		}
		if (a_ac >= x) {
			if (a_dm == 0) {
				a_xp = 1;
			} else {
				float xp = 10 + (dStat.Lv - aStat.Lv) / 2;
				if (xp < 1) {
					xp = 1;
				}
				a_xp = xp;
			}
			dStat.cHP -= a_dm;
		} else {
			a_xp = 1;
		}

		if (dStat.cHP <= 0) {
			a_xp += (dStat.Lv - aStat.Lv) + 15 + 5;
			if (defender.character.CompareTag ("Enemy")) {
				a_xp += 40 * defender.character.GetComponent<Enemy> ().special;
			}
			UpdateXp (aStat,a_xp,dStat,0);
			return;
		}

		x = Random.Range(0,100);
		if (d_cc>=x) {
			d_dm = d_dm * 3;
		}
		if (d_ac >= x && dWep.Rng.Contains (defender.dis) && dWep.type >= 0) {
			if (d_dm == 0) {
				d_xp = 1;
			} else {
				float xp = 10 + (aStat.Lv - dStat.Lv) / 2;
				if (xp < 1) {
					xp = 1;
				}
				d_xp = xp;
			}
			aStat.cHP -= d_dm;
		} else {
			d_xp = 1;
		}

		if (aStat.cHP <= 0) {
			d_xp += (aStat.Lv - dStat.Lv) + 15 + 5;
			if (attacker.character.CompareTag ("Enemy")) {
				d_xp += 40 * defender.character.GetComponent<Enemy> ().special;
			}
			UpdateXp (aStat,a_xp,dStat,d_xp);
			return;
		}

		if (repAtt[0] == 1) {
			x = Random.Range(0,100);
			if (a_cc>=x) {
				a_dm = a_dm * 3;
			}
			if (a_ac >= x) {
				if (a_dm == 0) {
					a_xp += 1;
				} else {
					float xp = 10 + (dStat.Lv - aStat.Lv) / 2;
					if (xp < 1) {
						xp = 1;
					}
					a_xp += xp;
				}
				dStat.cHP -= a_dm;
			} else {
				a_xp += 1;
			}

			if (dStat.cHP <= 0) {
				a_xp += (dStat.Lv - aStat.Lv) + 15 + 5;
				if (defender.character.CompareTag ("Enemy")) {
					a_xp += 40 * defender.character.GetComponent<Enemy> ().special;
				}
				UpdateXp (aStat,a_xp,dStat,d_xp);
				return;
			}
		} else if (repAtt[1] == 1) {
			x = Random.Range(0,100);
			if (d_cc>=x) {
				d_dm = d_dm * 3;
			}
			if (d_ac >= x && dWep.Rng.Contains (defender.dis) && dWep.type >= 0) {
				if (d_dm == 0) {
					d_xp += 1;
				} else {
					float xp = 10 + (aStat.Lv - dStat.Lv) / 2;
					if (xp < 1) {
						xp = 1;
					}
					d_xp += xp;
				}
				aStat.cHP -= d_dm;
			} else {
				d_xp += 1;
			}

			if (aStat.cHP <= 0) {
				d_xp += (aStat.Lv - dStat.Lv) + 15 + 5;
				if (attacker.character.CompareTag ("Enemy")) {
					d_xp += 40 * defender.character.GetComponent<Enemy> ().special;
				}
				UpdateXp (aStat,a_xp,dStat,d_xp);
				return;
			}
		}

		UpdateXp (aStat,a_xp,dStat,d_xp);
	}

	private void UpdateXp(Stats aStat, float a_xp, Stats dStat, float d_xp) {
		aStat.Xp += a_xp;
		dStat.Xp += d_xp;
	}
}
