using UnityEngine;
using System.Collections;

public class TestLoader : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Awake () {
		if (TestGameManager.instanceT == null) {
			Instantiate (gameManager);
		}
	}
}
