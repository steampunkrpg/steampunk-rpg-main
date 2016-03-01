using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
	public HexTile tile;
	public bool Active;
	public bool moving;

	public GameObject char_class;
	public GameObject ui;
	public Text statsView;
	public Image health;
	public Inventory inv;
	public Stats char_stats;
	public float movement;

	void Awake() {
		char_stats = this.GetComponentInChildren<Stats> ();
		this.GetComponentInChildren<ParticleSystem> ().Stop (true);
		Active = false;
	}

	public void InitPosition() {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
		movement = char_stats.MOV;
		statsView.text = "STATS\nSTR: " + char_stats.STR + "\t\tHP: " + char_stats.HP + "\nDEX: " + char_stats.DEX + "\t\tDEF: " + char_stats.DEF + "\nINT: " + char_stats.INT + "\t\tMOV: " + char_stats.MOV;
		health.fillAmount = 1f;
		ui.SetActive (false);
	}

	bool ValidMove(int dir) {
		switch (dir) {
		case 1:
			return (tile.NW_Tile != null && tile.NW_Tile.character == null && movement >= tile.NW_Tile.mov_cost);
		case 2:
			return (tile.NE_Tile != null && tile.NE_Tile.character == null&& movement >= tile.NE_Tile.mov_cost);
		case 3:
			return (tile.E_Tile != null && tile.E_Tile.character == null && movement >= tile.E_Tile.mov_cost);
		case 4:
			return (tile.SE_Tile != null && tile.SE_Tile.character == null && movement >= tile.SE_Tile.mov_cost);
		case 5:
			return (tile.SW_Tile != null && tile.SW_Tile.character == null && movement >= tile.SW_Tile.mov_cost);
		case 6:
			return (tile.W_Tile != null && tile.W_Tile.character == null && movement >= tile.W_Tile.mov_cost);
		default:
			return false;
		}
	}

	public void ActivateUI(){
		if (Input.GetMouseButtonDown (1)) {
			if (ui.activeSelf) {
				ui.SetActive (false);
			} else if (!ui.activeSelf) {
				ui.SetActive (true);
			}
		}
	}

	public void Move(int dir) {
		bool validMove = ValidMove (dir);
		if (validMove && movement != 0) {
			tile.character = null;

			switch (dir) {
			case 1:
				tile.NW_Tile.character = this.gameObject;
				tile = tile.NW_Tile;
				break;
			case 2:
				tile.NE_Tile.character = this.gameObject;
				tile = tile.NE_Tile;
				break;
			case 3:
				tile.E_Tile.character = this.gameObject;
				tile = tile.E_Tile;
				break;
			case 4:
				tile.SE_Tile.character = this.gameObject;
				tile = tile.SE_Tile;
				break;
			case 5:
				tile.SW_Tile.character = this.gameObject;
				tile = tile.SW_Tile;
				break;
			case 6:
				tile.W_Tile.character = this.gameObject;
				tile = tile.W_Tile;
				break;
			default:
				break;
			}

			moving = true;
			movement = movement - tile.mov_cost;
		}

		if (movement == 0) {
			Active = false;
		}
	}

	void Update() {
		if ((Active || moving) && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				moving = false;
			}
		}
		ActivateUI ();
		health.fillAmount = (float)char_stats.HP / 5f;
	}

	public void DontDestroy() {
		DontDestroyOnLoad (this);
	}

	public void Death() {
		Destroy (this);
	}

	public void ResetMovement() {
		movement = char_stats.MOV;
	}
}
