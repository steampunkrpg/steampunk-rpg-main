using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	private Dictionary<string, int> itemsAndCounts;

	void Start() {
		itemsAndCounts = new Dictionary<string, int> ();

		//testing item
		itemsAndCounts.Add ("Potion", 10);
	}

	public void AddItem(string item, int count) {
		if (itemsAndCounts.ContainsKey (item) == false) {
			itemsAndCounts.Add (item, count);
		} else {
			itemsAndCounts [item] += 1;
		}
	}

	public void UseItem(string item) {
		if (itemsAndCounts.ContainsKey (item) == true) {
			itemsAndCounts [item] -= 1;

			if (itemsAndCounts [item] == 0) {
				itemsAndCounts.Remove (item);
			}
		}
	}
}