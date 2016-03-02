using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryManager : MonoBehaviour {
	public static Dictionary<string, int> itemsAndCounts;
	public GameObject inventoryWindow, inventoryEntry, inventoryEntryGO;

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
	}

	public void AddItem(string item, int count) {
		if (itemsAndCounts.ContainsKey (item) == false) {
			itemsAndCounts.Add (item, count);
			print (itemsAndCounts ["Potion"]);
			ButtonCreator (item, count);
		} else {
			itemsAndCounts [item] += 1;
		}
	}

	//handles use of the item
	public void UseItem(Button button)
	{	
		Regex rgx = new Regex("[^a-zA-Z]+");

		Text pressedText = button.GetComponentInChildren<Text> ();
		string pressedTextKey = rgx.Replace (pressedText.text, "");

		itemsAndCounts [pressedTextKey] -= 1;
		pressedText.text = " " + pressedTextKey + ": " + itemsAndCounts [pressedTextKey];
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
		inventoryEntryGO.transform.parent = this.transform.FindChild ("InventoryWindow");
		inventoryEntryGO.name = "Inventory Entry " + buttonCount;
		inventoryEntryGO.transform.position = inventoryWindow.transform.position + heightOffset;
		buttonsInMenu.Add (inventoryEntryGO);

		buttonText = inventoryEntryGO.transform.FindChild("Text").GetComponent<Text>();
		buttonText.text = " " + item + ": " + count;

		buttonCount += 1;
		heightOffset.y -= 40.0f;
	}
}