using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlchemistSelect : CharacterSelect {

	private bool isTextActive;
	private Text classText, classDescription;
	private Vector3 scaleOnMouseover, offset, originalScale, originalPosition;
	
	void Start() {
		classText = GameObject.Find ("AlchemistSelectText").GetComponent<Text> ();
		classDescription = GameObject.Find ("AlchemistDescriptionText").GetComponent<Text> ();

		classText.color = Color.clear;
		classDescription.color = Color.clear;

		offset = Vector3.zero;
		scaleOnMouseover = new Vector3(0.2f, 0.2f, 0.2f);
		
		originalScale = transform.localScale;
		originalPosition = transform.position;
	}
	
}
