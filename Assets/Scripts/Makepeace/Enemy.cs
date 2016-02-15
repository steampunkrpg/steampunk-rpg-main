using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public HexTile tile;

	void Start() {
		tile = null;
	}

	public void InitPosition () {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
	}

	public void MoveEnemy() {

	}

	void Update(){
		
	}
}
