using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WriteText : MonoBehaviour {

    private string text;
    public Text writeText;
    public GameObject sound1;
    public GameObject sound2;
    public GameObject sound3;
    public GameObject sound4;
    public GameObject sound5;
    public bool writing1;
    public bool writing2;
    public bool writing3;
    public bool writing4;
    public bool writing5;

    // Use this for initialization
    void Start () {
        writeText.text = "";
        writing1 = true;
        StartCoroutine(AnimateText());
        writing2 = false;
        writing3 = false;
        writing4 = false;
        writing5 = false;
        sound2.GetComponent<AudioSource>().Pause();
        sound3.GetComponent<AudioSource>().Pause();
        sound4.GetComponent<AudioSource>().Pause();
        sound5.GetComponent<AudioSource>().Pause();
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
            yield return new WaitForSeconds(.065f);
        }
    }

    public IEnumerator textPt2()
    {
        sound2.GetComponent<AudioSource>().Play();
        int i = 0;
        text = "Basically everyone was like Yo fam if we were to quantify our happiness at this instant in time, it would probably be low";
        string str = "";
        while (i < text.Length && writing2)
        {
            str += text[i++];
            writeText.text = str;
            yield return new WaitForSeconds(.065f);
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
            yield return new WaitForSeconds(.065f);
        }
    }

    public IEnumerator textPt4()
    {
        sound4.GetComponent<AudioSource>().Play();
        int i = 0;
        text = "I should prolly introduce myself. Roughly translated to English, my name is \"Ungrqhqkhkhkhkhqkhahhah\" *coughs* Oh oh lawdy, oh lawdy that name is, ugh, that name is AWFUL, let's do another take. Wait, wha-we aren't doing another take? We're still recording? Ugh Jeepers Creepers I suck.";
        string str = "";
        while (i < text.Length && writing4)
        {
            str += text[i++];
            writeText.text = str;
            yield return new WaitForSeconds(.087f);
            if (str.Equals("I should prolly introduce myself. Roughly translated to English, my name is \"Ungrqhqkhkhkhkhqkhahhah\" *coughs*")) {
                yield return new WaitForSeconds(1.2f);
                writeText.text = "";
                str = "";
            }
            if (str.Equals(" Oh oh lawdy, oh lawdy that name is, ugh, that name is AWFUL, let's do another take. Wait, wha-we aren't doing"))
            {
                yield return new WaitForSeconds(.65f);
                writeText.text = "";
                str = "";
            }
        }
    }

    public IEnumerator textPt5()
    {
        sound5.GetComponent<AudioSource>().Play();
        int i = 0;
        text = "In short, I was tasked by some dudes with going back in time to try to beat this guy before he becomes the Warlord, & his influence spreads like a oil spill. If I succeed, we can return to the present & all will be fine and dandy. But if I fail, it will not just be famine today...but Famine Tomorrow. (Eh? You like that? Usin' the title of the game and such?)";
        string str = "";
        while (i < text.Length && writing5)
        {
            str += text[i++];
            writeText.text = str;
            yield return new WaitForSeconds(.055f);
            if (str.Equals("In short, I was tasked by some dudes with going back in time to try to beat this guy before he becomes the Warlord, &"))
            {
                yield return new WaitForSeconds(.2f);
                writeText.text = "";
                str = "";
            }
            else if (str.Equals(" his influence spreads like a oil spill. If I succeed, we can return to the present & all will be fine and dandy. But if"))
            {
                yield return new WaitForSeconds(.2f);
                writeText.text = "";
                str = "";
            }
        }
    }

    IEnumerator LoadLevelWithBar(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            yield return null;
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
        else if (Input.GetKeyDown(KeyCode.Return) && writing3)
        {
            sound3.GetComponent<AudioSource>().Stop();
            writing3 = false;
            writing4 = true;
            writeText.text = "";
            StartCoroutine(textPt4());
        }
        else if (Input.GetKeyDown(KeyCode.Return) && writing4)
        {
            sound4.GetComponent<AudioSource>().Stop();
            writing4 = false;
            writing5 = true;
            writeText.text = "";
            StartCoroutine(textPt5());
        }
        else if (Input.GetKeyDown(KeyCode.Return) && writing5)
        {
            sound5.GetComponent<AudioSource>().Stop();
            StartCoroutine(LoadLevelWithBar("World_Map"));
        }
    }
}
