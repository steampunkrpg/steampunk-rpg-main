using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

	public Text charInfo;
	public Text level;

	public void InitializeMenu(Stats charStats) {
		charInfo.text = charStats.U_Name + "\nHealth: " + charStats.cHP + "/" + charStats.mHP;
		level.text = "Lv " + charStats.Lv;
		this.gameObject.SetActive (false);
	}

	public void UpdateMenu(Stats charStats) {
		charInfo.text = charStats.U_Name + "\nHealth: " + charStats.cHP + "/" + charStats.mHP;
		level.text = "Lv " + charStats.Lv;
	}
}
