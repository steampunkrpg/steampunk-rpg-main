using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryManager : MonoBehaviour {
	
	public static List<KeyValuePair<string, int>> itemsAndCounts;
	private GameObject inventoryEntry;

	private Text buttonText;

	public void CreateDefault() {
		itemsAndCounts = new List<KeyValuePair<string, int>> ();

		//testing item
		//AddItem("Potion", 10);
		AddItem ("WoodSword", 1);
		//AddItem ("Potion", 1);
		for (int i = 1; i <= 20; i++) {
			AddItem ("Potion" + i, 1);
		}
	}

	public void AddItem(string item, int count) {
		for (int i = 0; i < itemsAndCounts.Count; i++) {
			if (itemsAndCounts [i].Key == item) {
				itemsAndCounts [i] = new KeyValuePair<string, int> (item, itemsAndCounts [i].Value + count);
				GameObject.Find (item).GetComponentInChildren<Text> ().text = item + ": " + itemsAndCounts [i].Value;
				return;
			} 
		}

		itemsAndCounts.Add (new KeyValuePair<string, int> (item, count));
		ButtonCreator (item, count);
	}

	void ButtonCreator (string item, int value) {
		inventoryEntry = (GameObject)Resources.Load ("Prefabs/inventoryEntry", typeof(GameObject));
		GameObject inventoryEntryGO = Instantiate (inventoryEntry) as GameObject;
		inventoryEntryGO.transform.SetParent (GameObject.Find("Window").transform);
		inventoryEntryGO.name = item;
		inventoryEntryGO.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		
		buttonText = inventoryEntryGO.GetComponentInChildren<Text> ();
		buttonText.text = item + ": " + value;
	}

	//handles use of the item
	public void UseItem (Button button)
	{	
		/*SECTION 1*/
		//decrements the count on the inventory button
		string pressedItem = button.transform.name.ToString ();

		/*SECTION 2*/
		//use of items
		var apsScript =	GameManager.instance.activePlayer.GetComponentInChildren<Stats> ();
		var apwScript = GameManager.instance.activePlayer.GetComponentInChildren<Weapon> ();

		//get type of item (Weapon/Consumable)
		InventoryLUT lookup = new InventoryLUT();
		Item targetItem = lookup.Lookup(pressedItem);

		//not many consumables so just handle cases here
		if (targetItem.iType == 0) {
			if (targetItem.iName == "Potion") {
				if (apsScript.cHP != apsScript.mHP) {
					return;
				} else {
					apsScript.cHP += apsScript.mHP * 0.40f;
				}
			}
			if (targetItem.iName == "FuryElixir") {
				apwScript.Mt *= 1.4f;
			}
			if (targetItem.iName == "IronhideElixir") {
				apsScript.Def *= 1.4f;
			}
		}

		//weapon type stat assignments
		else if (targetItem.iType == 1) {
			AddItem (apwScript.wName, 1);

			apwScript.wName = targetItem.iName;
			apwScript.Mt = targetItem.Mt;
			apwScript.Hit = targetItem.Hit;
			apwScript.Crit = targetItem.Crit;
			apwScript.Wt = targetItem.Wt;
			apwScript.Rng = targetItem.Rng;
			apwScript.type = targetItem.type;
		}

		for (int i = 0; i < itemsAndCounts.Count; i++) {
			if (itemsAndCounts [i].Key == pressedItem) {
				itemsAndCounts [i] = new KeyValuePair<string, int> (pressedItem, itemsAndCounts [i].Value - 1);
				if (itemsAndCounts [i].Value == 0) {
					GameObject.Destroy (GameObject.Find (pressedItem));
				} else {
					GameObject.Find (pressedItem).GetComponentInChildren<Text> ().text = pressedItem + ": " + itemsAndCounts [i].Value;
				}
				break;
			}
		}
	}
}