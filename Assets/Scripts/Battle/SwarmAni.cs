using UnityEngine;
using System.Collections;

public class SwarmAni : MonoBehaviour {

    public bool atStart;

	// Use this for initialization
	void Start () {
        atStart = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runSwarm)
        {
            WriteMovelist.currentMove = "SWARM!";
            if (atStart)
            {
                atStart = false;
                Instantiate(GameManager.instance.activeEnemy, new Vector3(0.0f, 1.0f, 2.0f), new Quaternion(0.0f, 90.0f, 0.0f, -90.0f));
            }
        }
	}
}
