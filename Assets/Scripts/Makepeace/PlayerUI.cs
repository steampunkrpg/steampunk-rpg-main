using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	public Text playerName;
	public Text playerLevel;
	public Text playerHealth;
	public Text playerIntAtt;
	public Button playerMove;

	public void UpdateUI(Stats playerStats) {

		playerName.text = playerStats.U_Name;
		playerLevel.text = "Lvl: " + playerStats.Lv + "\nExp: " + playerStats.Xp;
		playerHealth.text = "Health: " + playerStats.cHP + "/" + playerStats.mHP;

		if (playerStats.gameObject.transform.parent.GetComponentInChildren<Weapon> ().type < 0) {
			playerIntAtt.text = "Interact";
		} else {
			playerIntAtt.text = "Attack";
		}

		if (playerStats.GetComponentInParent<Unit> ().movement == 0) {
			playerMove.interactable = false;
		} else {
			playerMove.interactable = true;
		}
	}

	public void Move_Button() {
		GameManager.instance.activePlayer.Status = 6;
		GameManager.instance.activePlayer.possibleMoves ();
		GameManager.instance.PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
	}

	public void Attack_Button() {
		if (playerIntAtt.text == "Interact") {
			GameManager.instance.activePlayer.Status = 5;
			GameManager.instance.activePlayer.possibleInteract ();
		} else if (playerIntAtt.text == "Attack") {
			GameManager.instance.activePlayer.Status = 3;
			GameManager.instance.activePlayer.possibleAttack ();
		}
		GameManager.instance.activePlayer.Status = 3;
		GameManager.instance.PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
	}

	public void Item_Button() {
		GameManager.instance.PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
		GameManager.instance.InvUI.SetActive (true);
	}

	public void End_Button() {
		GameManager.instance.PlayerUI.GetComponentInChildren<Animator> ().SetTrigger ("UI_Trigger");
		GameManager.instance.activePlayer.Status = 0;
	}
}
