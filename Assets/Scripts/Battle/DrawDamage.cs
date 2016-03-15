using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DrawDamage : MonoBehaviour {

    public Text damageText;
    private float timeElapsed = 0.0f;
    private float duration = 2.0f;
    public Vector3 startPos;

    public DrawDamage(Text damage)
    {
        damageText = damage;
        damageText.enabled = false;
    }

	// Use this for initialization
	void Start () {
        damageText.text = "42";
        startPos = damageText.transform.position;
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
            damageText.transform.Translate(new Vector3(0.0f, 2.0f, 0.0f));
            yield return new WaitForSeconds(.025f);
        }
        damageText.enabled = false;
        damageText.transform.position = startPos;
        yield return null;
    }
}
