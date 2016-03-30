using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyMenu : MonoBehaviour {

	public Text charInfo;
	public Text level;

	public void InitializeMenu(Stats enemyStats) {

		charInfo.text = enemyStats.U_Name + "\nHealth: " + enemyStats.cHP + "/" + enemyStats.mHP;
		level.text = "Lv " + enemyStats.Lv;

		this.gameObject.SetActive (false);
	}

	public void UpdateMenu(Stats enemyStats) {
		charInfo.text = enemyStats.U_Name + "\nHealth: " + enemyStats.cHP + "/" + enemyStats.mHP;
		level.text = "Lv " + enemyStats.Lv;
	}
}
