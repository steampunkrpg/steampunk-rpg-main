using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Item : MonoBehaviour
{
	public int iType;
	public string iName;
	public float Mt;
	public float Hit;
	public float Crit;
	public float Wt;
	public List<float> Rng;
	public float type;

	//consumable overload
	public Item(int iType, string iName){
		this.iType = iType;
		this.iName = iName;
	}

	//weapon overload
	public Item(int iType, string iName, float Mt, float Hit, float Crit, float Wt, List<float> Rng,float type) {
		this.iType = iType;
		this.iName = iName;
		this.Mt = Mt;
		this.Hit = Hit;
		this.Crit = Crit;
		this.Wt = Wt;
		this.Rng = Rng;
		this.type = type;
	}
}
