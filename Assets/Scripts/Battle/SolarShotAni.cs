using UnityEngine;
using System.Collections;

public class SolarShotAni : MonoBehaviour {

    public GameObject sphere;
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
    }

    // Update is called once per frame
    void Update() {
        if (sphere.transform.localScale.x < 2.6f)
        {
            sphere.transform.localScale += Vector3.one * Time.deltaTime;
            //sphere.transform.Rotate(new Vector3(0.0f, 1.24f, 0.0f));
            StartCoroutine(throwSun());
        }
        else
        {
            if (sphere.transform.position.z > 4.5f)
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
                animEnemy.Play("Get hit");
                this.SunBurst();
                /* ParticleSystem.EmissionModule em = burst.emission;
                 em.enabled = true;
                 burst.Play(); */
                //ParticleBurst.isBursting = true;
            }
        }
    }
    public IEnumerator throwSun()
    {
        yield return new WaitForSeconds(1.75f);
        animPlayer.Play("Attack(4)");
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
        sphere.SetActive(false);
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
