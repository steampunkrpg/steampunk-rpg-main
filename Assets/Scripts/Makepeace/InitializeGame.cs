using UnityEngine;
using System.Collections;

public class InitializeGame : MonoBehaviour {

	void Awake() {
		if (!GameManager.instance.inLevel) {
			GameManager.instance.InitGame ();
			InitializeLists ();
		} else {
			UpdateLists ();
			GameManager.instance.State = 5;
		}
	}

	public void InitializeLists() {
		for (int i = 0; i < GameManager.instance.enemyL.Count; i++) {
			GameManager.instance.enemyL[i].GetComponentInChildren<ParticleSystem> ().Stop (true);
		}

		for (int i = 0; i < GameManager.instance.playerL.Count; i++) {
			GameManager.instance.playerL [i].GetComponentInChildren<ParticleSystem> ().Stop (true);
		}
	}

	public void UpdateLists() {
		GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("GridTile");

		foreach (GameObject unit in units) {
			if (!GameManager.instance.playerL.Contains (unit.GetComponent<Unit> ())) {
				Destroy (unit);
			} else if (GameManager.instance.activePlayer == unit.GetComponent<Unit> ()) {
				unit.transform.position = unit.GetComponent<Unit> ().tile.transform.position;
				unit.GetComponent<Unit> ().animUnit.Play ("Idle");
				unit.transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
			}
		}

		foreach (GameObject enemy in enemies) {
			if (!GameManager.instance.enemyL.Contains (enemy.GetComponent<Enemy> ())) {
				Destroy (enemy);
			} else {
				enemy.transform.position = enemy.GetComponent<Enemy> ().tile.transform.position;
				enemy.GetComponent<Enemy> ().animEnemy.Play ("Idle");
				enemy.transform.Rotate (new Vector3 (0.0f, 0.0f, 0.0f));
			}
		}

		foreach (GameObject tile in tiles) {
			if (!GameManager.instance.tileL.Contains (tile.GetComponent<HexTile> ())) {
				Destroy (tile.transform.parent.gameObject);
				break;
			}
		}
	}
}
