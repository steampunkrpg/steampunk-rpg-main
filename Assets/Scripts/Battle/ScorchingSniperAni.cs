using UnityEngine;
using System.Collections;

public class ScorchingSniperAni : MonoBehaviour {

    public Camera cam1;
    public Camera cam2;
    public bool atStart;
    public bool moveLeft;
    public bool moveRight;
    public bool moveUp;
    public bool moveDown;
    public GameObject crosshairs;
    public GameObject sniperBoxN;
    public GameObject sniperBoxNE;
    public GameObject sniperBoxE;
    public GameObject sniperBoxSE;
    public GameObject sniperBoxS;
    public GameObject sniperBoxSW;
    public GameObject sniperBoxW;
    public GameObject sniperBoxNW;
    public bool moveN;
    public bool moveNE;
    public bool moveE;
    public bool moveSE;
    public bool moveS;
    public bool moveSW;
    public bool moveW;
    public bool moveNW;
    int runCount = 0;

    // Use this for initialization
    void Start () {
        atStart = true;
        crosshairs.GetComponent<Renderer>().enabled = false;
        cam2.enabled = false;
        sniperBoxN.GetComponent<Renderer>().enabled = false;
        sniperBoxNE.GetComponent<Renderer>().enabled = false;
        sniperBoxE.GetComponent<Renderer>().enabled = false;
        sniperBoxSE.GetComponent<Renderer>().enabled = false;
        sniperBoxS.GetComponent<Renderer>().enabled = false;
        sniperBoxSW.GetComponent<Renderer>().enabled = false;
        sniperBoxW.GetComponent<Renderer>().enabled = false;
        sniperBoxNW.GetComponent<Renderer>().enabled = false;
        moveN = false;
        moveNE = false;
        moveE = false;
        moveSE = false;
        moveS = false;
        moveSW = false;
        moveW = false;
        moveNW = false;
}
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runScorchingSniper)
        {
            if (atStart)
            {
                crosshairs.GetComponent<Renderer>().enabled = true;
                GameManager.instance.activeEnemy.transform.Translate(new Vector3(0.0f, -.3f, 0.0f));
                WriteMovelist.currentMove = "Possibly American Sniper";
                atStart = false;
                cam1.enabled = false;
                cam2.enabled = true;
                StartCoroutine(moveCamera());
            }
            if (moveLeft)
            {
                cam2.transform.Translate(new Vector3(-.02f, 0.0f, 0.0f));
            }
            if (moveRight)
            {
                cam2.transform.Translate(new Vector3(.02f, 0.0f, 0.0f));
            }
            if (moveUp)
            {
                cam2.transform.Translate(new Vector3(0.0f, 0.02f, 0.0f));
            }
            if (moveDown)
            {
                cam2.transform.Translate(new Vector3(0.0f, -0.02f, 0.0f));
            }
            if (moveS)
            {
                if (sniperBoxS.transform.position.x >= -2.69)
                {
                    moveS = false;
                }
                sniperBoxS.transform.Translate(new Vector3(0.0f, -0.03f, 0.0f));
            }
            if (moveN)
            {
                if (sniperBoxN.transform.position.y <= 3.36)
                {
                    moveN = false;
                }
                sniperBoxN.transform.Translate(new Vector3(0.0f, -0.02f, 0.0f));
            }
            if (moveW)
            {
                if (sniperBoxW.transform.position.z <= 6.39)
                {
                    moveW = false;
                }
                sniperBoxW.transform.Translate(new Vector3(0.0f, -0.02f, 0.0f));
            }
            if (moveE)
            {
                if (sniperBoxE.transform.position.z >= -3.54)
                {
                    moveE = false;
                }
                sniperBoxE.transform.Translate(new Vector3(0.0f, 0.02f, 0.0f));
            }
            if (moveNE)
            {
                if (sniperBoxNE.transform.position.z >= .5 && sniperBoxNE.transform.position.y <= 2.53)
                {
                    moveNE = false;
                }
                if (sniperBoxNE.transform.position.z <= .5)
                {
                    sniperBoxNE.transform.Translate(new Vector3(0.0f, 0.0f, 0.02f));
                }
                if (sniperBoxNE.transform.position.y >= 2.53)
                {
                    sniperBoxNE.transform.Translate(new Vector3(0.0f, 0.02f, 0.0f));
                }
            }
            if (moveSE)
            {
                if (sniperBoxSE.transform.position.z >= .37 && sniperBoxSE.transform.position.y >= 1.24)
                {
                    moveSE = false;
                }
                if (sniperBoxSE.transform.position.z <= .37)
                {
                    sniperBoxSE.transform.Translate(new Vector3(0.0f, 0.0f, 0.02f));
                }
                if (sniperBoxSE.transform.position.y <= 1.24)
                {
                    sniperBoxSE.transform.Translate(new Vector3(0.0f, 0.0f, 0.02f));
                }
            }
            if (moveSW)
            {
                if (sniperBoxSW.transform.position.z <= 2.41 && sniperBoxSW.transform.position.y >= 1.24)
                {
                    moveSW = false;
                }
                if (sniperBoxSW.transform.position.z >= 2.41)
                {
                    sniperBoxSW.transform.Translate(new Vector3(0.0f, 0.0f, -0.02f));
                }
                if (sniperBoxSW.transform.position.y <= 1.24)
                {
                    sniperBoxSW.transform.Translate(new Vector3(0.0f, 0.0f, -0.02f));
                }
            }
            if (moveNW)
            {
                if (sniperBoxNW.transform.position.z <= 2.32 && sniperBoxNW.transform.position.y <= 2.53)
                {
                    moveNW = false;
                }
                if (sniperBoxNW.transform.position.z >= 2.32)
                {
                    sniperBoxNW.transform.Translate(new Vector3(0.0f, 0.0f, -0.02f));
                }
                if (sniperBoxNE.transform.position.y >= 2.53)
                {
                    sniperBoxNE.transform.Translate(new Vector3(0.0f, -0.02f, 0.0f));
                }
            }
        }
	}

    public IEnumerator moveCamera()
    {
        moveLeft = true;
        yield return new WaitForSeconds(1.1f);
        moveLeft = false;
        moveRight = true;
        moveDown = true;
        yield return new WaitForSeconds(.6f);
        moveRight = false;
        moveDown = false;
        moveLeft = true;
        yield return new WaitForSeconds(.8f);
        moveLeft = false;
        moveUp = true;
        moveRight = true;
        yield return new WaitForSeconds(.8f);
        moveUp = false;
        moveRight = false;
        moveLeft = true;
        yield return new WaitForSeconds(.3f);
        moveLeft = false;
        moveDown = true;
        yield return new WaitForSeconds(.6f);
        moveDown = false;
        moveRight = true;
        moveDown = true;
        yield return new WaitForSeconds(.37f);
        moveRight = false;
        moveDown = false;
        moveUp = true;
        yield return new WaitForSeconds(.4f);
        moveUp = false;
        moveRight = true;
        yield return new WaitForSeconds(.44f);
        moveRight = false;
        StartCoroutine(moveBoxes());
    }

    public IEnumerator moveBoxes()
    {
        yield return new WaitForSeconds(.1f);
        sniperBoxN.GetComponent<Renderer>().enabled = true;
        sniperBoxNE.GetComponent<Renderer>().enabled = true;
        sniperBoxE.GetComponent<Renderer>().enabled = true;
        sniperBoxSE.GetComponent<Renderer>().enabled = true;
        sniperBoxS.GetComponent<Renderer>().enabled = true;
        sniperBoxSW.GetComponent<Renderer>().enabled = true;
        sniperBoxW.GetComponent<Renderer>().enabled = true;
        sniperBoxNW.GetComponent<Renderer>().enabled = true;
        moveN = true;
        moveNE = true;
        moveE = true;
        moveSE = true;
        moveS = true;
        moveSW = true;
        moveW = true;
        moveNW = true;
        yield return new WaitForSeconds(4.5f);
        // counterattack, so check if it hits with appropriate array 
        if (runCount == 0)
        {
            runCount++;
            // MISS
            if (GameManager.instance.battleAnimation[4]!=1)
            {
                // attack twice
                if (GameManager.instance.battleAnimation[6]==1)
                {
                    if (GameManager.instance.battleAnimation[7]!=1)
                    {
                        // DOUBLE MISS, GO TO END
                    }
                    else
                    {
                        // second hit
                        if (GameManager.instance.battleAnimation[9]== -1)
                        {
                            GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Dead");
                            // GO TO END
                        }
                        else
                        {
                            GameManager.instance.activeEnemy.GetComponent<Animator>().Play("break_through_the_block");
                            // GO TO END
                        }
                    }
                }
                // enemy attacks again
                else if (GameManager.instance.battleAnimation[6]==2)
                {
                    sniperBoxN.GetComponent<Renderer>().enabled = false;
                    sniperBoxNE.GetComponent<Renderer>().enabled = false;
                    sniperBoxE.GetComponent<Renderer>().enabled = false;
                    sniperBoxSE.GetComponent<Renderer>().enabled = false;
                    sniperBoxS.GetComponent<Renderer>().enabled = false;
                    sniperBoxSW.GetComponent<Renderer>().enabled = false;
                    sniperBoxW.GetComponent<Renderer>().enabled = false;
                    sniperBoxNW.GetComponent<Renderer>().enabled = false;
                    crosshairs.GetComponent<Renderer>().enabled = false;
                    cam1.enabled = true;
                    cam2.enabled = false;
                    BattleCommands.runDropBricks = true;
                } 
            }
            // HIT
            else
            {
                // KILL
                if (GameManager.instance.battleAnimation[6]==-1)
                {
                    GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Dead");
                    // GO TO END
                }
                else
                {
                    GameManager.instance.activeEnemy.GetComponent<Animator>().Play("break_through_the_block");
                    yield return new WaitForSeconds(1.0f);
                    // attack twice
                    if (GameManager.instance.battleAnimation[6] == 1)
                    {
                        if (GameManager.instance.battleAnimation[7] != 1)
                        {
                            // MISS, GO TO END
                        }
                        else
                        {
                            // second hit
                            if (GameManager.instance.battleAnimation[9] == -1)
                            {
                                GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Dead");
                                // GO TO END
                            }
                            else
                            {
                                GameManager.instance.activeEnemy.GetComponent<Animator>().Play("break_through_the_block", -1, 0f);
                                // GO TO END
                            }
                        }
                    }
                    // enemy attacks again
                    else if (GameManager.instance.battleAnimation[6] == 2)
                    {
                        sniperBoxN.GetComponent<Renderer>().enabled = false;
                        sniperBoxNE.GetComponent<Renderer>().enabled = false;
                        sniperBoxE.GetComponent<Renderer>().enabled = false;
                        sniperBoxSE.GetComponent<Renderer>().enabled = false;
                        sniperBoxS.GetComponent<Renderer>().enabled = false;
                        sniperBoxSW.GetComponent<Renderer>().enabled = false;
                        sniperBoxW.GetComponent<Renderer>().enabled = false;
                        sniperBoxNW.GetComponent<Renderer>().enabled = false;
                        crosshairs.GetComponent<Renderer>().enabled = false;
                        cam2.enabled = false;
                        cam1.enabled = true;
                        BattleCommands.runDropBricks = true;
                    }
                }
            }
        }
    }
}
