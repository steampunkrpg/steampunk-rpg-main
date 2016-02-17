using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiTest : MonoBehaviour {

    private float health;
    private float mana;
    public Image healthBar;
    public Image manaBar;
	private int partySize;

	// Use this for initialization
	void Start () {
        healthBar.fillAmount = 1f;
        manaBar.fillAmount = 1f;
<<<<<<< HEAD
		partySize = GameManager.instance.playerL.Count - 1;
=======
		partySize = GameManager.instance.playerL.Count;
>>>>>>> refs/remotes/origin/master
		for (int i = 0; i < partySize; i++) {
            RawImage player = Instantiate(Resources.Load("Hud-Party-Member", typeof(RawImage))) as RawImage;
            player.transform.SetParent(GameObject.Find("Party-Info").transform);
            player.transform.localScale = new Vector3(1f, 1f, 1f);
            player.transform.localPosition = new Vector3(5f, -5f - ((float)i * 55f), 0f);
            player.name = "Party-Member-" + (i + 1);
           
        }
    }
	
	// Update is called once per frame
	void Update () {
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

	//public void LoadParty()
	//{

	//}

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
