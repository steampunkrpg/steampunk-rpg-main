using UnityEngine;
using System.Collections;

public class TestInitializeGame : MonoBehaviour {

	private float STRmin, STRmax, DEXmin, DEXmax, INTmin, INTmax, HPmin, HPmax, 
	DEFmin, DEFmax, STRval, DEXval, INTval, HPval, DEFval;

	void Start() {
		TestGameManager.instanceT.InitGame ();
		InitializeLists ();
	}

	public void InitializeLists() {
		for (int i = 0; i < TestGameManager.instanceT.enemyL.Count; i++) {
			RandomizeStats ();
			TestGameManager.instanceT.enemyL [i].GetComponentInChildren<Stats> ().SetStats (5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2);
		}

		for (int i = 0; i < TestGameManager.instanceT.playerL.Count; i++) {
			TestGameManager.instanceT.playerL [i].GetComponentInChildren<ParticleSystem> ().Stop (true);
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
