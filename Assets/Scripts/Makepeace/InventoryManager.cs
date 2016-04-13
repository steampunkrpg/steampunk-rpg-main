using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryManager : MonoBehaviour {
	
	public static List<KeyValuePair<string, int>> itemsAndCounts;
	public GameObject inventoryWindow, inventoryEntry;

	private Vector3 heightOffset;
	private Text buttonText;

	public void CreateDefault() {
		heightOffset = new Vector3(0.0f, 80.0f, 0.0f);
		itemsAndCounts = new List<KeyValuePair<string, int>> ();

		//testing item
		AddItem("Potion", 10);
		//AddItem ("Sword", 1);
		AddItem ("Potion", 1);
	}

	public void AddItem(string item, int count) {
		for (int i = 0; i < itemsAndCounts.Count; i++) {
			if (itemsAndCounts [i].Key == item) {
				itemsAndCounts [i] = new KeyValuePair<string, int> (item, itemsAndCounts [i].Value + count);
				GameObject.Find (item).GetComponentInChildren<Text>().text = " " + item + ": " + (itemsAndCounts [i].Value + count);
				return;
			} 
		}

		itemsAndCounts.Add (new KeyValuePair<string, int> (item, count));
		ButtonCreator (item, count);
	}

	void ButtonCreator (string item, int value) {
		GameObject inventoryEntryGO = Instantiate (inventoryEntry);
		if (itemsAndCounts.Count % 2 == 1) {
			inventoryEntryGO.transform.SetParent (inventoryWindow.transform.Find ("LeftWindow"));
		} else {
			inventoryEntryGO.transform.SetParent (inventoryWindow.transform.Find ("RightWindow"));
		}
		inventoryEntryGO.name = item;
		inventoryEntryGO.transform.position = heightOffset;
		inventoryEntryGO.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		
		buttonText = inventoryEntryGO.GetComponentInChildren<Text> ();
		buttonText.text = " " + item + ": " + value;
		heightOffset.y -= 40.0f;
	}

	//handles use of the item
	public void UseItem (Button button)
	{	
		/*SECTION 1*/
		//decrements the count on the inventory button
		string pressedItem = button.transform.name.ToString ();

		for (int i = 0; i < itemsAndCounts.Count; i++) {
			if (itemsAndCounts [i].Key == pressedItem) {
				itemsAndCounts [i] = new KeyValuePair<string, int> (pressedItem, itemsAndCounts [i].Value + 1);
				GameObject.Destroy (GameObject.Find (pressedItem));
				ButtonCreator (pressedItem, itemsAndCounts [i].Value - 1);
				break;
			}
		}

		/*SECTION 2*/
		//use of items
		var apsScript =	GameManager.instance.activePlayer.GetComponent<Stats> ();
		var apwScript = GameManager.instance.activePlayer.GetComponent<Weapon> ();
		InventoryLUT lookup = new InventoryLUT ();

		//get type of item (Weapon/Consumable)
		Item targetItem = lookup.Lookup(pressedItem);

		//not many consumables so just handle cases here
		if (targetItem.iType == 0) {
			if (targetItem.iName == "Potion") {
				apsScript.cHP += apsScript.mHP * 0.40f;
			}
			if (targetItem.iName == "FuryElixir") {
				apwScript.Mt = apwScript.Mt * 1.4f;
			}
		}

		//weapon type stat assignments
		else if (targetItem.iType == 1){
			apwScript.Mt = targetItem.Mt ;
			apwScript.Hit = targetItem.Hit;
			apwScript.Crit = targetItem.Crit;
			apwScript.Wt = targetItem.Wt;
			apwScript.Rng = targetItem.Rng;
			apwScript.type = targetItem.type;
		}
	}
}