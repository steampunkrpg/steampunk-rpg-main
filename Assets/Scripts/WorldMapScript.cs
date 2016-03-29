using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapScript : MonoBehaviour {

	public GameObject loader;
	public Slider loadingBar;
	private AsyncOperation async;			
	
	
	public void LoadScene(string scene) 
	{
		int level = GameManager.instance.level;

		if ((level == 0 && scene == "Plains_Scene") || (level == 1 && scene == "Desert_Scene") || (level == 2 && scene == "Forest_Scene") || (level == 3 && scene == "Winter_Scene") || (level == 4 && scene == "Lava_Scene")) {
			loader.SetActive (true);
			StartCoroutine (LoadLevelWithBar (scene));
		}
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
	
}