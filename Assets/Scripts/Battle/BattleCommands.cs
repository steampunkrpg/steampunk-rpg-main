using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleCommands : MonoBehaviour {

    public RawImage arrow1;
    public RawImage arrow2;
    public bool onFirst;
    public GameObject commandMenu;
    public GameObject commandBackground;
    public static bool runDeepSix = false;
    public Text damageText;
    DrawDamage damageWriter;

	// Use this for initialization
	void Start () {
        arrow1.enabled = true;
        arrow2.enabled = false;
        onFirst = true;
        runDeepSix = false;
        damageWriter = new DrawDamage(damageText);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (onFirst)
            {
                onFirst = false;
                arrow1.enabled = false;
                arrow2.enabled = true;
            }
            else
            {
                onFirst = true;
                arrow2.enabled = false;
                arrow1.enabled = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            commandMenu.transform.Translate(new Vector3(0.0f, -200.0f, 0.0f));
            commandBackground.transform.Translate(new Vector3(0.0f, -200.0f, 0.0f));
            if (onFirst)
            {
                Debug.Log("onFirst");
                ShakeScreen.timeElapsed = 0.0f;
                StartCoroutine(DeepSixAnimation());
                runDeepSix = true;
            }
            else
            {
                Debug.Log("onSecond");
                StartCoroutine(DeepSixAnimation());
            }
        }
	}
    IEnumerator DeepSixAnimation()
    {
        Debug.Log("Inside Animation");
        yield return new WaitForSeconds(5);
        Debug.Log("Done waiting");
        commandMenu.transform.Translate(new Vector3(0.0f, 200.0f, 0.0f));
        commandBackground.transform.Translate(new Vector3(0.0f, 200.0f, 0.0f));
        runDeepSix = false;
        StartCoroutine(damageWriter.CoDrawDamage());
    }
}
