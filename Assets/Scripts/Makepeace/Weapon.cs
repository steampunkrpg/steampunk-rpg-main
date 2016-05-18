using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Weapon : MonoBehaviour
{
	public string wName;
	public float Mt;
	public float Hit;
	public float Crit;
	public float Wt;
	public List<float> Rng;
	public float type;

	public Weapon(string wName, float Mt, float Hit, float Crit, float Wt, List<float> Rng,float type) {
		this.wName = wName;
		this.Mt = Mt;
		this.Hit = Hit;
		this.Crit = Crit;
		this.Wt = Wt;
		this.Rng = Rng;
		this.type = type;
	}
}
