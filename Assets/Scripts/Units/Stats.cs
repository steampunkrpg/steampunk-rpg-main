using UnityEngine;
using System.Collections;
using System;

public class Stats : MonoBehaviour
{
	public float Lv;
	public float Xp;
	public float cHP;
	public float mHP;
	public float Str;
	public float Mag;
	public float Skl;
	public float Spd;
	public float Lck;
	public float Def;
	public float Res;
	public float Con;
	public float Wt;
	public float Mov;
	public string U_Name;

	public void SetStats(float Lv, float mHP, float Str, float Mag, float Skl, float Spd, float Lck, float Def, float Res, float Con, float Wt, float Mov) {
		this.Lv = Lv;
		this.mHP = mHP;
		this.Str = Str;
		this.Mag = Mag;
		this.Skl = Skl;
		this.Spd = Spd;
		this.Lck = Lck;
		this.Def = Def;
		this.Res = Res;
		this.Con = Con;
		this.Wt = Wt;
		this.Mov = Mov;
		cHP = mHP;
		Xp = 0;
	}

	public void LevelUp(float[] lvStats) {
		Lv++;
		Xp = Xp - 100;

		mHP+=lvStats[0];
		cHP = mHP;
		Str += lvStats [1];
		Mag += lvStats [2];
		Skl += lvStats [3];
		Spd += lvStats [4];
		Lck += lvStats [5];
		Def += lvStats [6];
		Res += lvStats [7];
		//Debug.Log (STR + " " + DEX + " " + INT + " " + HP + " " + DEF);
	}
}
