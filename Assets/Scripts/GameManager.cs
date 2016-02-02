using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private List<Unit> units;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		units = new List<Unit> ();
	}

	public void InitGame() {

	}

    private KeyboardController keyboardController;
    public GameObject currentUnit;

    // Use this for initialization
    void Start()
    {

        // todo - move controller creation to a controller factory?
        // initialize controller
        keyboardController = new KeyboardController(currentUnit);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.R))
        {
            currentUnit.GetComponent<Unit>().SetUp(); 
        }

        // update controllers for mouse, keyboard, gamepad
        keyboardController.Update();

    }

}
