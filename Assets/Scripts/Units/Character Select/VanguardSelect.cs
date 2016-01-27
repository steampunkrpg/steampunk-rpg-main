using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VanguardSelect : MonoBehaviour {
	
	public float fadeSpeed;
	
	private bool isTextActive;
	private Text classText, classDescription;
	private Vector3 scaleOnMouseover, offset, originalScale, originalPosition;

	void Start() {
		classText = GameObject.Find ("VanguardSelectText").GetComponent<Text> ();
		classDescription = GameObject.Find ("VanguardDescriptionText").GetComponent<Text> ();

		classText.color = Color.clear;
		classDescription.color = Color.clear;

		offset = Vector3.zero;
		scaleOnMouseover = new Vector3(0.2f, 0.2f, 0.2f);

		originalScale = transform.localScale;
		originalPosition = transform.position;
	}

	void Update () {
		FadeText ();
	}

	void OnMouseEnter () {
		isTextActive = true;

		transform.localScale = transform.localScale + scaleOnMouseover;
		offset = new Vector3 (transform.position.x, transform.localScale.y / 2, transform.position.z);
		transform.position = offset;
	}

	void OnMouseExit() {
		isTextActive = false;

		transform.position = originalPosition;
		transform.localScale = originalScale;
	}

	void FadeText () {
		if (isTextActive) {
			classText.color = Color.Lerp (classText.color, Color.white, fadeSpeed * Time.deltaTime);
			classDescription.color = Color.Lerp (classDescription.color, Color.white, fadeSpeed * Time.deltaTime);

		} else {
			classText.color = Color.Lerp (classText.color, Color.clear, fadeSpeed * Time.deltaTime);
			classDescription.color = Color.Lerp (classDescription.color, Color.clear, fadeSpeed * Time.deltaTime);
		}
	}
}
