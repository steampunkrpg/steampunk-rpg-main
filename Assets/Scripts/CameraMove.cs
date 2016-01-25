using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           transform.Translate(-.01f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, .01f, 0);
        }
	    else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(.01f, 0, 0);
        } 
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -.01f, 0);
        }
	}
}
