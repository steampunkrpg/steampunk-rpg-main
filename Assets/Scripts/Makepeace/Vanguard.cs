using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vanguard : MonoBehaviour {

	private Stats char_stat;

	void Start() {
		char_stat = this.gameObject.GetComponent<Stats> ();

		DontDestroyOnLoad (this);
	}

	void Update() {

	}
}
