using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TileClickTest : MonoBehaviour {

	private Ray mouseRay;
	private RaycastHit hit;
	private RaycastHit vertHit;

	void Update() {
		TileSelect ();
	}

	void TileSelect() {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (mouseRay, out hit)) {
				if (hit.collider.tag.Equals ("Terrain")) {
					if (Physics.Raycast (hit.point, Vector3.down, out vertHit)) {
						Debug.DrawLine (hit.point, vertHit.point);
						if (vertHit.collider.tag.Equals ("GridTile")) {
							vertHit.collider.gameObject.transform.Find ("Possible_Move").gameObject.SetActive (true);
						}
					}
				}
			}
		}
	}
}
