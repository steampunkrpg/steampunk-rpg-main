using UnityEngine;
using System.Collections;

public class ShakeScreen : MonoBehaviour {

    public GameObject mainCam;
    public static float timeElapsed = 0.0f;
    float duration;
    float yMove;
    Vector3 initPos;
    float damping = .01f;
    bool goingUp;
    public static bool shaking;

	// Use this for initialization
	void Start () {
        timeElapsed = 0.0f;
        duration = 4.0f;
        yMove = 1;
        initPos = mainCam.transform.position;
        goingUp = true;
        shaking = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (shaking)
        {
            timeElapsed += Time.deltaTime;
            if (goingUp)
            {
                mainCam.transform.Translate(0.0f, yMove, 0.0f);
                goingUp = false;
            }
            else if (yMove > 0)
            {
                mainCam.transform.Translate(0.0f, -yMove, 0.0f);
                goingUp = true;
                yMove -= damping;
            }
        }
    }
}
