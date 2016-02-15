using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DrawDamage : MonoBehaviour {

    public Text damageText;
    private float timeElapsed = 0.0f;
    private float duration = 2.0f;

	// Use this for initialization
	void Start () {
        damageText = GetComponent<Text>();
        damageText.text = "42";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator CoDrawDamage()
    {
        damageText.enabled = true;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            damageText.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f));
        }
        damageText.enabled = false;
        yield return null;
    }
}
