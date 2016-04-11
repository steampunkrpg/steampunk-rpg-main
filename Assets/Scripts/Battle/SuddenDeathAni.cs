using UnityEngine;
using System.Collections;

public class SuddenDeathAni : MonoBehaviour {

    public GameObject suddenDeathCurtain;
    public bool dead;

	// Use this for initialization
	void Start () {
        suddenDeathCurtain.GetComponent<Renderer>().enabled = false;
        dead = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runSuddenDeath)
        {
            if (suddenDeathCurtain.GetComponent<Renderer>().enabled == false)
            {
                suddenDeathCurtain.GetComponent<Renderer>().enabled = true;
            }
            WriteMovelist.currentMove = "Sudden Death";
            
            suddenDeathCurtain.transform.Translate(new Vector3((-1 * Time.deltaTime), 0.0f,  0.0f));
            if (suddenDeathCurtain.transform.position.y <= 5 && !dead)
            {
                dead = true;
                GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Dead");
                GameManager.instance.activePlayer.GetComponent<Animator>().Play("Attack (2)");
            }
        }
	}
}
