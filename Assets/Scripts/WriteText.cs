using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WriteText : MonoBehaviour {

    private string text;
    public Text writeText;
    public GameObject sound1;
    public GameObject sound2;
    public GameObject sound3;
    public bool writing1;
    public bool writing2;
    public bool writing3;

	// Use this for initialization
	void Start () {
        writeText.text = "";
        writing1 = true;
        StartCoroutine(AnimateText());
        writing2 = false;
        writing2 = false;
        sound2.GetComponent<AudioSource>().Pause();
        sound3.GetComponent<AudioSource>().Pause();
	}
	
    public IEnumerator AnimateText()
    {
        sound1.GetComponent<AudioSource>().Play();
        int i = 0;
        text = "Imma come clean, I have no idea how we got to this point. The regime of the warlord can be summed up like this:";
        string str = "";
        while (i < text.Length && writing1)
        {
            str += text[i++];
            writeText.text = str;
            yield return new WaitForSeconds(.08f);
        }
    }

    public IEnumerator textPt2()
    {
        sound2.GetComponent<AudioSource>().Play();
        int i = 0;
        text = "Basically everyone was like \"Ey Yo fam if we were to quantify our happiness at this instant, it would probably be low\"";
        string str = "";
        while (i < text.Length && writing2)
        {
            str += text[i++];
            writeText.text = str;
            yield return new WaitForSeconds(.08f);
        }
    }

    public IEnumerator textPt3()
    {
        sound3.GetComponent<AudioSource>().Play();
        int i = 0;
        text = "I mean, it's kinda hard to put into words, but seriously, this dude was trash-can city as a person.";
        string str = "";
        while (i < text.Length && writing3)
        {
            str += text[i++];
            writeText.text = str;
            yield return new WaitForSeconds(.08f);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && writing1)
        {
            sound1.GetComponent<AudioSource>().Stop();
            writing1 = false;
            writing2 = true;
            writeText.text = "";
            StartCoroutine(textPt2());
        }
        else if (Input.GetKeyDown(KeyCode.Return) && writing2)
        {
            sound2.GetComponent<AudioSource>().Stop();
            writing2 = false;
            writing3 = true;
            writeText.text = "";
            StartCoroutine(textPt3());
        }
	}
}
