using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingBehavior : MonoBehaviour {

    public Canvas[] loadingView;
	private float currentTime;
	private float loadTime = 8f;

	// Use this for initialization
	void Start ()
    {
        loadingView[1].enabled = false;
		loadingView [2].enabled = false;
		currentTime = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (currentTime >= 0.5f && currentTime < 1f) {
			loadingView [0].enabled = false;
			loadingView [1].enabled = true;
			currentTime += Time.deltaTime / loadTime;
		} else if (currentTime > 1f) {
			loadingView [1].enabled = false;
			loadingView [2].enabled = true;
		} else {
			currentTime += Time.deltaTime / loadTime;
		}

        //InvokeRepeating("ToggleViews", 4, 4);
	}

//    void ToggleViews()
//    {
//        if (loadingView[0].enabled)
//        {
//            loadingView[0].enabled = false;
//            loadingView[1].enabled = true;
//        } else
//        {
//            loadingView[0].enabled = true;
//            loadingView[1].enabled = false;
//        }
//    }
}
