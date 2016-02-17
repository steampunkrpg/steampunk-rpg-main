using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	private List<GameObject> items;

	void Start() {
		items = new List<GameObject> ();
	}
}
