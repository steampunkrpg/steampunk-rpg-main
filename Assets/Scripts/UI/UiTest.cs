using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiTest : MonoBehaviour {

    private float health;
    private float mana;
    public Image healthBar;
    public Image manaBar;
//	public RawImage partyBackdrop;
	public int partySize;

	// Use this for initialization
	void Start () {
        healthBar.fillAmount = 1f;
        manaBar.fillAmount = 1f;
//		partyBackdrop.rectTransform.rect.height = 50f * partySize + (partySize + 1) * 5f;
		for (int i = 0; i < partySize; i++) {
//			;
//			partyMember.name = "Party-Member-" + (i + 1);
//			partyMember.transform.localPosition = new Vector2 (5f, -((float)partySize * 55f + 5f));
		}
	}
	
	// Update is called once per frame
	void Update () {
//        UseAbility();
		if (Input.GetKeyDown (KeyCode.Alpha4) && manaBar.fillAmount >= 0.1f) {
			UseAbility4 ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha5) && manaBar.fillAmount >= 0.2f) {
			UseAbility5 ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha6) && manaBar.fillAmount >= 0.3f) {
			UseAbility6 ();
		}
        TakeDamage();
        ResetHealthAndMana();
	}

//    public void UseAbility()
//    {
//        if (Input.GetKeyDown(KeyCode.Alpha4) && 
//            manaBar.fillAmount >= 0.1f)
//        {
//            manaBar.fillAmount -= 0.1f;
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha5) &&
//            manaBar.fillAmount >= 0.2f)
//        {
//            manaBar.fillAmount -= 0.2f;
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha6) &&
//            manaBar.fillAmount >= 0.3f)
//        {
//            manaBar.fillAmount -= 0.3f;
//        }
//    }

	public void UseAbility4()
	{
		manaBar.fillAmount -= 0.1f;
	}

	public void UseAbility5()
	{
		manaBar.fillAmount -= 0.2f;
	}

	public void UseAbility6()
	{
		manaBar.fillAmount -= 0.3f;
	}

	public void LoadParty()
	{

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
