using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour
{
	public string cName;
	public string statAffected;
	public float modifier;

	public Consumable(string cName, string statAffected, float modifier) {
		this.cName = cName;
		this.statAffected = statAffected;
		this.modifier = modifier;
	}
}
