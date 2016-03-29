using UnityEngine;
using System.Collections;

public class PauseMenu1 : MonoBehaviour {
    
	public void Continue_Button()
    {
        GameManager.instance.State = GameManager.instance.prevState;
        this.gameObject.SetActive(false);
    }

    public void Quit_Button()
    {
        this.gameObject.SetActive(false);
        GameManager.instance.LoadScene("New_Menu_Scene");
    }
}
