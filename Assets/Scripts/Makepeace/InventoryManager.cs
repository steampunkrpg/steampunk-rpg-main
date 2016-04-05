using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryManager : MonoBehaviour {
	public static Dictionary<string, int> itemsAndCounts;
	public GameObject inventoryWindow, inventoryEntry, inventoryEntryGO;
	public Unit activePlayer;

	private List<GameObject> buttonsInMenu;
	private Vector3 heightOffset;
	private int buttonCount = 0, hasClicked = 0;
	private Text buttonText;

	void Start() {
		buttonsInMenu = new List<GameObject> ();
		heightOffset = new Vector3(0.0f, 80.0f, 0.0f);
		itemsAndCounts = new Dictionary<string, int> ();
		inventoryWindow = GameObject.Find ("InventoryWindow");
		inventoryWindow.SetActive (false);

		//testing item
		AddItem("Potion", 10);
		AddItem ("Sword", 1);

		//generate some "weapons" for testing
		Weapon WoodSword = new Weapon(5, 95, 20, 5, new List<float>{2, 1, 2}, 0);
		Weapon Crossbow = new Weapon (7, 90, 20, 3, new List<float> {2, 1, 2}, 0);
		Weapon PyroFlame = new Weapon (9, 70, 10, 1, new List<float> {2, 1, 2}, 1);
		Weapon HealingHand = new Weapon (3, 100, 10, 1, new List<float> {2, 1, 2}, -1);
	}

	public void AddItem(string item, int count) {
		if (itemsAndCounts.ContainsKey (item) == false) {
			itemsAndCounts.Add (item, count);
			ButtonCreator (item, count);
		} else {
			itemsAndCounts [item] += 1;
		}
	}

	//handles use of the item
	public void UseItem(Button button)
	{	
		/*SECTION 1*/
		//decrements the count on the inventory button
		Regex rgx = new Regex("[^a-zA-Z]+");
		Text pressedText = button.GetComponentInChildren<Text> ();
		string pressedTextKey = rgx.Replace (pressedText.text, "");

		if (itemsAndCounts [pressedTextKey] != 0) {
			itemsAndCounts [pressedTextKey] -= 1;
			pressedText.text = " " + pressedTextKey + ": " + itemsAndCounts [pressedTextKey];
		}

		/*SECTION 2*/
		//use cases for items
		var gameManager = GameObject.Find ("GameManager");
		//GameManager.instance.activePlayer.GetComponent<Stats> ();
		var script = gameManager.GetComponent<GameManager>();
		var activePlayerStats =	script.activePlayer.transform.FindChild ("Stats").GetComponent<Stats>();
		var apsScript = activePlayerStats.GetComponent<Stats> ();
		Debug.Log (activePlayerStats);
		Debug.Log (apsScript);

		var activePlayerWeapon = activePlayer.transform.FindChild ("Weapon");
		var apwScript = activePlayerWeapon.GetComponent<Weapon> ();

		/*
		switch (item) {
			case "Potion":
				apsScript.cHP += apsScript.mHP * 0.40f;
					break;
			case "Sword":
				apwScript.Mt = 10;
				apwScript.Hit = 95;
				apwScript.Crit = 15;
				apwScript.Wt = 3;
				apwScript.Rng = new List<float> {1,1};
				apwScript.type = 0;
			}
			*/
		}

	public void InventoryToggler() {
		if (hasClicked == 0) {
			inventoryWindow.SetActive (true);
			hasClicked = 1;
		} else if (hasClicked == 1){
			inventoryWindow.SetActive (false);
			hasClicked = 0;
		}
	}

	void ButtonCreator (string item, int count) {
		inventoryEntryGO = Instantiate (inventoryEntry);
		inventoryEntryGO.transform.SetParent(inventoryWindow.transform);
		inventoryEntryGO.name = "Inventory Entry " + buttonCount;
		inventoryEntryGO.transform.position = inventoryWindow.transform.position + heightOffset;
		buttonsInMenu.Add (inventoryEntryGO);

		buttonText = inventoryEntryGO.transform.FindChild("Text").GetComponent<Text>();
		buttonText.text = " " + item + ": " + count;

		buttonCount += 1;
		heightOffset.y -= 40.0f;
	}
}