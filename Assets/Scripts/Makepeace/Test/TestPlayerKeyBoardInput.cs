﻿using UnityEngine;
using System.Collections;

public class TestPlayerKeyBoardInput : MonoBehaviour {

	public void ProvideAction(TestUnit activePlayer) {
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
			return;
		}
	}
}
