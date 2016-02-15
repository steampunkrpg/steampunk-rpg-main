using UnityEngine;
using System.Collections;

public class InitializeGame : MonoBehaviour {

	void Start() {
		GameManager.instance.InitGame ();
	}
}
