using UnityEngine;
using System.Collections;

public class Alchemist : MonoBehaviour {

	private Stats char_stat;

	void Start() {
		char_stat = this.gameObject.GetComponent<Stats> ();
	}

	void Update() {

	}
}
