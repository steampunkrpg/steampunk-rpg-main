using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour {

	private AudioSource musicPlayer;
	private AudioClip clip;
	private GameObject musicSource, musicSourceGO;
	private Scene scene;
	private string sceneName;

	void PlayMusic () {
		scene = SceneManager.GetActiveScene ();
		sceneName = scene.name;

		musicSource = (GameObject)Resources.Load ("Prefabs/MusicPlayer", typeof(GameObject));
		musicSourceGO = Instantiate (musicSource) as GameObject;
		musicSourceGO.transform.parent = GameObject.Find ("GameManager(Clone)").transform;
		musicPlayer = musicSourceGO.GetComponent<AudioSource> ();

		/*
		switch (sceneName){
		case "New_Menu_Scene":
			clip = 
			playedMusic.clip = 
		
		}
		*/
	}
}
