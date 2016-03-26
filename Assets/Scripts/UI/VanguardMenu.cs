using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VanguardMenu : MonoBehaviour {

	public Text charInfo;
	public Text level;
	public Stats charStats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitializeMenu() {
		charInfo.text = "Name\nHealth: " + charStats.cHP + "/" + charStats.mHP;
		level.text = "Lv " + charStats.Lv;
		this.gameObject.SetActive (false);
	}

	public void OpenMenu() {
		this.gameObject.SetActive (true);
	}

	public void CloseMenu() {
		this.gameObject.SetActive (false);
	}
}
