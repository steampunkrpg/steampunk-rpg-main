using UnityEngine;
using System.Collections;

public class CameraIntroScene : MonoBehaviour {

    public Camera mainCamera;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public bool atStart;
    public bool onCam2;
    public bool onMainCam2;
    public bool onCam3;
    public bool onCam4;

	// Use this for initialization
	void Start () {
        mainCamera.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        atStart = true;
        onCam2 = false;
        onCam3 = false;
        onCam4 = false;
	}

    // Update is called once per frame
    void Update() {
        if (atStart)
        {
            mainCamera.transform.Translate(0.0f, 0.0f, 0.01f);
            if (mainCamera.transform.position.z >= -1)
            {
                atStart = false;
                onCam2 = true;
                mainCamera.enabled = false;
                camera2.enabled = true;
            }
        }
        else if (onCam2)
        {
            camera2.transform.Translate(-0.01f, 0.0f, 0.0f);
            if (camera2.transform.position.x <= -1.4)
            {
                onCam2 = false;
                camera2.enabled = false;
                mainCamera.enabled = true;
                onMainCam2 = true;
            }
        }
        else if (onMainCam2)
        {
            mainCamera.transform.Translate(0.0f, 0.0f, -.01f);
            if (mainCamera.transform.position.z <= -5)
            {
                onMainCam2 = false;
                onCam3 = true;
                mainCamera.enabled = false;
                camera3.enabled = true;
            }
        }
        else if (onCam3)
        {
            camera3.transform.Translate(0.0f, -.005f, 0.0f);
            if (camera3.transform.position.y <= .3)
            {
                onCam4 = true;
                onCam3 = false;
                camera3.enabled = false;
                camera4.enabled = true;
            }
        }
        else if (onCam4)
        {
            camera4.transform.Translate(0.0f, 0.0f, -.001f);   
        }
	}
}
