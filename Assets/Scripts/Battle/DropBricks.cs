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
    public static int runNum;

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
        runNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runDropBricks)
        {
            if (atStart)
            {
                runNum++;
                sun.GetComponent<SphereCollider>().enabled = false;
                atStart = false;
                GameManager.instance.activeEnemy.GetComponent<Animator>().Play("jump", -1, 0f);
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
        if (runNum == 1)
        {
            if (GameManager.instance.battleAnimation[1] != 1)
            {
                brick1.transform.Translate(-2.5f, 0.0f, 3f);
                yield return new WaitForSeconds(1.2f);
                brick1.GetComponent<Renderer>().enabled = true;
                brick1.GetComponent<Rigidbody>().useGravity = true;
                //GameManager.instance.activePlayer.GetComponent<Collider>().enabled = false;
                StartCoroutine(reactMiss1());
            }
            else
            {
                yield return new WaitForSeconds(1.2f);
                brick1.GetComponent<Renderer>().enabled = true;
                brick1.GetComponent<Rigidbody>().useGravity = true;
                //GameManager.instance.activePlayer.GetComponent<Collider>().enabled = false;
                StartCoroutine(reactBrick1());
            }
        }
        if (runNum == 2)
        {
            if (GameManager.instance.battleAnimation[7] != 1)
            {
                brick1.transform.Translate(-2.5f, 0.0f, 3f);
                yield return new WaitForSeconds(1.2f);
                brick1.GetComponent<Renderer>().enabled = true;
                brick1.GetComponent<Rigidbody>().useGravity = true;
                GameManager.instance.activePlayer.GetComponent<Collider>().enabled = false;
                StartCoroutine(reactMiss1());
            }
            else
            {
                yield return new WaitForSeconds(1.2f);
                brick1.GetComponent<Renderer>().enabled = true;
                brick1.GetComponent<Rigidbody>().useGravity = true;
                GameManager.instance.activePlayer.GetComponent<Collider>().enabled = false;
                StartCoroutine(reactBrick1());
            }
        }
        
    }

    public IEnumerator reactBrick1()
    {
        yield return new WaitForSeconds(.6f);
        GameManager.instance.activePlayer.transform.localScale -= new Vector3(0.0f, .9f, 0.0f);
        if (GameManager.instance.battleAnimation[3] == -1)
        {
            StartCoroutine(pyramidScheme());
        }
        else if (runNum == 1)
        {
            if (GameManager.instance.battleAnimation[3] == 0)
            {
                yield return new WaitForSeconds(1.0f);
                brick1.transform.Translate(500.0f, 500.0f, 500.0f);
                yield return new WaitForSeconds(.8f);
                decompressing = true;
                yield return new WaitForSeconds(1.2f);
				AllDone ();
                // end of attack
                // GO TO END CODE
            }
            else if (GameManager.instance.battleAnimation[3] == 2)
            {
                StartCoroutine(dropBrick2());
            }
            else if (GameManager.instance.battleAnimation[3] == 1)
            {
                yield return new WaitForSeconds(1.5f);
                brick1.GetComponent<Renderer>().enabled = false;
                brick1.GetComponent<Rigidbody>().useGravity = false;
                brick1.transform.position = new Vector3(-5.99f, 5.25f, 2.27f);
                decompressing = true;
                yield return new WaitForSeconds(1.4f);
                atStart = true;
                BattleCommands.runDropBricks = false;
                BattleCommands.runScorchingSniper = true;
            }
        }
        else if (runNum == 2)
        {
			AllDone ();
            // GO TO END
        }
    }

    public IEnumerator reactMiss1()
    {
        yield return new WaitForSeconds(.6f);
        if (runNum == 1)
        {
            if (GameManager.instance.battleAnimation[3] == 0)
            {
				AllDone ();
            }
            else if (GameManager.instance.battleAnimation[3] == 2)
            {
                StartCoroutine(dropBrick2());
            }
            else if (GameManager.instance.battleAnimation[3] == 1)
            {
                yield return new WaitForSeconds(1.5f);
                brick1.GetComponent<Renderer>().enabled = false;
                brick1.GetComponent<Rigidbody>().useGravity = false;
                brick1.transform.position = new Vector3(-5.99f, 5.25f, 2.27f);
                BattleCommands.runDropBricks = false;
                atStart = true;
                yield return new WaitForSeconds(.9f);
                BattleCommands.runScorchingSniper = true;
            }
        }
        else if (runNum == 2)
        {
			AllDone ();
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
			AllDone ();
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
				AllDone ();
            }
            else if (GameManager.instance.battleAnimation[6] == 0)
            {
                decompressing = true;
                yield return new WaitForSeconds(1.2f);
                AllDone();
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
        yield return new WaitForSeconds(1.7f);
        AllDone();
    }

	private void AllDone() {
		GameManager.instance.LoadScene (GameManager.instance.level);
	}
}
