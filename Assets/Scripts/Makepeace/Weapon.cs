using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Weapon : MonoBehaviour
{
	public float Mt;
	public float Hit;
	public float Crit;
	public float Wt;
	public List<float> Rng;
	public float type;

	public void SetStats(float Mt, float Hit, float Crit, float Wt, List<float> Rng,float type) {
		this.Mt = Mt;
		this.Hit = Hit;
		this.Crit = Crit;
		this.Wt = Wt;
		this.Rng = Rng;
		this.type = type;
	}
}
