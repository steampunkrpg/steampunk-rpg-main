using UnityEngine;
using System.Collections;

public class ClassSelectManager : MonoBehaviour {
	
	public bool isFocusing, classIsPicked;
	public string className;

	private RaycastHit hit;
	private Vector3 offset, startingLocation;
	private Ray mouseRay;
	private GameObject title, backButton, confirmButton, areYouSurePanel, 
		buildingCharacterPanel, mainCamera, toggler;

	void Start () {
		isFocusing = false;
		classIsPicked = false;

		title = GameObject.Find("TitleText");
		toggler = GameObject.Find ("UIToggler");
		backButton = GameObject.Find ("BackButton");
		confirmButton = GameObject.Find ("ConfirmButton");
		areYouSurePanel = GameObject.Find ("AreYouSurePanel");
		buildingCharacterPanel = GameObject.Find ("BuildingCharacterPanel");
		mainCamera = GameObject.Find ("Main Camera");

		toggler.SetActive (false);
		areYouSurePanel.SetActive (false);
		buildingCharacterPanel.SetActive (false);

		startingLocation = mainCamera.transform.position;
		offset = new Vector3 (0.0f, 0.6f, -1.5f);
	}

	void Update (){
		CameraFocus ();
		UIToggler ();
	}

	void CameraFocus () {
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(mouseRay, out hit)){
				if (hit.collider.tag.Equals("CharacterClass")){
					className = hit.collider.name;

					title.SetActive(false);
					mainCamera.transform.position = hit.collider.transform.position + offset;
					isFocusing = true;
				}
			}
		}
	}

	void UIToggler () {
		if (isFocusing == false) {
			backButton.SetActive (false);
			confirmButton.SetActive (false);
			toggler.SetActive(false);
		} 
		else if (isFocusing == true) {
			backButton.SetActive (true);
			confirmButton.SetActive (true);
			toggler.SetActive(true);
		}
	}

	//public to be used by the ConfirmButton
	public void ConfirmCharacter () {
		areYouSurePanel.SetActive (true);
	}

	//public for yes
	public void YesButton () {
		areYouSurePanel.SetActive (false);
		buildingCharacterPanel.SetActive (true);
		classIsPicked = true;
	}

	//public for no
	public void NoButton () {
		areYouSurePanel.SetActive (false);
	}

	//public to be used by the BackButton
	public void MoveBackToStart() {
		mainCamera.transform.position = startingLocation;
		title.SetActive (true);
		isFocusing = false;
		areYouSurePanel.SetActive (false);

		toggler.SetActive (false);
	}
}
