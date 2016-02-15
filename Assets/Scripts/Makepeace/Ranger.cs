using UnityEngine;
using System.Collections;

public class Ranger : MonoBehaviour {

	private Stats char_stat;

	void Start() {
		char_stat = this.gameObject.GetComponent<Stats> ();
	}

	void Update() {

	}
}
