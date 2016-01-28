using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GameObject mainCamera;
	private float scrollingSpeed = 2.5f;
	private static float LENGTH_OF_CREDITS = 65f;

	void Start() {
		StartCoroutine ("endOfCredits");
	}
	
	void Update () {	
		mainCamera.transform.Translate (Vector3.down * Time.deltaTime * scrollingSpeed);
	}

	//At end of credits, stops music, opens course webpage and quits application 
	IEnumerator endOfCredits(){
		yield return new WaitForSeconds (LENGTH_OF_CREDITS);
		gameObject.GetComponent<AudioSource> ().Stop ();
		Application.OpenURL ("http://web.cse.ohio-state.edu/~boggus/5912/");
		Application.Quit ();		
	}
}
