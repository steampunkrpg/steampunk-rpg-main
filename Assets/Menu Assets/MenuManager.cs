using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject osuCanvas;
    public GameObject deepManateeCanvas;
    public Canvas menuCanvas;
    public GameObject gui;
    public GameObject menuBackground;
    private float speed = 0.25f;


    // Use this for initialization
    void Start() {
        osuCanvas.SetActive(true);
        deepManateeCanvas.SetActive(false);
        menuCanvas.enabled = false;
        menuBackground.SetActive(false);

        StartCoroutine("InitilizeDeepManatee");
        StartCoroutine("InitilizeMainMenu");
    }
	    
	IEnumerator InitilizeDeepManatee()
    {
        yield return new WaitForSeconds(4);
        osuCanvas.SetActive(false);
        deepManateeCanvas.SetActive(true);
    }

    IEnumerator InitilizeMainMenu()
    {
        yield return new WaitForSeconds(8);
        StartCoroutine(FadeOut(speed, deepManateeCanvas));
        deepManateeCanvas.SetActive(false);
        menuBackground.SetActive(true);
        menuCanvas.enabled = true;
        StartCoroutine(FadeIn(speed, gui));
    }

    public IEnumerator FadeIn(float speed, GameObject go)
    {
        go.GetComponent<CanvasGroup>().alpha = 0;
        while (go.GetComponent<CanvasGroup>().alpha < 1f)
        {
            go.GetComponent<CanvasGroup>().alpha += speed * Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float speed, GameObject go)
    {
        go.GetComponent<CanvasGroup>().alpha = 0;
        while (go.GetComponent<CanvasGroup>().alpha > 0f)
        {
            go.GetComponent<CanvasGroup>().alpha -= speed * Time.deltaTime;
            yield return null;
        }
    }
}
