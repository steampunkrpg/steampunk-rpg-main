using UnityEngine;
using System.Collections;

public class InitiateBattle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onMouseDown()
    {
        Application.LoadLevel("Battle View");
    }
}
