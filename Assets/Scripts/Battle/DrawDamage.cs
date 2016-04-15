using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DrawDamage : MonoBehaviour {

    public Text damageText;
    public Text playerDamText;
    private float timeElapsed = 0.0f;
    private float duration = 2.0f;
    public Vector3 startPos;
    public Vector3 startPos2;

    public DrawDamage(Text damage, Text damage2)
    {
        damageText = damage;
        playerDamText = damage2;
        damageText.enabled = false;
        playerDamText.enabled = false;
    }

    // Use this for initialization
    void Start() {
        damageText.text = "42";
        playerDamText.text = "42";
        startPos = damageText.transform.position;
        startPos2 = playerDamText.transform.position;
    }

    // Update is called once per frame
    void Update() {

    }

    public void setPlayerDamText(int number)
    {
        playerDamText.text = number.ToString();
    }

    public void setEnemyDamText(int number)
    {
        damageText.text = number.ToString();
    }

    public IEnumerator CoDrawDamageEn()
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
        timeElapsed = 0.0f;
        yield return null;
    }

    public IEnumerator CoDrawDamagePl()
    {
        playerDamText.enabled = true;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            playerDamText.transform.Translate(new Vector3(0.0f, 2.0f, 0.0f));
            yield return new WaitForSeconds(.025f);
        }
        playerDamText.enabled = false;
        playerDamText.transform.position = startPos2;
        timeElapsed = 0.0f;
        yield return null;
    }
}
