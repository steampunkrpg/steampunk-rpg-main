using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector3 offset, startingLocation;
	private Ray mouseRay;
	private GameObject title;

	void Start () {
		title = GameObject.Find("TitleText");

		startingLocation = transform.position;
		offset = new Vector3 (0.0f, 0.6f, -1.5f);
	}

	void Update (){
		if (Input.GetMouseButtonDown (0)) {
			mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(mouseRay, out hit)){
				if (hit.collider.tag.Equals("CharacterClass")){

					title.SetActive(false);
					transform.position = hit.collider.transform.position + offset;

				}
			}
		}
	}

	//public to be used by the BackButton
	public void MoveBackToStart() {
		transform.position = startingLocation;
		title.SetActive (true);
	}
}
