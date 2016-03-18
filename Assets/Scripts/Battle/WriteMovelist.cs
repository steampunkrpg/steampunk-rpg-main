using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteMovelist : MonoBehaviour {

    //public GameObject canvas;
    public Text attack1;
    public Text attack2;
    Font sans;

	// Use this for initialization
	void Start () {
        sans = Resources.Load("Comic Sans MS", typeof(Font)) as Font;
        attack1.text = "Sonic Phantom";
        attack2.text = "Deep Six";
        attack2.font = (Font)Resources.Load("911Fonts.com_ComicSansMSRegular__-_911fonts.com-fonts-BpCS");
        attack1.font = (Font)Resources.Load("True Lies");
        attack1.fontSize = 20;
        attack2.fontSize = 20;
        //attack2.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
