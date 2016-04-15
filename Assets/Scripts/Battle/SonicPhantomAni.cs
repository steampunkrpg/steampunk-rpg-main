using UnityEngine;
using System.Collections;

public class SonicPhantomAni : MonoBehaviour {

    public GameObject player;
    public Animator animPlayer;
    public Animator animEnemy;
    public bool inMotion;
    public bool endTime;
    public ParticleSystem ray1;
    public ParticleSystem ray2;
    public ParticleSystem ray3;
    public ParticleSystem ray4;
    public ParticleSystem ray5;
    public bool activeRay;
    public bool activeRay2;
    public bool activeRay3;
    public bool activeRay4;
    public bool activeRay5;

	// Use this for initialization
	void Start () {
        inMotion = false;
        ray1.Pause();
        ray2.Pause();
        activeRay = false;
        activeRay2 = false;
        activeRay3 = false;
        activeRay4 = false;
        activeRay5 = false;
        ray1.GetComponent<Renderer>().enabled = false;
        ray2.GetComponent<Renderer>().enabled = false;
        ray3.GetComponent<Renderer>().enabled = false;
        ray4.GetComponent<Renderer>().enabled = false;
        ray5.GetComponent<Renderer>().enabled = false;
        endTime = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (BattleCommands.runSonicPhantom)
        {
            WriteMovelist.currentMove = "Sonic Phantom";
            if (!inMotion)
            {
                GameManager.instance.activePlayer.GetComponent<Animator>().Play("Jamp");
                inMotion = true;
                StartCoroutine(characterMotion());
            }
            if (activeRay)
            {
                ray1.startSize -= .05f;
                StartCoroutine(sonic1());
                StartCoroutine(enemyReaction());
            }
            if (activeRay2)
            {
                ray2.startSize -= .05f;
                StartCoroutine(sonic2());
            }
            if (activeRay3)
            {
                ray3.startSize -= .05f;
                StartCoroutine(sonic3());
            }
            if (activeRay4)
            {
                if (ray4.startSize <= 6)
                {
                    ray4.startSize += .05f;
                }
                else if (activeRay4)
                {
                    activeRay = false;
                    activeRay2 = false;
                    activeRay3 = false;
                    activeRay4 = false;
                    if (!endTime)
                    {
                        StartCoroutine(endAni());
                        StartCoroutine(returnStart());
                        endTime = true;
                    }

                }
            }
        }
        
	}

    IEnumerator characterMotion()
    {
        yield return new WaitForSeconds(.7f);
        GameManager.instance.activePlayer.transform.Translate(new Vector3(0.0f, 5.0f, 0.0f));
        // ray1.transform.Translate(new Vector3(0.0f, -5.0f, 0.0f));
        ray1.GetComponent<Renderer>().enabled = true;
        ray1.Play();
        activeRay = true;
    }

    IEnumerator sonic1()
    {
        yield return new WaitForSeconds(.5f);
        ray2.GetComponent<Renderer>().enabled = true;
        ray2.Play();
        activeRay2 = true;
    }

    IEnumerator enemyReaction()
    {
        yield return new WaitForSeconds(.8f);
        animEnemy.Play("break_through_the_block");
        animEnemy.enabled = false;
    }

    IEnumerator sonic2()
    {
        yield return new WaitForSeconds(.6f);
        ray3.GetComponent<Renderer>().enabled = true;
        ray3.Play();
        activeRay3 = true;
    }

    IEnumerator sonic3()
    {
        yield return new WaitForSeconds(1.2f);
        if (!endTime)
        {
            ray4.GetComponent<Renderer>().enabled = true;
        }
        ray4.Play();
        activeRay4 = true;
    }

    IEnumerator endAni()
    {
        yield return new WaitForSeconds(1.0f);
        animEnemy.enabled = true;
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Dead");
        ray4.GetComponent<Renderer>().enabled = false;
    }

    IEnumerator returnStart()
    {
        yield return new WaitForSeconds(2.0f);
        ray4.GetComponent<Renderer>().enabled = false;
        ray5.GetComponent<Renderer>().enabled = true;
        ray5.Play();
        yield return new WaitForSeconds(1.0f);
        ray5.GetComponent<Renderer>().enabled = false;
        GameManager.instance.activePlayer.transform.Translate(new Vector3(0.0f, -5.0f, 0.0f));
    }
}
