using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private GameObject[] GOs;

	// Use this for initialization
	void Start () {
		GOs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
	}

	void FixedUpdate () {
		foreach (GameObject o in GOs) {
			if (o.tag == "Unit" || o.tag == "Enemy"){
				Vector3 pos = o.transform.position;
				pos.y = Terrain.activeTerrain.SampleHeight(o.transform.position) + (o.transform.lossyScale.y/2f);
				o.transform.position = pos;
			}
		}
	}
}
