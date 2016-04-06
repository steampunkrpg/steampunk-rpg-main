using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryManager : MonoBehaviour {
	
	public static List<KeyValuePair<string, int>> itemsAndCounts;
	public GameObject inventoryWindow, inventoryEntry, inventoryEntryGO;

	private List<GameObject> buttonsInMenu;
	private Vector3 heightOffset;
	private Text buttonText;

	public void CreateDefault() {
		buttonsInMenu = new List<GameObject> ();
		heightOffset = new Vector3(0.0f, 80.0f, 0.0f);
		itemsAndCounts = new List<KeyValuePair<string, int>> ();
		inventoryWindow = GameObject.Find ("InventoryWindow");
		//inventoryWindow.SetActive (false);

		//testing item
		AddItem("Potion", 10);
		AddItem ("Sword", 1);

		//generate some "weapons" for testing
		/*Weapon WoodSword = new Weapon("WoodSword", 5, 95, 20, 5, new List<float>{1}, 0);
		Weapon Crossbow = new Weapon ("Crossbow", 7, 90, 20, 3, new List<float> {2}, 0);
		Weapon PyroFlame = new Weapon ("PyroFlame", 9, 70, 10, 1, new List<float> {1, 2, 3}, 1);
		Weapon HealingHand = new Weapon ("HealingHand", 3, 100, 10, 1, new List<float> {1}, -1);*/
	}

	public void AddItem(string item, int count) {
		
		for (int i = 0; i < itemsAndCounts.Count; i++) {
			if (itemsAndCounts [i].Key == item) {
				itemsAndCounts [i] = new KeyValuePair<string, int> (item, itemsAndCounts [i].Value + 1);
				GameObject.Destroy (GameObject.Find (item));
				ButtonCreator (item, itemsAndCounts [i].Value + 1);
				i = itemsAndCounts.Count;
			} else if (i == itemsAndCounts.Count - 1 && itemsAndCounts [i].Key != item) {
				itemsAndCounts.Add (new KeyValuePair<string, int> (item, count));
				ButtonCreator (item, count);
			}
		}
	}

	void ButtonCreator (string item, int value) {
		inventoryEntryGO = Instantiate (inventoryEntry);
		inventoryEntryGO.transform.SetParent (inventoryWindow.transform);
		inventoryEntryGO.name = item;
		inventoryEntryGO.transform.position = inventoryWindow.transform.position + heightOffset;
		buttonsInMenu.Add (inventoryEntryGO);
		
		buttonText = inventoryEntryGO.transform.FindChild ("Text").GetComponent<Text> ();
		buttonText.text = " " + item + ": " + value;
		heightOffset.y -= 40.0f;
	}

	//handles use of the item
	public void UseItem(Button button)
	{	
		/*SECTION 1*/
		//decrements the count on the inventory button
		string pressedString = button.transform.name.ToString();

		for (int i = 0; i < itemsAndCounts.Count; i++) {
			if (itemsAndCounts [i].Key == pressedString) {
				itemsAndCounts [i] = new KeyValuePair<string, int> (pressedString, itemsAndCounts [i].Value + 1);
				GameObject.Destroy (GameObject.Find (pressedString));
				ButtonCreator (pressedString, itemsAndCounts [i].Value - 1);
				i = itemsAndCounts.Count;
			}
		}

		/*SECTION 2*/
		//use cases for items
		var apsScript =	GameManager.instance.activePlayer.GetComponent<Stats> ();
		var apwScript = GameManager.instance.activeEnemy.GetComponent<Weapon> ();

}