using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ranger : MonoBehaviour {

	public List<int> validEquips;

	void Start() {
		validEquips.Add (4);
		validEquips.Add (6);
	}

	void Update() {

	}
}
