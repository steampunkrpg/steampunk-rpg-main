using UnityEngine;
using System.Collections;

public class XpGrowthRate : MonoBehaviour {

	public float[] GetGrowthRates(string className) {
		float[] lvStats = new float[8];
		int x = Random.Range (0, 100);
		switch (className) {
		case "Vanguard":
			if (x <= 70) {
				lvStats [0] = 1;
			}
			if (x <= 50) {
				lvStats [1] = 1;
			}
			if (x <= 20) {
				lvStats [2] = 1;
			}
			if (x <= 45) {
				lvStats [3] = 1;
			}
			if (x <= 50) {
				lvStats [4] = 1;
			}
			if (x <= 25) {
				lvStats [5] = 1;
			}
			if (x <= 40) {
				lvStats [6] = 1;
			}
			if (x <= 40) {
				lvStats [7] = 1;
			}
			break;

		case "Ranger":
			if (x <= 65) {
				lvStats [0] = 1;
			}
			if (x <= 55) {
				lvStats [1] = 1;
			}
			if (x <= 10) {
				lvStats [2] = 1;
			}
			if (x <= 35) {
				lvStats [3] = 1;
			}
			if (x <= 50) {
				lvStats [4] = 1;
			}
			if (x <= 30) {
				lvStats [5] = 1;
			}
			if (x <= 45) {
				lvStats [6] = 1;
			}
			if (x <= 35) {
				lvStats [7] = 1;
			}
			break;

		case "Alchemist":
			if (x <= 50) {
				lvStats [0] = 1;
			}
			if (x <= 20) {
				lvStats [1] = 1;
			}
			if (x <= 65) {
				lvStats [2] = 1;
			}
			if (x <= 40) {
				lvStats [3] = 1;
			}
			if (x <= 40) {
				lvStats [4] = 1;
			}
			if (x <= 25) {
				lvStats [5] = 1;
			}
			if (x <= 25) {
				lvStats [6] = 1;
			}
			if (x <= 55) {
				lvStats [7] = 1;
			}
			break;

		default:
			lvStats = null;
			break;
		}

		return lvStats;
	}
}
