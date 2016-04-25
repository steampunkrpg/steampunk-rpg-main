using UnityEngine;
using System.Collections;

public class PlayerKeyBoardInput : MonoBehaviour {

	public bool tabReset;
	public bool statReset;
	public bool invReset;
	public int prevActiveState;

	void Start() {
		tabReset = true;
		statReset = true;
		invReset = true;
	}

	public void UnitAction(Unit activePlayer) {	
		/*if (Input.GetKey (KeyCode.F) && activePlayer.GetComponentInChildren<Weapon> ().type >= 0) {
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
			if (GameManager.instance.StatsUI.activeSelf) {
				GameManager.instance.StatsUI.SetActive (false);
			}
			activePlayer.Status = 0;
			return;
		}
		if (Input.GetKeyDown (KeyCode.I) && invReset) {
			invReset = false;
			if (GameManager.instance.InvUI.activeSelf) {
				GameManager.instance.InvUI.SetActive (false);
				activePlayer.Status = prevActiveState;
			} else {
				prevActiveState = activePlayer.Status;
				activePlayer.Status = 7;
				GameManager.instance.InvUI.SetActive (true);
			}
		} else if (Input.GetKeyUp (KeyCode.I)) {
			invReset = true;
		}*/
		if (Input.GetKeyDown (KeyCode.Y) && statReset) {
			statReset = false;
			GameManager.instance.StatsUI.GetComponent<StatsMenu> ().UpdateMenu (activePlayer.char_stats);
			if (GameManager.instance.StatsUI.activeSelf) {
				GameManager.instance.StatsUI.SetActive (false);
			} else {
				GameManager.instance.StatsUI.SetActive (true);
			}
		} else if (Input.GetKeyUp (KeyCode.Y)) {
			statReset = true;
		}
		/*if (Input.GetKey (KeyCode.M)) {
			GameManager.instance.activePlayer.Status = 6;
			activePlayer.possibleMoves ();
			return;
		}*/
	}

	public void GlobalAction() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!GameManager.instance.PauseUI.active)
            {
                GameManager.instance.prevState = GameManager.instance.State;
                GameManager.instance.State = 0;
                GameManager.instance.PauseUI.SetActive(true);
            } else
            {
                GameManager.instance.prevState = GameManager.instance.State;
                GameManager.instance.State = GameManager.instance.prevState;
                GameManager.instance.PauseUI.SetActive(false);
            }
		}
	}

	public void PlayerTurn() {
			if (Input.GetKeyUp (KeyCode.Tab)) {
			tabReset = true;
		} else if (Input.GetKey (KeyCode.Tab) && tabReset) {
			tabReset = false;
			if (GameManager.instance.activePlayer == null) {
				for (int i = 0; i < GameManager.instance.playerL.Count; i++) {
					if (GameManager.instance.playerL [i].Status == 1) {
						GameManager.instance.activePlayer = GameManager.instance.playerL [i];
						GameManager.instance.PlayerUI.GetComponent<PlayerUI> ().UpdateUI (GameManager.instance.activePlayer.char_stats);
						GameManager.instance.PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
						break;
					}
				}
			} else {
				for (int i = 0; i < GameManager.instance.playerL.Count; i++) {
					if (GameManager.instance.activePlayer == GameManager.instance.playerL [i]) {
						if (i == GameManager.instance.playerL.Count - 1) {
							for (int j = 0; j < GameManager.instance.playerL.Count; j++) {
								if (GameManager.instance.playerL [j].Status == 1 && GameManager.instance.playerL [j] != GameManager.instance.activePlayer) {
									GameManager.instance.activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
									GameManager.instance.activePlayer = GameManager.instance.playerL [j];
									GameManager.instance.PlayerUI.GetComponent<PlayerUI> ().UpdateUI (GameManager.instance.activePlayer.char_stats);
									break;
								}
							}
							break;
						} else {
							for (int j = 0; j < GameManager.instance.playerL.Count; j++) {
								if (GameManager.instance.playerL [(j+i+1)%GameManager.instance.playerL.Count].Status == 1 && GameManager.instance.playerL [(j+i+1)%GameManager.instance.playerL.Count] != GameManager.instance.activePlayer) {
									GameManager.instance.activePlayer.GetComponentInChildren<ParticleSystem> ().Stop (true);
									GameManager.instance.activePlayer = GameManager.instance.playerL [(j+i+1)%GameManager.instance.playerL.Count];
									GameManager.instance.PlayerUI.GetComponent<PlayerUI> ().UpdateUI (GameManager.instance.activePlayer.char_stats);
									break;
								}
							}
							break;
						}
					}
				}
			}
		}
	}

	public void CameraAction() {
		GameObject camera = GameObject.Find ("Main Camera");
		CameraBounds bounds = camera.GetComponentInChildren<CameraBounds> ();


		if (Input.GetKey (KeyCode.A)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x - 0.5F, camera.transform.position.y, camera.transform.position.z);
			if (pos.x < bounds.minX) {
				pos.x = bounds.minX;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.D)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x + 0.5F, camera.transform.position.y, camera.transform.position.z);
			if (pos.x > bounds.maxX) {
				pos.x = bounds.maxX;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.W)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 0.5F);
			if (pos.z > bounds.maxZ) {
				pos.z = bounds.maxZ;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.S)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 0.5F);
			if (pos.z < bounds.minZ) {
				pos.z = bounds.minZ;
			}
			camera.transform.position = pos;
		}

		if (Input.GetKey (KeyCode.Q)) {
			//Rotate clockwise
		}

		if (Input.GetKey (KeyCode.E)) {
			//Rotate counter clockwise
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0) {
			float zoom = Input.GetAxis ("Mouse ScrollWheel");
			if (bounds.zoom + zoom <= 6) {
				bounds.zoom += zoom;
				camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y - zoom, camera.transform.position.z + zoom);
			} else {
				//do nothing
			}
		}

		if (Input.GetKey (KeyCode.R)) {
			camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y + bounds.zoom, camera.transform.position.z - bounds.zoom);
			//Rotate back to center
			bounds.zoom = 0;
		}
	}
}
