using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleCommands : MonoBehaviour {

    public RawImage arrow1;
    //public RawImage arrow2;
    public bool onFirst;
    public GameObject commandMenu;
    public GameObject commandBackground;
    // STILL IN PRODUCTION public static bool runDeepSix = false;
    public static bool runGrittySlap = false;
    public static bool runSolarShot = false;
    public static bool runSonicPhantom = false;
    public static bool runJotun = false;
    public static bool runSuddenDeath = false;
    public static bool runSwarm = false;
    public static bool runDropBricks = false;
    public static bool runDominoTheory = false;
    public static bool runScorchingSniper = false;
    // STILL IN PRODUCTION public static bool runThaBigGuy = false;
    public bool walking = false;
    public Text damageText;
    public Text playerDamText;
    public static DrawDamage damageWriter;

	// Use this for initialization
	void Start () {
        arrow1.enabled = true;
        onFirst = true;
        //runDeepSix = false;
        damageWriter = new DrawDamage(damageText, playerDamText);
        GameManager.instance.activePlayer.transform.position = new Vector3(-7.55f, 1f, 1.5f);
        GameManager.instance.activeEnemy.transform.position = new Vector3(0.0f, 1.0f, 1.5f);
        //crosshairs.GetComponent<Renderer>().enabled = false;
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
                // TEST CASES FOR BATTLE ANIMATION
                // #1: Player attacks, enemy instantly dies
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 25;
                GameManager.instance.battleAnimation[3] = -1; */
                // #2: Player attacks, hits, enemy counters, end
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 27;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 14; */
                // #3: Player attacks, hits, enemy counter kills
                /* GameManager.instance.battleAnimation[0] = 1;
                 GameManager.instance.battleAnimation[1] = 1;
                 GameManager.instance.battleAnimation[2] = 27;
                 GameManager.instance.battleAnimation[3] = 2;
                 GameManager.instance.battleAnimation[4] = 1;
                 GameManager.instance.battleAnimation[5] = 102;
                 GameManager.instance.battleAnimation[6] = -1; */
                // #4: Player attacks, misses, enemy counters
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 0;
                GameManager.instance.battleAnimation[2] = 0;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 102;
                GameManager.instance.battleAnimation[6] = 0; */
                // #5: Player attacks, enemy counters, player attacks again
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 25;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 2;
                GameManager.instance.battleAnimation[6] = 1;
                GameManager.instance.battleAnimation[7] = 1;
                GameManager.instance.battleAnimation[8] = 57;*/
                // #6: Enemy attacks, wipes out player
                //GameManager.instance.battleAnimation[0] = 2;
                // #7: Enemy one shots player
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = -1;*/
                // #8: Enemy attacks once and that's all
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 0; */
                // #9: Enemy attacks player twice (ranged), player survives
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 16;*/
                // #10: Enemy attacks player twice (ranged), second kills
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 16;
                GameManager.instance.battleAnimation[6] = -1; */
                // #11: Enemy attacks player twice (ranged), second misses
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 0;
                GameManager.instance.battleAnimation[5] = 0;
                GameManager.instance.battleAnimation[6] = 0; */
                // #12: Enemy attacks player twice (ranged), first misses
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 0;
                GameManager.instance.battleAnimation[2] = 0;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 9;
                GameManager.instance.battleAnimation[6] = 0; */
                //#13: Enemy attacks player twice (ranged), first misses, second kills
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 0;
                GameManager.instance.battleAnimation[2] = 0;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 9;
                GameManager.instance.battleAnimation[6] = -1; */
                //#14: Enemy attacks player twice (ranged), both miss
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 0;
                GameManager.instance.battleAnimation[2] = 0;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 0;
                GameManager.instance.battleAnimation[5] = 0;
                GameManager.instance.battleAnimation[6] = 0; */
                //#15: Enemy attacks once and it misses
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 0;
                GameManager.instance.battleAnimation[2] = 0;
                GameManager.instance.battleAnimation[3] = 0; */
                //#16: Player hits long range, end
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 25;
                GameManager.instance.battleAnimation[3] = 0; */
                //#17: Player misses long range, end
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 0;
                GameManager.instance.battleAnimation[2] = 0;
                GameManager.instance.battleAnimation[3] = 0;*/
                //#18: Player hits twice (long range)
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 25;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 27;*/
                //#19: Player hits first, second misses
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 25;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 0;
                GameManager.instance.battleAnimation[5] = 0; */
                //#20: Player misses both
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 0;
                GameManager.instance.battleAnimation[2] = 0;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 0;
                GameManager.instance.battleAnimation[5] = 0; */
                //#21: Player hits both, second kills
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 25;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 27;
                GameManager.instance.battleAnimation[6] = -1;*/
                //#22: Player hits ranged, enemy counters
                /*GameManager.instance.battleAnimation[0] = 1;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 25;
                GameManager.instance.battleAnimation[3] = 2;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 27;
                GameManager.instance.battleAnimation[6] = -1;*/
                //#23: Enemy attacks, player counters twice, second kills
                /*GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 24;
                GameManager.instance.battleAnimation[6] = 1;
                GameManager.instance.battleAnimation[7] = 1;
                GameManager.instance.battleAnimation[8] = 24;
                GameManager.instance.battleAnimation[9] = -1; */
                //#24: Enemy attacks, player counters twice, first misses, second kills
                GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 0;
                GameManager.instance.battleAnimation[5] = 0;
                GameManager.instance.battleAnimation[6] = 1;
                GameManager.instance.battleAnimation[7] = 1;
                GameManager.instance.battleAnimation[8] = 24;
                GameManager.instance.battleAnimation[9] = -1;
                //#25: Enemy attacks, player counters twice, both hurt
                GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 1;
                GameManager.instance.battleAnimation[6] = 1;
                GameManager.instance.battleAnimation[7] = 1;
                GameManager.instance.battleAnimation[8] = 24;
                GameManager.instance.battleAnimation[9] = 0;
                //#25: Enemy attacks, player counters twice, neither hit
                GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 0;
                GameManager.instance.battleAnimation[5] = 0;
                GameManager.instance.battleAnimation[6] = 1;
                GameManager.instance.battleAnimation[7] = 0;
                GameManager.instance.battleAnimation[8] = 0;
                GameManager.instance.battleAnimation[9] = 0;
                //#26: Enemy attacks, player counters, enemy attacks again
                GameManager.instance.battleAnimation[0] = 2;
                GameManager.instance.battleAnimation[1] = 1;
                GameManager.instance.battleAnimation[2] = 24;
                GameManager.instance.battleAnimation[3] = 1;
                GameManager.instance.battleAnimation[4] = 1;
                GameManager.instance.battleAnimation[5] = 8;
                GameManager.instance.battleAnimation[6] = 2;
                GameManager.instance.battleAnimation[7] = 1;
                GameManager.instance.battleAnimation[8] = 23;

                // player one shots the enemy
                if (GameManager.instance.battleAnimation[0] == 1 && GameManager.instance.battleAnimation[3] == -1)
                {
                    damageWriter.setEnemyDamText(GameManager.instance.battleAnimation[2]);
                    runJotun = true;
                }
                // player attacks enemy, enemy counters
                else if (GameManager.instance.battleAnimation[0] == 1 && GameManager.instance.battleAnimation[3] == 2) { 
                    damageWriter.setEnemyDamText(GameManager.instance.battleAnimation[2]);
                    //runDominoTheory = true;
                    runSolarShot = true;
                }
                else if (GameManager.instance.battleAnimation[0] == 1 && (GameManager.instance.battleAnimation[3] == 0 || GameManager.instance.battleAnimation[3] == 1))
                {
                    runDominoTheory = true;
                }
                else if (GameManager.instance.battleAnimation[0] == 2)
                {
                    runDropBricks = true;
                }
                //runScorchingSniper = true;
                //runDominoTheory = true;
                //runSonicPhantom = true;
                //runJotun = true;
                //runSuddenDeath = true;

                ShakeScreen.timeElapsed = 0.0f;
                //StartCoroutine(AnimationMenuMove(5.5f));
                //runGrittySlap = true;
            }
            else
            {
                Debug.Log("onSecond");
                ShakeScreen.timeElapsed = 0.0f;
                //StartCoroutine(AnimationMenuMove(5.0f));
                //runDeepSix = true;
            }
        }
	}
    /*IEnumerator AnimationMenuMove(float waitSeconds)
    {
        Debug.Log("Inside Animation");
        yield return new WaitForSeconds(waitSeconds);
        Debug.Log("Done waiting");
        runDeepSix = false;
        StartCoroutine(damageWriter.CoDrawDamageEn());
    } */
            }
