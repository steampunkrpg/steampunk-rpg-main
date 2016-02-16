using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiTest : MonoBehaviour {

    private float health;
    private float mana;
    public Image healthBar;
    public Image manaBar;

	// Use this for initialization
	void Start () {
        healthBar.fillAmount = 1f;
        manaBar.fillAmount = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        UseAbility();
        TakeDamage();
        ResetHealthAndMana();
	}

    void UseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && 
            manaBar.fillAmount >= 0.1f)
        {
            manaBar.fillAmount -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) &&
            manaBar.fillAmount >= 0.2f)
        {
            manaBar.fillAmount -= 0.2f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) &&
            manaBar.fillAmount >= 0.3f)
        {
            manaBar.fillAmount -= 0.3f;
        }
    }

    void TakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            healthBar.fillAmount -= 0.1f;
        }
    }

    void ResetHealthAndMana()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            healthBar.fillAmount = 1f;
            manaBar.fillAmount = 1f;
        }
    }

}
