using UnityEngine;
using System.Collections;
using System;

public class Stat : MonoBehaviour
{
	public string statName;
	public int value;
	
	public Stat(string newName, int newValue)
	{
		statName = newName;
		value = newValue;
	}
}