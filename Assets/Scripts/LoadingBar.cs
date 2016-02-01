using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {

	public Image progressBar;
	private float maxTime = 8f;

	// Use this for initialization
	void Start () {
		progressBar.fillAmount = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		progressBar.fillAmount += Time.deltaTime / maxTime;
	}
}
