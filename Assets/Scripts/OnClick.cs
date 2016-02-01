using UnityEngine;
using System.Collections;

public class OnClick : MonoBehaviour {

	public void LoadScene(string scene_name) {
		Application.LoadLevel (scene_name);
	}

	public void QuitScene() {
		Application.Quit ();
	}
}
