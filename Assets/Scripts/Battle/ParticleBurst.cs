using UnityEngine;
using System.Collections;

public class ParticleBurst : MonoBehaviour {

    public ParticleSystem burst;
    public static bool isBursting;

    // Use this for initialization
    void Start () {
        ParticleSystem.EmissionModule em = burst.emission;
        em.enabled = false;
        isBursting = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (isBursting)
        {
            Debug.Log("In is bursting");
            ParticleSystem.EmissionModule em = burst.emission;
            em.enabled = true;
            burst.Play();
        }
	}
}
