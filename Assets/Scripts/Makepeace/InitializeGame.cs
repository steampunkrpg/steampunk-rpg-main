using UnityEngine;
using System.Collections;

public class InitializeGame : MonoBehaviour {

	void Awake() {
		if (!GameManager.instance.inLevel) {
			GameManager.instance.InitGame ();
			InitializeLists ();
		} else {
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
}
