using UnityEngine;
using System.Collections;

public class DropBricks : MonoBehaviour {

    public GameObject brick1;
    public GameObject brick2;
    public GameObject brick3;
    public GameObject brick4;
    public bool atStart;
    public GameObject sun;
    public bool decompressing;

	// Use this for initialization
	void Start () {
        brick1.GetComponent<Renderer>().enabled = false;
        brick2.GetComponent<Renderer>().enabled = false;
        brick3.GetComponent<Renderer>().enabled = false;
        brick4.GetComponent<Renderer>().enabled = false;
        brick1.GetComponent<Rigidbody>().useGravity = false;
        brick2.GetComponent<Rigidbody>().useGravity = false;
        brick3.GetComponent<Rigidbody>().useGravity = false;
        brick4.GetComponent<Rigidbody>().useGravity = false;
        atStart = true;
        decompressing = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runDropBricks)
        {
            if (atStart)
            {
                sun.GetComponent<SphereCollider>().enabled = false;
                atStart = false;
                GameManager.instance.activeEnemy.GetComponent<Animator>().Play("jump");
                StartCoroutine(dropBrick1());
            }
            if (decompressing)
            {
                if (GameManager.instance.activePlayer.transform.localScale.y >= 1)
                {
                    decompressing = false;
                }
                GameManager.instance.activePlayer.transform.localScale += new Vector3(0.0f, 0.02f, 0.0f);
            }
        }
	}

    public IEnumerator dropBrick1()
    {
        if (GameManager.instance.battleAnimation[3] == -1)
        {
            WriteMovelist.currentMove = "Pyramid Scheme";
        }
        else
        {
            WriteMovelist.currentMove = "Lay the Foundation";
        }
        yield return new WaitForSeconds(1.2f);
        brick1.GetComponent<Renderer>().enabled = true;
        brick1.GetComponent<Rigidbody>().useGravity = true;
        GameManager.instance.activePlayer.GetComponent<Collider>().enabled = false;
        StartCoroutine(reactBrick1());
    }

    public IEnumerator reactBrick1()
    {
        yield return new WaitForSeconds(.6f);
        GameManager.instance.activePlayer.transform.localScale -= new Vector3(0.0f, .9f, 0.0f);
        if (GameManager.instance.battleAnimation[3] == -1)
        {
            StartCoroutine(pyramidScheme());
        }
        else if (GameManager.instance.battleAnimation[3] == 0)
        {
            // GO TO END CODE
        }
        else if (GameManager.instance.battleAnimation[3] == 2)
        {
            StartCoroutine(dropBrick2());
        }
    }

    public IEnumerator dropBrick2()
    {
        yield return new WaitForSeconds(1.2f);
        // "remove" block
        brick1.transform.Translate(500.0f, 500.0f, 500.0f);
        // unflatten character
        decompressing = true;
        yield return new WaitForSeconds(.6f);
        WriteMovelist.currentMove = "Lay the Foundation x2";
        if (GameManager.instance.battleAnimation[4] != 1)
        {
            GameManager.instance.activeEnemy.GetComponent<Animator>().Play("jump", -1, 0f);
            yield return new WaitForSeconds(1.2f);
            brick2.GetComponent<Renderer>().enabled = true;
            brick2.GetComponent<Rigidbody>().useGravity = true;
            // GO TO END
        }
        else
        {
            brick2.transform.Translate(-1.0f, 0.0f, 0.0f);
            GameManager.instance.activeEnemy.GetComponent<Animator>().Play("jump", -1, 0f);
            yield return new WaitForSeconds(1.2f);
            brick2.GetComponent<Renderer>().enabled = true;
            brick2.GetComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(.8f);
            GameManager.instance.activePlayer.transform.localScale -= new Vector3(0.0f, .9f, 0.0f);
            yield return new WaitForSeconds(1.5f);
            brick2.transform.Translate(500.0f, 500.0f, 500.0f);
            if (GameManager.instance.battleAnimation[6] == -1)
            {
                // GO TO END, CHARACTER REMAINS FLATTENED
            }
            else if (GameManager.instance.battleAnimation[6] == 0)
            {
                decompressing = true;
            }
        }
        
    }

    public IEnumerator pyramidScheme()
    {
        yield return new WaitForSeconds(.6f);
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("jump", -1, 0f);
        yield return new WaitForSeconds(1.2f);
        brick2.GetComponent<Renderer>().enabled = true;
        brick2.GetComponent<Rigidbody>().useGravity = true;
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("jump", -1, 0f);
        yield return new WaitForSeconds(1.2f);
        brick3.GetComponent<Renderer>().enabled = true;
        brick3.GetComponent<Rigidbody>().useGravity = true;
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("jump", -1, 0f);
        yield return new WaitForSeconds(1.2f);
        brick4.GetComponent<Renderer>().enabled = true;
        brick4.GetComponent<Rigidbody>().useGravity = true;
    }
}
