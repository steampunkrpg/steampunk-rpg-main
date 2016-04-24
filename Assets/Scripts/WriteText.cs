using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WriteText : MonoBehaviour {

    private string text;
    public Text writeText;

	// Use this for initialization
	void Start () {
        writeText.text = "";
        StartCoroutine(AnimateText());
	}
	
    public IEnumerator AnimateText()
    {
        int i = 0;
        text = "Imma come clean, I have no idea how we got to this point. The regime of the warlord can be summed up like this:";
        string str = "";
        while (i < text.Length)
        {
            str += text[i++];
            writeText.text = str;
            yield return new WaitForSeconds(.05f);
        }
    }
	// Update is called once per frame
	void Update () {
        
	}
}
