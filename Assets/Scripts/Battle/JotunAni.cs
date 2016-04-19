using UnityEngine;
using System.Collections;
 
 public class JotunAni : MonoBehaviour
{
 
    public GameObject player;
    public Camera cam1;
    public Camera cam2;
    public Animator animPlayer;
    public Animator animEnemy;
    public Vector3 origPos;
    public bool hit;
 
 	// Use this for initialization
 	void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        origPos = GameManager.instance.activePlayer.transform.localScale;
        hit = false;
    }
 	
 	// Update is called once per frame
 	void Update() {
		if (BattleCommands.runJotun) {
			WriteMovelist.currentMove = "Jotun";
			if (GameManager.instance.activePlayer.transform.localScale.x <= 8.6f) {
                GameManager.instance.activePlayer.transform.localScale += Vector3.one * Time.deltaTime;
			}
			if (GameManager.instance.activePlayer.transform.localScale.x >= 1.8f) {
				cam2.enabled = true;
				cam1.enabled = false;
			}
			if (GameManager.instance.activePlayer.transform.localScale.x >= 8.5f) {
				GameManager.instance.activePlayer.GetComponent<Animator> ().Play ("Attack(4)");
				if (!hit) {
					StartCoroutine (enemyReact ());
				}
			}
		}
	}
 
	IEnumerator enemyReact() {
		hit = true;
		yield return new WaitForSeconds(.5f);
		GameManager.instance.activeEnemy.transform.localScale = new Vector3(1.0f, .1f, 1.0f);
		//GameManager.instance.activeEnemy.GetComponent<Animator>().Play("break_through_the_block");
		yield return new WaitForSeconds(1.0f);

		AllDone ();
		//GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Idle");
	}

	private void AllDone() {
        GameManager.instance.activePlayer.transform.localScale = origPos;
		GameManager.instance.LoadScene (GameManager.instance.level);
	}
}