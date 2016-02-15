using UnityEngine;
using System.Collections;
using System;

public class Stats : MonoBehaviour
{
	public float STR;
	public float DEX;
	public float INT;
	public float HP;
	public float DEF;
	public float MOV;

	public void SetStats(float STRval, float DEXval, float INTval, float HPval, float DEFval, float MOVval){
		STR = Mathf.Round(STRval * 10f) / 10f;
		DEX = Mathf.Round(DEXval * 10f) / 10f;
		INT = Mathf.Round(INTval * 10f) / 10f;
		HP = Mathf.Round(HPval * 10f) / 10f;
		DEF = Mathf.Round(DEFval * 10f) / 10f;
		MOV = MOVval;

		Debug.Log (STR + " " + DEX + " " + INT + " " + HP + " " + DEF);
	}
}