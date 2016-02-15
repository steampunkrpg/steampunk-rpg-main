using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class WorldMapScript : MonoBehaviour {

	public GameObject loader;
	public Slider loadingBar;
	private AsyncOperation async;			
	
	
	public void LoadScene(string scene) 
		
	{
		loader.SetActive(true);
		StartCoroutine(LoadLevelWithBar(scene));
	}

	IEnumerator LoadLevelWithBar (string sceneName)
	{
		async = Application.LoadLevelAsync(sceneName);
		while (!async.isDone)
		{
			loadingBar.value = async.progress;
			yield return null;
		}
	}
	
}