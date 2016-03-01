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
		if (Input.GetKey (KeyCode.F)) {
			activePlayer.possibleAttack ();
			activePlayer.Status = 3;
			return;
		}
	}

	public void CameraAction() {
		GameObject camera = GameObject.Find ("Main Camera");
		CameraBounds bounds = camera.GetComponentInChildren<CameraBounds> ();

		if (Input.GetKey (KeyCode.LeftArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x - 0.1F, camera.transform.position.y, camera.transform.position.z);
			if (pos.x < bounds.minX) {
				pos.x = bounds.minX;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x + 0.1F, camera.transform.position.y, camera.transform.position.z);
			if (pos.x > bounds.maxX) {
				pos.x = bounds.maxX;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 0.1F);
			if (pos.z > bounds.maxZ) {
				pos.z = bounds.maxZ;
			}
			camera.transform.position = pos;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			bounds.locked = false;
			Vector3 pos = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 0.1F);
			if (pos.z < bounds.minZ) {
				pos.z = bounds.minZ;
			}
			camera.transform.position = pos;
		}
	}
}
