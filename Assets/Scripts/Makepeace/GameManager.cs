using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public GameObject PauseUI;
	public GameObject TurnUI;
	public Text turnText;
	public Image turnImage;
	public GameObject StatsUI;
	public GameObject InvUI;
	public GameObject PlayerUI;
	public GameObject EnemyUI;

	public static GameManager instance = null;
	public float turnDelay = 0.1f;
	private PlayerKeyBoardInput playerInput;
	private XpGrowthRate xpGrowthRate;

	private AsyncOperation async;

	private Ray mouseRay;
	private RaycastHit hit;
	private RaycastHit vertHit;

	public List<HexTile> tileL;
	public List<Unit> playerL;
	public List<Enemy> enemyL;
	public Unit activePlayer = null;
	public Enemy activeEnemy = null;
	public Unit interactPlayer = null;

	public int[] battleAnimation;

	public int State;
	public int prevState;
	public int level;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		tileL = new List<HexTile> ();
		playerL = new List<Unit> ();
		enemyL = new List<Enemy> ();
		battleAnimation = new int[10];

		xpGrowthRate = this.gameObject.GetComponent<XpGrowthRate> ();
		playerInput = this.gameObject.GetComponent<PlayerKeyBoardInput> ();

		PauseUI.SetActive (false);
		TurnUI.SetActive (false);
		StatsUI.SetActive (false);
		InvUI.GetComponent<InventoryManager> ().CreateDefault ();
		InvUI.SetActive (false);

		level = 0;
		State = 0;
	}

	public void InitGame() {
		enemyL = new List<Enemy> ();
		tileL = new List<HexTile> ();

		activePlayer = null;
		activeEnemy = null;
		LoadLists ();
		State = 4;
	}

	void Update() {
		if (State == 1) {
			if (activePlayer == null || activePlayer.Status == 1) {
				PlayerSelect ();
			}

			if (activePlayer != null && activePlayer.Status == 1) {
				if (!activePlayer.GetComponentInChildren<ParticleSystem> ().isPlaying) {
					activePlayer.GetComponentInChildren<ParticleSystem> ().Play (true);
				}
				playerInput.UnitAction (activePlayer);
			}

			if (activePlayer != null && activePlayer.Status == 0) {
				activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
				PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
				ResetTileParticles ();
				activePlayer = null;
			}

			if (activePlayer != null && activePlayer.Status == 6) {
				MoveSelect ();
			}

			if (activePlayer != null && activePlayer.Status == 5) {
				InteractSelect ();
				if (interactPlayer != null) {
					InitiateInteraction (activePlayer.tile, interactPlayer.tile);
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
					InitiateBattle (activePlayer.tile, activeEnemy.tile);
					ResetEnemyPar ();
					activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
					prevState = 1;
					State = 0;

					//Call Battle Animation Scene
					return;
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
					enemy.Status = 1;
					enemy.ResetMovement ();
				}
			}
		} else if (State == 2) {
			for (int i = 0; i < enemyL.Count; i++) {
				if (enemyL [i].Status == 2) {
					break;
				} else if (enemyL [i].Status == 1) {
					enemyL [i].MoveEnemy ();
					if (State == 0) {
						return;
					}
					break;
				}
			}

			bool all_done = true;
			for (int i = 0; i < enemyL.Count; i++) {
				if (enemyL [i].Status != 0) {
					all_done = false;
					break;
				}
			}

			if (all_done) {
				State = 4;
				activeEnemy = null;

				foreach (Unit player in playerL) {
					player.Status = 1;
					player.ResetMovement ();
				}
			}
		} else if (State == 3) {
			StartCoroutine (TimerEnumerator (3, 2));
		} else if (State == 4) {
			StartCoroutine (TimerEnumerator (3, 1));
		} 

		if (State == 5) {
			battleAnimation = new int[9];
			CheckForDeaths ();

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

			CheckWinOrLoseCondition ();

			activePlayer.Status = 0;
			activeEnemy.Status = 0;
			activePlayer = null;
			activeEnemy = null;

			State = prevState;
		}

		if (State != 0 && State != 3 && State != 4) {
			if (activePlayer != null) {
				playerInput.CameraAction ();
				GameObject camera = GameObject.Find ("Main Camera");
				CameraBounds bounds = camera.GetComponent<CameraBounds> ();
				camera.transform.position = new Vector3 (activePlayer.transform.position.x, activePlayer.transform.position.y + bounds.offset - bounds.zoom, activePlayer.transform.position.z - bounds.offset + bounds.zoom);
			} else if (activeEnemy != null) {
				playerInput.CameraAction ();
				GameObject camera = GameObject.Find ("Main Camera");
				CameraBounds bounds = camera.GetComponent<CameraBounds> ();
				camera.transform.position = new Vector3 (activeEnemy.transform.position.x, activeEnemy.transform.position.y + bounds.offset - bounds.zoom, activeEnemy.transform.position.z - bounds.offset + bounds.zoom);
			} else {
				playerInput.CameraAction ();
			}

			playerInput.GlobalAction ();
		}
	}

	void CameraFocusPlayer() {
		if (playerL.Count > 0) {
			GameObject camera = GameObject.Find ("Main Camera");
			CameraBounds bounds = camera.GetComponent<CameraBounds> ();
			camera.transform.position = new Vector3 (playerL[0].transform.position.x, playerL[0].transform.position.y + bounds.offset - bounds.zoom, playerL[0].transform.position.z - bounds.offset + bounds.zoom);
		}
	}

	void PlayerSelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Unit")) {
					if (activePlayer != null && activePlayer != hit.collider.gameObject.GetComponent<Unit> ()) {
						activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
					}

					if (activeEnemy != null) {
						activeEnemy = null;
						EnemyUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
					}

					if (activePlayer == null) {
						PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
					}

					activePlayer = hit.collider.gameObject.GetComponent<Unit> ();
					PlayerUI.GetComponent<PlayerUI> ().UpdateUI (activePlayer.char_stats);
				} else if (hit.collider.tag.Equals ("Enemy")) {
					if (activePlayer != null) {
						activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
						activePlayer = null;
						PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
					}

					if (activeEnemy == null) {
						EnemyUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
					}

					activeEnemy = hit.collider.gameObject.GetComponent<Enemy> ();
					EnemyUI.GetComponent<EnemyUI> ().UpdateUI (activeEnemy.enemy_stats);
				}

				if (hit.collider.tag.Equals ("Terrain") && (activePlayer != null || activeEnemy != null) && !EventSystem.current.IsPointerOverGameObject()) {
					if (activePlayer != null) {
						activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
						PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
						activePlayer = null;
					} else if (activeEnemy != null) {
						EnemyUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
						activeEnemy = null;
					}
				}
			}
		} else if (Input.GetMouseButtonDown (1)) {
			if (activePlayer != null) {
				activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
				PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
				activePlayer = null;
			} else if (activeEnemy != null) {
				EnemyUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
				activeEnemy = null;
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
			}
		} else if (Input.GetMouseButtonDown (1)) {
			ResetEnemyPar ();
			PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
			ResetTileParticles ();
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
			}
		} else if (Input.GetMouseButtonDown (1)) {
			ResetPlayerPar ();
			PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
			ResetTileParticles ();
		}
	}

	void MoveSelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Terrain")) {
					if (Physics.Raycast (hit.point, Vector3.down, out vertHit)) {
						Debug.DrawLine (hit.point, vertHit.point);
						if (vertHit.collider.tag.Equals ("GridTile")) {
							GameObject tile = vertHit.collider.gameObject;

							if (tile.transform.Find ("Possible_Move").gameObject.activeSelf) {
								activePlayer.MoveTo (tile.GetComponent<HexTile> ());
								Debug.Log ("Possible Move: " + vertHit.collider.gameObject.GetComponent<HexTile> ());
							}
						}
					}
				}
			}
		} else if (Input.GetMouseButtonDown (1)) {
			ResetTileParticles ();
			PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
		}
	}

	private void LoadLists() {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("GridTile");
		foreach (GameObject tile in tiles) {
			HexTile tempTile = tile.GetComponent<HexTile> ();
			tempTile.FindNeighbors ();
			tempTile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Stop (true);
			tileL.Add (tempTile);	
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			enemyL.Add (enemy.GetComponent<Enemy> ());
		}

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Unit");
		foreach (GameObject player in players) {
			if (!playerL.Contains (player.GetComponent<Unit> ())) {
				player.GetComponent<Unit> ().DontDestroy ();
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

	public void ResetTileMovDis() {
		foreach (HexTile tile in tileL) {
			tile.mov_dis = -1;
		}
	}

	public void ResetTileAttDis() {
		foreach (HexTile tile in tileL) {
			tile.att_dis = -1;
		}
	}

	public void ResetTileParticles() {
		foreach (HexTile tile in tileL) {
			tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem> ().Stop ();
			tile.transform.Find ("Possible_Move").GetComponent<ParticleSystem>().startColor = new Color32(0,120,255,255);
		}
	}

	public void ResetTileParent() {
		foreach (HexTile tile in tileL) {
			tile.parent = null;
		}
	}

	private void ResetEnemyPar() {
		foreach (Enemy enemy in enemyL) {
			enemy.GetComponentInChildren<ParticleSystem> ().Stop (true);
		}
	}

	private void ResetPlayerPar() {
		foreach (Unit unit in playerL) {
			unit.GetComponentInChildren<ParticleSystem> ().Stop (true);
			unit.GetComponentInChildren<ParticleSystem> ().startColor = new Color (255, 255, 255);
		}
	}

	IEnumerator TimerEnumerator(float secs, int nextState) {
		State = 0;

		TurnUI.SetActive (true);
		if (nextState == 1) {
			turnImage.color = new Color32(0,255,100,255);
			turnText.text = "PLAYER'S TURN";
		} else if (nextState == 2) {
			turnImage.color = new Color32(255,0,100,255);
			turnText.text = "ENEMY'S TURN";
		} else if (nextState == -1) {
			turnImage.color = new Color32(37,38,38,123);
			turnText.text = "GAME OVER";
		} else if (nextState == -2) {
			turnImage.color = new Color32(37,38,38,123);
			turnText.text = "VICTORY";
		}

		yield return new WaitForSeconds (secs);

		TurnUI.SetActive(false);
		if (nextState == 1) {
			CameraFocusPlayer ();
		} else if (nextState == -1) {
			LoadScene ("New_Menu_Scene");
		} else if (nextState == -2) {
			LoadScene ("World_Map");
		}

		State = nextState;
	}

	private void InitiateInteraction (HexTile init, HexTile rec) {
		Weapon iWep = init.character.GetComponentInChildren<Weapon> ();
		Stats iStat = init.character.GetComponentInChildren<Stats> ();
		Stats rStat = rec.character.GetComponentInChildren<Stats> ();

		if (iWep.type%2==1) {
			rStat.cHP += iWep.Mt + iStat.Mag;
			if (rStat.cHP > rStat.mHP) {
				rStat.cHP = rStat.mHP;
			}
			iStat.Xp += iWep.Mt;
		}
	}

	public void InitiateBattle (HexTile attacker, HexTile defender) {
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
		a_hr = aWep.Hit + aStat.Skl * 2 + aStat.Lck;
		d_hr = dWep.Hit + dStat.Skl * 2 + aStat.Lck;

		float a_ev, d_ev;
		a_ev = a_as + aStat.Lck + attacker.terrainBonus;
		d_ev = d_as + dStat.Lck + defender.terrainBonus;

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
		a_cr = aWep.Crit + aStat.Skl / 2;
		d_cr = dWep.Crit + dStat.Skl / 2;

		float a_ce, d_ce;
		a_ce = aStat.Lck;
		d_ce = dStat.Lck;

		float a_cc, d_cc;
		a_cc = a_cr - d_ce;
		d_cc = d_cr - a_ce;

		float a_xp, d_xp;

		if (attacker.character.tag == "Unit") {
			battleAnimation [0] = 1;
		} else {
			battleAnimation [0] = 2;
		}

		float x = Random.Range(0,100);
		if (a_cc>=x) {
			a_dm = a_dm * 3;
		}
		if (a_ac >= x) {
			battleAnimation [1] = 0;

			if (a_dm == 0) {
				a_xp = 1;
			} else {
				float xp = 10 + (dStat.Lv - aStat.Lv) / 2;
				if (xp < 1) {
					xp = 1;
				}
				a_xp = xp;
			}

			battleAnimation [2] = a_dm;
			dStat.cHP -= a_dm;
		} else {
			battleAnimation [1] = 0;
			a_xp = 1;
		}

		if (dStat.cHP <= 0) {
			battleAnimation [3] = -1;

			a_xp += (dStat.Lv - aStat.Lv) + 15 + 5;
			if (defender.character.CompareTag ("Enemy")) {
				a_xp += 40 * defender.character.GetComponent<Enemy> ().special;
			}
			UpdateXp (aStat,a_xp,dStat,0);
			return;
		}

		if (dWep.Rng.Contains (defender.att_dis)) {
			if (attacker.character.tag == "Unit") {
				battleAnimation [3] = 1;
			} else {
				battleAnimation [3] = 2;
			}
		} else {
			battleAnimation [3] = 0;
		}

		x = Random.Range(0,100);
		if (d_cc>=x) {
			d_dm = d_dm * 3;
		}
		if (d_ac >= x && dWep.Rng.Contains (defender.att_dis) && dWep.type >= 0) {
			battleAnimation [4] = 1;

			if (d_dm == 0) {
				d_xp = 1;
			} else {
				float xp = 10 + (aStat.Lv - dStat.Lv) / 2;
				if (xp < 1) {
					xp = 1;
				}
				d_xp = xp;
			}

			battleAnimation [5] = d_dm;
			aStat.cHP -= d_dm;
		} else {
			battleAnimation [4] = 0;
			d_xp = 1;
		}

		if (aStat.cHP <= 0) {
			battleAnimation [6] = -1;
			d_xp += (aStat.Lv - dStat.Lv) + 15 + 5;
			if (attacker.character.CompareTag ("Enemy")) {
				d_xp += 40 * attacker.character.GetComponent<Enemy> ().special;
			}
			UpdateXp (aStat,a_xp,dStat,d_xp);
			return;
		}

		int offset = 3;
		if (battleAnimation [3] == 0) {
			offset = 0;
		}

		if (repAtt[0] == 1) {
			if (attacker.character.tag == "Unit") {
				battleAnimation [3+offset] = 1;
			} else {
				battleAnimation [3+offset] = 2;
			}

			x = Random.Range(0,100);
			if (a_cc>=x) {
				a_dm = a_dm * 3;
			}
			if (a_ac >= x) {
				battleAnimation [4 + offset] = 1;

				if (a_dm == 0) {
					a_xp += 1;
				} else {
					float xp = 10 + (dStat.Lv - aStat.Lv) / 2;
					if (xp < 1) {
						xp = 1;
					}
					a_xp += xp;
				}

				battleAnimation [5 + offset] = a_dm;
				dStat.cHP -= a_dm;
			} else {
				battleAnimation [4 + offset] = 0;
				a_xp += 1;
			}

			if (dStat.cHP <= 0) {
				battleAnimation [6 + offset] = -1;
				a_xp += (dStat.Lv - aStat.Lv) + 15 + 5;
				if (defender.character.CompareTag ("Enemy")) {
					a_xp += 40 * defender.character.GetComponent<Enemy> ().special;
				}
				UpdateXp (aStat,a_xp,dStat,d_xp);
				return;
			}
		} else if (repAtt[1] == 1) {
			if (dWep.Rng.Contains (defender.att_dis)) {
				if (attacker.character.tag == "Unit") {
					battleAnimation [3 + offset] = 1;
				} else {
					battleAnimation [3 + offset] = 2;
				}
			} else {
				battleAnimation [3 + offset] = 0;
			}

			x = Random.Range(0,100);
			if (d_cc>=x) {
				d_dm = d_dm * 3;
			}
			if (d_ac >= x && dWep.Rng.Contains (defender.att_dis) && dWep.type >= 0) {
				battleAnimation [4 + offset] = 1;

				if (d_dm == 0) {
					d_xp += 1;
				} else {
					float xp = 10 + (aStat.Lv - dStat.Lv) / 2;
					if (xp < 1) {
						xp = 1;
					}
					d_xp += xp;
				}

				battleAnimation [5 + offset] = d_dm;
				aStat.cHP -= d_dm;
			} else {
				battleAnimation [4 + offset] = 0;
				d_xp += 1;
			}

			if (aStat.cHP <= 0) {
				battleAnimation [6 + offset] = -1;
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

	private void CheckForDeaths() {
		if (activePlayer.char_stats.cHP <= 0) {
			activePlayer.Death ();
		}

		if (activeEnemy.enemy_stats.cHP <= 0) {
			activeEnemy.Death ();
		}

		CheckWinOrLoseCondition ();
	}

	private void CheckWinOrLoseCondition() {
		if (playerL.Count == 0) {
			StartCoroutine(TimerEnumerator(5,-1));
		}

		if (enemyL.Count == 0) {
			level++;
			StartCoroutine(TimerEnumerator(5,-2));
		}
	}

	public void LoadScene(string scene) 
	{
		StartCoroutine(LoadLevelWithBar(scene));
	}

	IEnumerator LoadLevelWithBar (string sceneName)
	{
		async = SceneManager.LoadSceneAsync(sceneName);
		while (!async.isDone)
		{
			yield return null;
		}
	}

	public void DestroyThis () {
		foreach (Unit player in playerL) {
			player.Death ();
		}
		Destroy (this.gameObject);
	}
}
