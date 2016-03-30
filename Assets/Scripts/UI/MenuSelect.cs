using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selectOption(int nextState){
		GameManager.instance.activePlayer.Status = nextState;
		//0: inactive
		//1: player is active
		//2: moving
		//3: attacking
		//4: death (not needed)
		//5: interaction
	}

}
