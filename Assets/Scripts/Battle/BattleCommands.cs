using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleCommands : MonoBehaviour {

    public RawImage arrow1;
    //public RawImage arrow2;
    public bool onFirst;
    public GameObject commandMenu;
    public GameObject commandBackground;
    public static bool runDeepSix = false;
    public static bool runGrittySlap = false;
    public static bool runSolarShot = false;
    public static bool runSonicPhantom = false;
    public static bool runJotun = false;
    public static bool runSuddenDeath = false;
    public bool walking = false;
    public Text damageText;
    DrawDamage damageWriter;

	// Use this for initialization
	void Start () {
        arrow1.enabled = true;
        onFirst = true;
        runDeepSix = false;
        damageWriter = new DrawDamage(damageText);
        GameManager.instance.activePlayer.transform.position = new Vector3(-7.55f, 1f, 1.5f);
        GameManager.instance.activeEnemy.transform.position = new Vector3(0.0f, 1.0f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!walking)
        {
            walking = true;
            //commandMenu.transform.Translate(new Vector3(0.0f, -200.0f, 0.0f));
            //commandBackground.transform.Translate(new Vector3(0.0f, -200.0f, 0.0f));
            if (onFirst)
            {
                Debug.Log("onFirst");
                ShakeScreen.timeElapsed = 0.0f;
                StartCoroutine(AnimationMenuMove(5.5f));
                //runGrittySlap = true;
                //runSolarShot = true;
                //runSonicPhantom = true;
                //runJotun = true;
                runSuddenDeath = true;
            }
            else
            {
                Debug.Log("onSecond");
                ShakeScreen.timeElapsed = 0.0f;
                StartCoroutine(AnimationMenuMove(5.0f));
                runDeepSix = true;
            }
        }
	}
    IEnumerator AnimationMenuMove(float waitSeconds)
    {
        Debug.Log("Inside Animation");
        yield return new WaitForSeconds(waitSeconds);
        Debug.Log("Done waiting");
        runDeepSix = false;
        StartCoroutine(damageWriter.CoDrawDamage());
    }
}
