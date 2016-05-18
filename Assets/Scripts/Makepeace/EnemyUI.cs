using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {

	public Text enemyName;
	public Text enemyLevel;
	public Text enemyHealth;

	public void UpdateUI(Stats enemyStats) {

		enemyName.text = enemyStats.U_Name;
		enemyLevel.text = "Lvl: " + enemyStats.Lv;
		enemyHealth.text = "Health: " + enemyStats.cHP + "/" + enemyStats.mHP;
	}
}
