using UnityEngine;
using System.Collections;

public class EnemyDamaged : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runGrittySlap)
        {
            anim.SetTrigger("EnemyHit");
        }
	}
}
