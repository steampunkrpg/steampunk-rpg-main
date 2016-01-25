using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    KeyboardController keyboardController;
    public GameObject playerGO;


	// Use this for initialization
	void Start () {

        // todo - move controller creation to a controller factory?
        // initialize controller
        keyboardController = new KeyboardController(playerGO);

    }
	
	// Update is called once per frame
	void Update () {

        // update controllers for mouse, keyboard, gamepad
        keyboardController.Update();

    }
}
