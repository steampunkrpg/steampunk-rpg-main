using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingBehavior : MonoBehaviour {

    public Canvas[] loadingView;

	// Use this for initialization
	void Start ()
    {
        loadingView[1].enabled = false;
        InvokeRepeating("ToggleViews", 4, 4);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //InvokeRepeating("ToggleViews", 4, 4);
	}

    void ToggleViews()
    {
        if (loadingView[0].enabled)
        {
            loadingView[0].enabled = false;
            loadingView[1].enabled = true;
        } else
        {
            loadingView[0].enabled = true;
            loadingView[1].enabled = false;
        }
    }
}
