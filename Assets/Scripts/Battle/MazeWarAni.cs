using UnityEngine;
using System.Collections;

public class MazeWarAni : MonoBehaviour {

    public GameObject topWall;
    public GameObject rightWall;
    public GameObject leftWall;
    public GameObject backWall;
    public GameObject floor;
    public GameObject line1;
    public GameObject line2;
    public GameObject line3;
    public GameObject line4;
    public Camera mainCamera;
    public Camera mazeCamera;
    public GameObject eye;
    public bool atStart;

	// Use this for initialization
	void Start () {
        topWall.GetComponent<Renderer>().enabled = false;
        rightWall.GetComponent<Renderer>().enabled = false;
        leftWall.GetComponent<Renderer>().enabled = false;
        backWall.GetComponent<Renderer>().enabled = false;
        floor.GetComponent<Renderer>().enabled = false;
        line1.GetComponent<Renderer>().enabled = false;
        line2.GetComponent<Renderer>().enabled = false;
        line3.GetComponent<Renderer>().enabled = false;
        line4.GetComponent<Renderer>().enabled = false;
        mazeCamera.enabled = false;
        eye.GetComponent<Renderer>().enabled = false;
        atStart = true;
    }
	
	// Update is called once per frame
	void Update () {
	    if (BattleCommands.runMazeWar)
        {
            if (atStart)
            {
                WriteMovelist.currentMove = "Maze Wars";
                atStart = false;
                StartCoroutine(setUpWalls());
            }
        }
	}

    public IEnumerator setUpWalls()
    {
        leftWall.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(.6f);
        rightWall.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(.4f);
        floor.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(.2f);
        backWall.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(.4f);
        topWall.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(.4f);
        eye.GetComponent<Renderer>().enabled = true;
        line1.GetComponent<Renderer>().enabled = true;
        line2.GetComponent<Renderer>().enabled = true;
        line3.GetComponent<Renderer>().enabled = true;
        line4.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(eyeAttack());
    }

    public IEnumerator eyeAttack()
    {
        mazeCamera.enabled = true;
        mainCamera.enabled = false;
        yield return new WaitForSeconds(1.0f);
        eye.transform.Translate(10.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        eye.transform.Translate(10.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        eye.transform.Translate(10.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        eye.transform.Translate(10.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        eye.transform.Translate(10.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(.6f);
        mainCamera.enabled = true;
        mazeCamera.enabled = false;
        topWall.GetComponent<Renderer>().enabled = false;
        rightWall.GetComponent<Renderer>().enabled = false;
        leftWall.GetComponent<Renderer>().enabled = false;
        backWall.GetComponent<Renderer>().enabled = false;
        floor.GetComponent<Renderer>().enabled = false;
        line1.GetComponent<Renderer>().enabled = false;
        line2.GetComponent<Renderer>().enabled = false;
        line3.GetComponent<Renderer>().enabled = false;
        line4.GetComponent<Renderer>().enabled = false;
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Get Hit");
        yield return new WaitForSeconds(.8f);
        GameManager.instance.LoadScene(GameManager.instance.level);
    }
}
