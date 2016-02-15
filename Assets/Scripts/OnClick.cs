using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class OnClick : MonoBehaviour {

	public Slider loadingBar;
	public GameObject loadingImage;
	public GameObject buttonImage;

	private AsyncOperation async;

	public void ClickAsync(string sceneName)
	{
		buttonImage.SetActive (false);
		loadingImage.SetActive(true);
		StartCoroutine(LoadLevelWithBar(sceneName));
	}


	IEnumerator LoadLevelWithBar (string sceneName)
	{
		async = SceneManager.LoadSceneAsync(sceneName);
		while (!async.isDone)
		{
			loadingBar.value = async.progress;
			yield return null;
		}
	}

	public void QuitScene() {
		Application.Quit ();
	}
}
