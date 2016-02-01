using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public PauseMenu PM;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ScanForKeyStroke();
    }

    void ScanForKeyStroke()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PM.TogglePauseMenu();
        }
    }
}
