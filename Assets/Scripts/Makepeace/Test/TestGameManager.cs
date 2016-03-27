using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestGameManager : MonoBehaviour {

	public static TestGameManager instanceT = null;
	public float turnDelay = 0.1f;
	private TestPlayerKeyBoardInput playerInput;

	private Ray mouseRay;
	private RaycastHit hit;

	public List<TestHexTile> tileL;
	public List<TestUnit> playerL;
	public List<Enemy> enemyL;
	public TestUnit activePlayer;

	public bool playersTurn;
	private bool enemiesTurn;

	void Awake() {
		if (instanceT == null)
			instanceT = this;
		else if (instanceT != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		tileL = new List<TestHexTile> ();
		playerL = new List<TestUnit> ();
		enemyL = new List<Enemy> ();

		playerInput = this.gameObject.GetComponent<TestPlayerKeyBoardInput> ();

		playersTurn = false;
		enemiesTurn = false;
	}

	public void InitGame() {
		enemyL = new List<Enemy> ();
		tileL = new List<TestHexTile> ();

		LoadLists ();
		playersTurn = true;
	}

	void Update() {
		if (playersTurn) {
			PlayerSelect ();
			if (activePlayer != null && activePlayer.Active) {
				if (!activePlayer.GetComponentInChildren<ParticleSystem> ().isPlaying) {
					activePlayer.GetComponentInChildren<ParticleSystem> ().Play (true);
					activePlayer.possibleMoves ();
				}
			} else if (activePlayer != null && !activePlayer.Active && !activePlayer.moving) {
				activePlayer.GetComponentInChildren<ParticleSystem> ().Stop(true);
			}

			if (activePlayer != null && activePlayer.Active && !activePlayer.moving) {
				playerInput.ProvideAction (activePlayer);
			}

			bool all_done = true;
			for (int i = 0; i < playerL.Count; i++) {
				if (playerL [i].Active || playerL [i].moving) {
					all_done = false;
					break;
				}
			}

			if (all_done) {
				playersTurn = false;
				enemiesTurn = true;
				activePlayer = null;

				foreach (Enemy enemy in enemyL) {
					enemy.Status = 1;
					enemy.ResetMovement ();
				}
			}
		} else if (enemiesTurn) {
			bool all_done = true;

			/*for (int i = 0; i < enemyL.Count; i++) {
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

				foreach (TestUnit player in playerL) {
					player.Active = true;
					player.ResetMovement ();
				}
			}*/
		} 
	}
		
	void PlayerSelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Unit")) {
					if (activePlayer != null && activePlayer != hit.collider.gameObject.GetComponent<TestUnit>()) {
						activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
					}
					activePlayer = hit.collider.gameObject.GetComponent<TestUnit>();
				}
			}
		}
		}

	private void LoadLists() {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("GridTile");
		foreach (GameObject tile in tiles) {
			TestHexTile tempTile = tile.GetComponent<TestHexTile> ();
			tempTile.FindNeighbors ();
			tileL.Add (tempTile);	
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			enemyL.Add (enemy.GetComponent<Enemy> ());
		}

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Unit");
		foreach (GameObject player in players) {
			if (!playerL.Contains (player.GetComponent<TestUnit> ())) {
				playerL.Add (player.GetComponent<TestUnit> ());
			}
		}

		foreach (TestHexTile tile in tileL) {
			if (tile.GetComponent<TestHexTile> ().SpawnP == 1) {
				foreach (TestUnit player in playerL) {
					if (player.GetComponent<TestUnit> ().tile == null && tile.GetComponent<TestHexTile> ().character == null) {
						tile.GetComponent<TestHexTile> ().character = player.gameObject;
						player.GetComponent<TestUnit> ().tile = tile;
						player.GetComponent<TestUnit> ().InitPosition ();
						player.GetComponent<TestUnit>().Active = true;
					}
				}
			} else if (tile.GetComponent<TestHexTile> ().SpawnP == 2) {
				foreach (Enemy enemy in enemyL) {
					if (enemy.GetComponent<Enemy> ().tile == null && tile.GetComponent<HexTile> ().character == null) {
						tile.GetComponent<TestHexTile> ().character = enemy.gameObject;
						//enemy.GetComponent<Enemy> ().tile = tile;
						enemy.GetComponent<Enemy> ().InitPosition ();
					}
				}
			}
		}
	}

	public void AddPlayer(TestUnit player) {
		playerL.Add (player);
	}

	public void ResetTileDis() {
		foreach (TestHexTile tile in tileL) {
			tile.dis = -1;
		}
	}

	public void ResetTilePar() {
		foreach (TestHexTile tile in tileL) {
			tile.transform.Find ("Possible_Move").gameObject.SetActive (false);
		}
	}
}
