using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float turnDelay = 0.1f;
	private PlayerKeyBoardInput playerInput;

	private Ray mouseRay;
	private RaycastHit hit;

	public List<HexTile> tileL;
	public List<Unit> playerL;
	public List<Enemy> enemyL;
	public Unit activePlayer;

	public bool playersTurn = true;
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
	}

	public void InitGame() {
		enemyL = new List<Enemy> ();
		tileL = new List<HexTile> ();

		LoadLists ();
		playersTurn = true;
	}

	void Update() {
		if (playersTurn) {
			PlayerSelect ();
			if (activePlayer != null) {
				playerInput.ProvideAction (activePlayer);
			}

			bool all_done = true;
			for (int i = 0; i < playerL.Count; i++) {
				if (playerL[i].Active) {
					all_done = false;
					break;
				}
			}

			if (all_done) {
				playersTurn = false;
				enemiesTurn = true;
			}
		}

		//StartCoroutine (MoveEnemies ());
	}
		
	void PlayerSelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Unit")) {
					activePlayer = hit.collider.gameObject.GetComponent<Unit>();
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
			playerL.Add (player.GetComponent<Unit> ());
		}

		foreach (HexTile tile in tileL) {
			if (tile.GetComponent<HexTile> ().SpawnP == 1) {
				foreach (Unit player in playerL) {
					if (player.GetComponent<Unit> ().tile == null) {
						tile.GetComponent<HexTile> ().character = player.gameObject;
						player.GetComponent<Unit> ().tile = tile;
						player.GetComponent<Unit> ().InitPosition ();
						player.GetComponent<Unit>().Active = true;
					}
				}
			} else if (tile.GetComponent<HexTile> ().SpawnP == 2) {
				foreach (Enemy enemy in enemyL) {
					if (enemy.GetComponent<Enemy> ().tile == null) {
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

	IEnumerator MoveEnemies() {
		enemiesTurn = true;

		yield return new WaitForSeconds (turnDelay);

		if (enemyL.Count == 0) {
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < enemyL.Count; i++) {
			enemyL [i].MoveEnemy ();

			yield return new WaitForSeconds(turnDelay);
		}

		playersTurn = true;

		enemiesTurn = false;
	}
}
