using UnityEngine;
using System.Collections;

public class EnemyCounterattack : MonoBehaviour {

    public ParticleSystem shadow;
    public ParticleSystem shadowBurst;
    public bool forward;
    public bool expanding;
    public GameObject hole;

    // Use this for initialization
    void Start() {
        shadow.Pause();
        shadowBurst.Pause();
        shadow.GetComponent<Renderer>().enabled = false;
        shadowBurst.GetComponent<Renderer>().enabled = false;
        forward = false;
    }

    // Update is called once per frame
    void Update() {
        if (forward)
        {
            GameManager.instance.activeEnemy.transform.Translate(0.0f, 0.0f, .05f);
            if (GameManager.instance.activeEnemy.transform.position.x < -7)
            {
                forward = false;
            }
        }
    }

    public IEnumerator counter()
    {
        Debug.Log("in counter");
        StopCoroutine("enemyReact");
        //yield return new WaitForSeconds(1.0f);
        
        StartCoroutine(counter2());
        yield return new WaitForSeconds(0.0f);
    }

    public void invokeCo()
    {
        StartCoroutine(counter2());
    }

    public IEnumerator counter2()
    {
        yield return new WaitForSeconds(1.0f);
        forward = true;
       // GameManager.instance.activeEnemy.transform.Translate(new Vector3(0.0f, 0.0f, 7.0f));
        BattleCommands.runSolarShot = false;
        //shadow.GetComponent<Renderer>().enabled = true;
        shadowBurst.GetComponent<Renderer>().enabled = true;
        shadow.Play();
        shadowBurst.Play();
        WriteMovelist.currentMove = "Scumbag in the Shadows";
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("preparing_to_attack");
        
        GameManager.instance.activeEnemy.transform.Translate(new Vector3(0.0f, -5.0f, 0.0f));
        //  yield return null;
        

    }
}
