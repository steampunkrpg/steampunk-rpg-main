using UnityEngine;
using System.Collections;

public class LightingBOSSFlash : MonoBehaviour {

    public float minTime;
    public float flashRate;
    public float minLightningIntensity;
    public float maxLightningIntensity;
    public float minLightningBreak;
    public float maxLightningBreak;

    public Light lightningFlash;
    public AudioSource thunder;

    private float lastTime = 0f;

    void Start()
    {
        // Initialization of variables
        minTime = 1.8f;
        flashRate = 0.95f;
        minLightningIntensity = 0.3f;
        maxLightningIntensity = 0.7f;
        minLightningBreak = 1.5f;
        maxLightningBreak = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        RandomLightningFlash();
    }

    // Random Lightning Strike
    void RandomLightningFlash()
    {
        // Lightning flashes after a given 'break' period
        if ((Time.time - lastTime) > minTime)
        {
            // Determines how often a flash occurs per frame
            if (flashRate > Random.value)
            {
                lightningFlash.enabled = true;
                // Determines how often thunder occurs
                if (Random.value < 0.1f)
                {
                    if (!thunder.isPlaying)
                        thunder.Play();
                }
                // Randomly assigns an intensity to each lightning flash
                lightningFlash.intensity = Random.Range(minLightningIntensity, maxLightningIntensity);
            }
            else
            {
                // No lightning flash
                lightningFlash.enabled = false;
                lastTime = Time.time;
                minTime = Random.Range(minLightningBreak, maxLightningBreak);

            }

        } 
    }
}
