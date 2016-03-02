using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	private Dictionary<string, int> itemsAndCounts;
	private Vector3 heightOffset;
	private int buttonCount = 0, hasClicked = 0;
	public GameObject inventoryWindow, inventoryEntry, inventoryEntryGO;
	private Text buttonText;

	void Start() {
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
			ButtonCreator (item, count);
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

		buttonText = inventoryEntryGO.transform.FindChild("Text").GetComponent<Text>();
		buttonText.text = "  " + item + "   x" + count;
		Debug.Log (buttonText.text);

		buttonCount += 1;
		heightOffset.y -= 40.0f;
	}


}