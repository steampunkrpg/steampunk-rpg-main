using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float turnDelay = 0.1f;
	private PlayerKeyBoardInput playerInput;

	public GameObject LevelHUD;

	private Ray mouseRay;
	private RaycastHit hit;

	public List<HexTile> tileL;
	public List<Unit> playerL;
	public List<Enemy> enemyL;
	public Unit activePlayer = null;
	public Enemy activeEnemy = null;

	public bool playersTurn;
	private bool enemiesTurn;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		tileL = new List<HexTile> ();
		playerL = new List<Unit> ();
		enemyL = new List<Enemy> ();

		playerInput = this.gameObject.GetComponent<PlayerKeyBoardInput> ();

		playersTurn = false;
		enemiesTurn = false;
	}

	public void InitGame() {
		enemyL = new List<Enemy> ();
		tileL = new List<HexTile> ();

		LevelHUD.SetActive (true);

		LoadLists ();
		playersTurn = true;
	}

	void Update() {
		if (playersTurn) {
			PlayerSelect ();

			if (activePlayer != null && activePlayer.Status == 1) {
				if (!activePlayer.GetComponentInChildren<ParticleSystem> ().isPlaying) {
					activePlayer.GetComponentInChildren<ParticleSystem> ().Play (true);
					activePlayer.possibleMoves ();
				}
			} else if (activePlayer != null && activePlayer.Status == 0) {
				activePlayer.GetComponentInChildren<ParticleSystem> ().Stop(true);
			}

			if (activePlayer != null && activePlayer.Status == 1) {
				playerInput.ProvideAction (activePlayer);
			}

			if (activePlayer != null && activePlayer.Status == 3) {
				EnemySelect ();
				if (activeEnemy != null) {
					initiateBattle (activePlayer.tile, activeEnemy.tile);
					activeEnemy = null;
					activePlayer.Status = 0;
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
				playersTurn = false;
				enemiesTurn = true;
				activePlayer = null;

				foreach (Enemy enemy in enemyL) {
					enemy.Active = true;
					enemy.ResetMovement ();
				}
			}
		} else if (enemiesTurn) {
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
						enemyL [i].MoveEnemy ();
					}
				}
					
				if (enemyL [i].Active || enemyL [i].moving) {
					all_done = false;
				}
			}

			if (all_done) {
				playersTurn = true;
				enemiesTurn = false;

				foreach (Unit player in playerL) {
					player.Status = 1;
					player.ResetMovement ();
				}
			}
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
				if (hit.collider.tag.Equals ("Terrain")) {
					activePlayer.Status = 1;
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

	private void initiateBattle (HexTile attacker, HexTile defender) {

	}
}
