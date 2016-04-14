using UnityEngine;
using System.Collections;

public class SolarShotAni : MonoBehaviour {

    public GameObject sphere;
    public GameObject enemy;
    public ParticleSystem ray1;
    public ParticleSystem ray2;
    public ParticleSystem ray3;
    public ParticleSystem ray4;
    public ParticleSystem ray5;
    public ParticleSystem ray6;
    public bool emitted = false;
    public Animator animPlayer;
    public Animator animEnemy;
    public ParticleSystem burst;
    public ParticleSystem shadow;
    public EnemyCounterattack startCounter;

    // Use this for initialization
    void Start() {
        ray1.Pause();
        ray2.Pause();
        ray3.Pause();
        ray4.Pause();
        ray5.Pause();
        ray6.Pause();
        ParticleSystem.EmissionModule em = burst.emission;
        em.enabled = false;
        sphere.GetComponent<Renderer>().enabled = false;
        ray1.GetComponent<Renderer>().enabled = false;
        ray2.GetComponent<Renderer>().enabled = false;
        ray3.GetComponent<Renderer>().enabled = false;
        ray4.GetComponent<Renderer>().enabled = false;
        ray5.GetComponent<Renderer>().enabled = false;
        ray6.GetComponent<Renderer>().enabled = false;
        burst.GetComponent<Renderer>().enabled = false;
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("Idle_block");
        startCounter = new EnemyCounterattack();
    }

    // Update is called once per frame
    void Update() {
        if (BattleCommands.runSolarShot)
        {
            WriteMovelist.currentMove = "Solar Shot";
            if (sphere.GetComponent<Renderer>().enabled == false)
            {
                sphere.GetComponent<Renderer>().enabled = true;
                ray1.GetComponent<Renderer>().enabled = true;
                ray2.GetComponent<Renderer>().enabled = true;
                ray3.GetComponent<Renderer>().enabled = true;
                ray4.GetComponent<Renderer>().enabled = true;
                ray5.GetComponent<Renderer>().enabled = true;
                ray6.GetComponent<Renderer>().enabled = true;
                burst.GetComponent<Renderer>().enabled = true;
            }
            
            if (sphere.transform.localScale.x < 2.6f)
            {
                sphere.transform.localScale += Vector3.one * Time.deltaTime;
                //sphere.transform.Rotate(new Vector3(0.0f, 1.24f, 0.0f));
                StartCoroutine(throwSun());
            }
            else
            {
                if (sphere.transform.position.z > -2.0f)
                {
                    sphere.transform.Translate(.1f, 0.0f, -.1f);

                }
                else if (!emitted)
                {

                    ray1.Play();
                    ray2.Play();
                    ray3.Play();
                    ray4.Play();
                    ray5.Play();
                    ray6.Play();
                    emitted = true;
                    StartCoroutine(waiter());
                    GameManager.instance.activeEnemy.GetComponent<Animator>().Play("break_through_the_block");
                    StartCoroutine("enemyReact");
                    this.SunBurst();
                }
            }
        }
    }
    public IEnumerator throwSun()
    {
        yield return new WaitForSeconds(1.75f);

        GameManager.instance.activePlayer.GetComponent<Animator>().Play("Attack(4)");
    }

    public IEnumerator enemyReact()
    {
        yield return new WaitForSeconds(.6f);
        enemy.transform.Translate(new Vector3(-1.2f, 1.0f, 0.0f));
        GameManager.instance.activeEnemy.GetComponent<Animator>().Play("break_through_the_block");
        // Pseudocode for new actions
        // if (health <= 0) {
        //  play("death animation")
        // else

        StartCoroutine(shadow.GetComponent<EnemyCounterattack>().counter2());
        //startCounter.invokeCo();
    }

    public IEnumerator waiter()
    {
        yield return new WaitForSeconds(1.5f);
        ray1.Stop();
        ray2.Stop();
        ray3.Stop();
        ray4.Stop();
        ray5.Stop();
        ray6.Stop();
        //sphere.SetActive(false);
        sphere.transform.Translate(new Vector3(500.0f, 500.0f, 500.0f));
        //burst.enableEmission = true;
        //ShakeScreen.shaking = true;
        //ParticleBurst.isBursting = true;
        
        animEnemy.Stop();
        //this.SunBurst();
    }

    public void SunBurst()
    {
        Debug.Log("In sunburst");
        ParticleSystem.EmissionModule em = burst.emission;
        em.enabled = true;

        burst.Play();
    }
}
