using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {

	public Text charInfo;
	public Text level;
	public Text interaction;

	public void InitializeMenu(Stats charStats) {

		charInfo.text = charStats.U_Name + "\nHealth: " + charStats.cHP + "/" + charStats.mHP;
		level.text = "Lv " + charStats.Lv;

		if (this.gameObject.transform.parent.GetComponentInChildren<Weapon> ().type < 0) {
			interaction.text = "Interact";
		} else {
			interaction.text = "Attack";
		}

		this.gameObject.SetActive (false);
	}

	public void UpdateMenu(Stats charStats) {
		charInfo.text = charStats.U_Name + "\nHealth: " + charStats.cHP + "/" + charStats.mHP;
		level.text = "Lv " + charStats.Lv;
	}

	public void Move_Button() {
		GameManager.instance.activePlayer.Status = 6;
	}

	public void Attack_Button() {
		//		if (interaction.text == "Interact") {
		//			GameManager.instance.activePlayer.Status = 5;
		////		} else {
		//		} else if (interaction.text == "Attack") {
		//			GameManager.instance.activePlayer.Status = 3;
		//		}
		GameManager.instance.activePlayer.Status = 3;
	}

	public void End_Button() {
		GameManager.instance.activePlayer.Status = 0;
	}
}
