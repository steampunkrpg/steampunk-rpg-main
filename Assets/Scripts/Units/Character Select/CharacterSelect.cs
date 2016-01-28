using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class CharacterSelect : MonoBehaviour
{

    public float fadeSpeed;

    private bool isTextActive;
    private Text classText, classDescription;
    private Vector3 scaleOnMouseover, offset, originalScale, originalPosition;

    void Start()
    {
        // set class text and description in child classes

        classText.color = Color.clear;
        classDescription.color = Color.clear;

        offset = Vector3.zero;
        scaleOnMouseover = new Vector3(0.2f, 0.2f, 0.2f);

        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    public virtual void Update()
    {
        FadeText();
    }

    public virtual void OnMouseEnter()
    {
        isTextActive = true;

        transform.localScale = transform.localScale + scaleOnMouseover;
        offset = new Vector3(transform.position.x, transform.localScale.y, transform.position.z);
        transform.position = offset;
    }

    public virtual void OnMouseExit()
    {
        isTextActive = false;

        transform.position = originalPosition;
        transform.localScale = originalScale;
    }

    public virtual void FadeText()
    {
        if (isTextActive)
        {
            classText.color = Color.Lerp(classText.color, Color.white, fadeSpeed * Time.deltaTime);
            classDescription.color = Color.Lerp(classDescription.color, Color.white, fadeSpeed * Time.deltaTime);

        }
        else {
            classText.color = Color.Lerp(classText.color, Color.clear, fadeSpeed * Time.deltaTime);
            classDescription.color = Color.Lerp(classDescription.color, Color.clear, fadeSpeed * Time.deltaTime);
        }
    }
}
