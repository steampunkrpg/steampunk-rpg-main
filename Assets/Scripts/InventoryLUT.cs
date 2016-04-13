using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryLUT : MonoBehaviour {

	public Item returnedItem;

	public Item Lookup (string used) {

		switch (used) {

		case "WoodSword":
			returnedItem = new Item (1, "WoodSword", 5, 95, 20, 5, new List<float>{ 1 }, 0);
			break;

		case "Crossbow":
			returnedItem = new Item (1, "Crossbow", 7, 90, 20, 3, new List<float> { 2 }, 0);
			break;

		case "PyroFlame":
			returnedItem = new Item (1, "PyroFlame", 9, 70, 10, 1, new List<float> { 1, 2, 3 }, 1);
			break;

		case "HealingHand":
			returnedItem = new Item (1, "HealingHand", 3, 100, 10, 1, new List<float> { 1 }, -1);
			break;

		case "Potion":
			returnedItem = new Item (0, "Potion");
			break;

		case "FuryElixir":
			returnedItem = new Item (0, "FuryElixir");
			break;

		default:
			returnedItem = null;
			break;
		}

		return returnedItem;
	}
}