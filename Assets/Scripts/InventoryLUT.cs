using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryLUT : MonoBehaviour {

	public List<Item> treasury;
	//public List<Consumable> medicineCabinet;

	void Start () {
		Item WoodSword = new Item(1, "WoodSword", 5, 95, 20, 5, new List<float>{1}, 0);
		Item Crossbow = new Item (1, "Crossbow", 7, 90, 20, 3, new List<float> {2}, 0);
		Item PyroFlame = new Item (1, "PyroFlame", 9, 70, 10, 1, new List<float> {1, 2, 3}, 1);
		Item HealingHand = new Item (1, "HealingHand", 3, 100, 10, 1, new List<float> {1}, -1);

		Item Potion = new Item (0, "Potion");
		Item FuryElixir = new Item (0, "FuryElixir");

		treasury = new List<Item> {WoodSword, Crossbow, PyroFlame, HealingHand, Potion, FuryElixir};
	}

	public Item Lookup (string used) {
		for (int i = 0; i < treasury.Count; i++) {
			if (treasury [i].ToString() == used) {
				Item itemToUse = treasury [i];
				return itemToUse;
			}
		}
		return null;
	}
}
