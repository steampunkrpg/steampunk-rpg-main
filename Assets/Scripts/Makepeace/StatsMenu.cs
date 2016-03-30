using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour {

	public Text char_name;
	public Text level;
	public Text stats;

	public void InitializeMenu(Stats charStats) {

		char_name.text = charStats.U_Name;
		level.text = "Lv: " + charStats.Lv;
		stats.text = "HP: " + charStats.cHP + "/" + charStats.mHP + "\nStr: " + charStats.Str + "\nMag: " + charStats.Mag + "\nSkl: " + charStats.Skl + "\nSpd: " + charStats.Spd + "\nLuck: " + charStats.Lck + "\nDef: " + charStats.Def + "\nRes: " + charStats.Res;

		this.gameObject.SetActive (false);
	}

	public void UpdateMenu(Stats charStats) {
		char_name.text = charStats.U_Name;
		level.text = "Lv: " + charStats.Lv;
		stats.text = "HP: " + charStats.cHP + "/" + charStats.mHP + "\nStr: " + charStats.Str + "\nMag: " + charStats.Mag + "\nSkl: " + charStats.Skl + "\nSpd: " + charStats.Spd + "\nLuck: " + charStats.Lck + "\nDef: " + charStats.Def + "\nRes: " + charStats.Res;
	}
}
