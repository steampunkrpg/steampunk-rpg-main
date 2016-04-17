using UnityEngine;
using System.Collections;

public class EnemyCounterattack : MonoBehaviour {

    public ParticleSystem shadow;
    public ParticleSystem shadowBurst;
    public bool down;
    public bool forward;
    public bool upward;
    public bool expanding;
    public bool backwards;
    public GameObject hole;

    // Use this for initialization
    void Start() {
        shadow.Pause();
        shadowBurst.Pause();
        shadow.GetComponent<Renderer>().enabled = false;
        shadowBurst.GetComponent<Renderer>().enabled = false;
        forward = false;
        hole.GetComponent<Renderer>().enabled = false;
        down = false;
        hole.transform.Translate(new Vector3(1.4f, 0.0f, -1.1f));
    }

    // Update is called once per frame
    void Update() {
        if (forward)
        {
            GameManager.instance.activeEnemy.transform.Translate(0.02f, 0.0f, .1f);
            hole.transform.Translate(-0.1f, 0.0f, 0.02f);
            if (GameManager.instance.activeEnemy.transform.position.x < -6.5)
            {
                forward = false;
            }
        }
        if (backwards)
        {
            GameManager.instance.activeEnemy.transform.Translate(-0.02f, 0.0f, -.1f);
            hole.transform.Translate(0.1f, 0.0f, -0.02f);
            if (GameManager.instance.activeEnemy.transform.position.x > 0)
            {
                backwards = false;
            }
        }
        if (down)
        {
            GameManager.instance.activeEnemy.transform.Translate(0.0f, -0.03f, 0.0f);
            if (GameManager.instance.activeEnemy.transform.position.y < -2)
            {
                down = false;
            }
        }
        if (expanding)
        {
            hole.transform.localScale += new Vector3(.02f, 0.0f, .02f);
            if (hole.transform.localScale.x >= 3)
            {
                expanding = false;
            }
        }
        if (upward)
        {
            GameManager.instance.activeEnemy.transform.Translate(0.0f, 0.1f, 0.0f);
            if (GameManager.instance.activeEnemy.transform.position.y > 1)
            {
                GameManager.instance.activeEnemy.transform.Translate(new Vector3(-0.2f, 0.0f, -0.2f));
                GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Airstrike");
                upward = false;
            }
        }
    }

    public void invokeCo()
    {
        StartCoroutine(counter());
    }

    public IEnumerator counter()
    {
        yield return new WaitForSeconds(1.2f);
        WriteMovelist.currentMove = "Scumbag in the Shadows";
        GameManager.instance.activePlayer.GetComponent<Animator>().Play("Idle");
        hole.GetComponent<Renderer>().enabled = true;
        down = true;
        expanding = true;
        //forward = true;
       // GameManager.instance.activeEnemy.transform.Translate(new Vector3(0.0f, 0.0f, 7.0f));
        BattleCommands.runSolarShot = false;
        BattleCommands.damageWriter.setPlayerDamText(GameManager.instance.battleAnimation[5]);
        //shadow.GetComponent<Renderer>().enabled = true;
        //shadowBurst.GetComponent<Renderer>().enabled = true;
        shadow.Play();
        shadowBurst.Play();
        

        StartCoroutine(counter2());
    }

    public IEnumerator counter2()
    {
        yield return new WaitForSeconds(4.0f);
        forward = true;
        StartCoroutine(counter3());
    }

    public IEnumerator counter3()
    {
        yield return new WaitForSeconds(2.0f);
        upward = true;
        StartCoroutine(counter4());
    }

    public IEnumerator counter4()
    {
        yield return new WaitForSeconds(1.2f);
        if (GameManager.instance.battleAnimation[6] == -1)
        {
            GameManager.instance.activePlayer.GetComponent<Animator>().Play("Dead");
        } else
        {
            GameManager.instance.activePlayer.GetComponent<Animator>().Play("Get Hit");
        }
        StartCoroutine(counter5());
        StartCoroutine(BattleCommands.damageWriter.CoDrawDamagePl());
    }

    public IEnumerator counter5()
    {
        yield return new WaitForSeconds(1.2f);
        down = true;
        StartCoroutine(counter6());
    }

    public IEnumerator counter6()
    {
        yield return new WaitForSeconds(1.2f);
        backwards = true;
        StartCoroutine(counterEnd());
    }

    public IEnumerator counterEnd()
    {
        yield return new WaitForSeconds(1.2f);
        upward = true;
        StartCoroutine(startAttack3());
    }

    public IEnumerator startAttack3()
    {
        hole.GetComponent<Renderer>().enabled = false;
        SolarShotAni.runNum = 2;
        yield return new WaitForSeconds(1.2f);
        if (GameManager.instance.battleAnimation[6] == 1)
        {
            BattleCommands.runSolarShot = true;
        }
    }
}
