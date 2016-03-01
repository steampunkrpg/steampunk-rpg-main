using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{
	public float crit_rate;
	public float hit_perc;
	public float range;
	public float weap_effectiveness;
	public float might;
	public float type;
	public float weight;

	public void SetStats(float crit_rate, float hit_perc, float range, float weap_effectiveness, float might, float type, float weight) {
		this.crit_rate = crit_rate;
		this.hit_perc = hit_perc;
		this.range = range;
		this.weap_effectiveness = weap_effectiveness;
		this.might = might;
		this.type = type;
		this.weight = weight;
	}
}
