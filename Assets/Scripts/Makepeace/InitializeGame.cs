using UnityEngine;
using System.Collections;

public class InitializeGame : MonoBehaviour {

	private float STRmin, STRmax, DEXmin, DEXmax, INTmin, INTmax, HPmin, HPmax, 
	DEFmin, DEFmax, STRval, DEXval, INTval, HPval, DEFval;

	void Awake() {
		GameManager.instance.InitGame ();
		InitializeLists ();
	}

	public void InitializeLists() {
		for (int i = 0; i < GameManager.instance.enemyL.Count; i++) {
			RandomizeStats ();
			GameManager.instance.enemyL [i].GetComponentInChildren<Stats> ().SetStats (STRval, DEXval, INTval, HPval, DEFval, 2.0f);
		}

		for (int i = 0; i < GameManager.instance.playerL.Count; i++) {
			GameManager.instance.playerL [i].GetComponentInChildren<ParticleSystem> ().Stop (true);
		}
	}

	void RandomizeStats() {
		STRmin = 5.0f;
		STRmax = 8.0f;
		STRval = Random.Range (STRmin, STRmax);

		DEXmin = 1.0f;
		DEXmax = 5.0f;
		DEXval = Random.Range (DEXmin, DEXmax);

		INTmin = 1.0f;
		INTmax = 5.0f;
		INTval = Random.Range (INTmin, INTmax);

		HPmin = 40.0f;
		HPmax = 65.0f;
		HPval = Random.Range (HPmin, HPmax);

		DEFmin = 3.0f;
		DEFmax = 8.0f;
		DEFval = Random.Range (DEFmin, DEFmax);
	}
}
