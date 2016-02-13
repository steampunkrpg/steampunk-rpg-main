using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private BoardManager boardScript;

	public List<HexTile> hexGrid;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager> ();
		hexGrid = new List<HexTile> ();

		InitGame ();
	}

	public void InitGame() {
		boardScript.SetupScene ();
		foreach (HexTile tile in hexGrid) {
			tile.FindNeighbors ();
		}
	}

	public void AddTileToList(HexTile tile) {
		hexGrid.Add (tile);
	}
}
