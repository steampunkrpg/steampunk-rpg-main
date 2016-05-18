using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    private GameObject sourceGO;

    void Start()
    {
        sourceGO = gameObject;
        if (!sourceGO.GetComponent<AudioSource>().isPlaying)
        {
            sourceGO.GetComponent<AudioSource>().Play();
        }
        DontDestroyOnLoad(gameObject);
    }
}