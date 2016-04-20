using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour {

    public bool storyContinue;

	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (storyContinue)
        {
            this.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            storyContinue = true;
        }
	}

}
