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
        origPos = player.transform.localScale;
        hit = false;
         }
 	
 	// Update is called once per frame
 	void Update()
    {
                if (BattleCommands.runJotun)
                    {
                        if (player.transform.localScale.x <= 8.6f)
                            {
                player.transform.localScale += Vector3.one * Time.deltaTime;
                            }
                        if (player.transform.localScale.x >= 1.8f)
                            {
                cam2.enabled = true;
                cam1.enabled = false;
                            }
                        if (player.transform.localScale.x >= 8.5f)
                            {
                animPlayer.Play("Attack(4)");
                                if (!hit)
                                    {
                    StartCoroutine(enemyReact());
                                    }
                            }
                    }
         }
 
     IEnumerator enemyReact()
     {
         yield return new WaitForSeconds(.5f);
         animEnemy.Play("Get hit");
         hit = true;
         yield return new WaitForSeconds(1.0f);
         animEnemy.Play("Idel");
     }
 }