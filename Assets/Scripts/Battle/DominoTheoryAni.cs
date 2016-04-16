using UnityEngine;
using System.Collections;

public class DominoTheoryAni : MonoBehaviour {

    public GameObject domino1;
    public GameObject domino2;
    public GameObject domino3;
    public GameObject domino4;
    public GameObject domino5;
    public GameObject domino6;
    public bool atStart;
    public bool dom1rising;
    public bool dom2rising;
    public bool dom3rising;
    public bool dom4rising;
    public bool dom5rising;
    public bool dom6rising;
    public bool decompressing;

    // Use this for initialization
    void Start () {
        domino1.GetComponent<Rigidbody>().useGravity = false;
        domino2.GetComponent<Rigidbody>().useGravity = false;
        domino3.GetComponent<Rigidbody>().useGravity = false;
        domino4.GetComponent<Rigidbody>().useGravity = false;
        domino5.GetComponent<Rigidbody>().useGravity = false;
        domino6.GetComponent<Rigidbody>().useGravity = false;
        domino1.GetComponent<BoxCollider>().enabled = false;
        domino2.GetComponent<BoxCollider>().enabled = false;
        domino3.GetComponent<BoxCollider>().enabled = false;
        domino4.GetComponent<BoxCollider>().enabled = false;
        domino5.GetComponent<BoxCollider>().enabled = false;
        domino6.GetComponent<BoxCollider>().enabled = false;
        dom1rising = false;
        dom2rising = false;
        dom3rising = false;
        dom4rising = false;
        dom5rising = false;
        dom6rising = false;
        atStart = true;
        decompressing = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runDominoTheory)
        {
            if (atStart)
            {
                atStart = false;
                StartCoroutine(raiseDominoes());
            }
            if (dom1rising)
            {
                if (domino1.transform.position.y >= 1.77)
                {
                    dom1rising = false;
                }
                domino1.transform.Translate(new Vector3(0.0f, .04f, 0.0f));
            }
            if (dom2rising)
            {
                if (domino2.transform.position.y >= 1.77)
                {
                    dom2rising = false;
                }
                domino2.transform.Translate(new Vector3(0.0f, .04f, 0.0f));
            }
            if (dom3rising)
            {
                if (domino3.transform.position.y >= 1.77)
                {
                    dom3rising = false;
                }
                domino3.transform.Translate(new Vector3(0.0f, .04f, 0.0f));
            }
            if (dom4rising)
            {
                if (domino4.transform.position.y >= 1.77)
                {
                    dom4rising = false;
                }
                domino4.transform.Translate(new Vector3(0.0f, .04f, 0.0f));
            }
            if (dom5rising)
            {
                if (domino5.transform.position.y >= 1.77)
                {
                    dom5rising = false;
                }
                domino5.transform.Translate(new Vector3(0.0f, .04f, 0.0f));
            }
            if (dom6rising)
            {
                if (domino6.transform.position.y >= 2.9)
                {
                    dom6rising = false;
                }
                domino6.transform.Translate(new Vector3(0.0f, .04f, 0.0f));
            }
            if (decompressing)
            {
                if (GameManager.instance.activeEnemy.transform.localScale.y >= 1)
                {
                    decompressing = false;
                }
                GameManager.instance.activeEnemy.transform.localScale += new Vector3(0.0f, 0.02f, 0.0f);
            }
        }
	}

    public IEnumerator raiseDominoes()
    {
        WriteMovelist.currentMove = "Domino Theory";
        dom1rising = true;
        yield return new WaitForSeconds(.7f);
        dom2rising = true;
        yield return new WaitForSeconds(.7f);
        dom3rising = true;
        yield return new WaitForSeconds(.7f);
        dom4rising = true;
        yield return new WaitForSeconds(.7f);
        dom5rising = true;
        yield return new WaitForSeconds(.7f);
        dom6rising = true;
        yield return new WaitForSeconds(1.2f);
        GameManager.instance.activePlayer.GetComponent<Animator>().Play("Attack(2)");
        StartCoroutine(pushDominoes());
    }

    public IEnumerator pushDominoes()
    {
        yield return new WaitForSeconds(.5f);
        domino1.GetComponent<Rigidbody>().useGravity = true;
        domino2.GetComponent<Rigidbody>().useGravity = true;
        domino3.GetComponent<Rigidbody>().useGravity = true;
        domino4.GetComponent<Rigidbody>().useGravity = true;
        domino5.GetComponent<Rigidbody>().useGravity = true;
        domino6.GetComponent<Rigidbody>().useGravity = true;
        domino1.GetComponent<BoxCollider>().enabled = true;
        domino2.GetComponent<BoxCollider>().enabled = true;
        domino3.GetComponent<BoxCollider>().enabled = true;
        domino4.GetComponent<BoxCollider>().enabled = true;
        domino5.GetComponent<BoxCollider>().enabled = true;
        domino6.GetComponent<BoxCollider>().enabled = true;
        if (GameManager.instance.battleAnimation[1] != 1)
        {
            domino6.GetComponent<Rigidbody>().mass = 6000;
        }
        domino1.GetComponent<Rigidbody>().AddForce(new Vector3(30.0f, 0.0f, 0.0f));
        StartCoroutine(enemyReact());
    }

    public IEnumerator enemyReact()
    {
        yield return new WaitForSeconds(3.9f);
        if (GameManager.instance.battleAnimation[1] == 1)
        {
            GameManager.instance.activeEnemy.transform.localScale -= new Vector3(0.0f, .9f, 0.0f);
        }
        yield return new WaitForSeconds(1.1f);
        domino1.transform.Translate(500.0f, 500.0f, 500.0f);
        yield return new WaitForSeconds(.6f);
        domino2.transform.Translate(500.0f, 500.0f, 500.0f);
        yield return new WaitForSeconds(.6f);
        domino3.transform.Translate(500.0f, 500.0f, 500.0f);
        yield return new WaitForSeconds(.6f);
        domino4.transform.Translate(500.0f, 500.0f, 500.0f);
        yield return new WaitForSeconds(.6f);
        domino5.transform.Translate(500.0f, 500.0f, 500.0f);
        yield return new WaitForSeconds(.6f);
        domino6.transform.Translate(500.0f, 500.0f, 500.0f);
        yield return new WaitForSeconds(.3f);
        decompressing = true;
    }
}
