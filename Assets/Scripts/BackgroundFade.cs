using UnityEngine;
using System.Collections;

public class BackgroundFade : MonoBehaviour {

    public Texture2D background;
    public Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        Invoke("FadeIn", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FadeIn()
    {
        rend.material.SetTexture("_BumpMap", background);
    }
}
