using UnityEngine;
using System.Collections;

public class GameManagerDelete : MonoBehaviour {

	void Start () {
		if (GameManager.instance != null) {
			GameManager.instance.DestroyThis ();
		}
	}
}
