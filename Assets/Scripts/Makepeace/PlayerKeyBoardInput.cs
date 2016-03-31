using UnityEngine;
using System.Collections;

public class PlayerKeyBoardInput : MonoBehaviour {

	public void UnitAction(Unit activePlayer) {
		if (Input.GetKey (KeyCode.W)) {
			activePlayer.Move (1);
			return;
		}
		if (Input.GetKey (KeyCode.E)) {
			activePlayer.Move (2);
			return;
		}
		if (Input.GetKey (KeyCode.D)) {
			activePlayer.Move (3);
			return;
		}
		if (Input.GetKey (KeyCode.X)) {
			activePlayer.Move (4);
			return;
		}
		if (Input.GetKey (KeyCode.Z)) {
			activePlayer.Move (5);
			return;
		}
		if (Input.GetKey (KeyCode.A)) {
			activePlayer.Move (6);
			return;
		}
		if (Input.GetKey (KeyCode.F) && activePlayer.GetComponentInChildren<Weapon> ().type >= 0) {
			if (GameManager.instance.activeEnemy != null) {
				GameManager.instance.activeEnemy = null;
			}

			activePlayer.possibleAttack ();
			activePlayer.Status = 3;
			return;
		}
		if (Input.GetKey (KeyCode.H) && activePlayer.GetComponentInChildren<Weapon> ().type < 0) {
			activePlayer.possibleInteract ();
			activePlayer.Status = 5;
			return;
		}
		if (Input.GetKey (KeyCode.T)) {
			activePlayer.Status = 0;
			return;
		}
		if (Input.GetKey (KeyCode.Y)) {
			GameManager.instance.StatsUI.GetComponent<StatsMenu> ().UpdateMenu (activePlayer.char_stats);
			if (GameManager.instance.StatsUI.activeSelf) {
				GameManager.instance.StatsUI.SetActive (false);
			} else {
				GameManager.instance.StatsUI.SetActive (true);
			}
		}
	}

	public void GlobalAction() {
		if (Input.GetKey (KeyCode.Escape)) {
			GameManager.instance.prevState = GameManager.instance.State;
			GameManager.instance.State = 0;
			GameManager.instance.PauseUI.SetActive(true);
		}
	}

	public void CameraAction() {
		GameObject camera = GameObject.Find ("Main Camera");
		CameraBounds bounds = camera.GetComponentInChildren<CameraBounds> ();

		if (Input.GetKey (KeyCode.LeftArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x - 0.5F, camera.transform.position.y, camera.transform.position.z);
			if (pos.x < bounds.minX) {
				pos.x = bounds.minX;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x + 0.5F, camera.transform.position.y, camera.transform.position.z);
			if (pos.x > bounds.maxX) {
				pos.x = bounds.maxX;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 0.5F);
			if (pos.z > bounds.maxZ) {
				pos.z = bounds.maxZ;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 0.5F);
			if (pos.z < bounds.minZ) {
				pos.z = bounds.minZ;
			}
			camera.transform.position = pos;
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0) {
			float zoom = Input.GetAxis ("Mouse ScrollWheel");
			bounds.zoom += zoom;
			camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y - zoom, camera.transform.position.z + zoom);
		}

		if (Input.GetKey (KeyCode.R)) {
			camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y + bounds.zoom, camera.transform.position.z - bounds.zoom);
			bounds.zoom = 0;
		}
	}
}
