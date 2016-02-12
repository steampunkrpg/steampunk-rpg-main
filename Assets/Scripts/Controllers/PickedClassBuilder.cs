using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class PickedClassBuilder : MonoBehaviour {

	private string selectedClass;
	private float STRmin, STRmax, DEXmin, DEXmax, INTmin, INTmax, HPmin, HPmax, 
		DEFmin, DEFmax, STRval, DEXval, INTval, HPval, DEFval;
	
	private ClassSelectManager csm;
	private string pickedClassName;
	private GameObject pickedClass;

	private bool classFetched;
	
	void Start () {
		csm = ClassSelectManager.FindObjectOfType<ClassSelectManager> ();
		classFetched = false;
	}

	void Update () {
		if (csm.classIsPicked == true && classFetched == false) {
			selectedClass = GetSelectedClass ();
			classFetched = true;
			RandomizeStats();
			SetStats();

			//transfers object to next scene
			DontDestroyOnLoad (pickedClass);
		}
	}
	
	string GetSelectedClass () {
		pickedClassName = csm.className;
		return pickedClassName;
	}
	
	void RandomizeStats () {
		if (selectedClass == "Vanguard") {
			STRmin = 7.0f;
			STRmax = 10.0f;
			STRval = Random.Range(STRmin, STRmax);

			DEXmin = 2.0f;
			DEXmax = 7.0f;
			DEXval = Random.Range(DEXmin, DEXmax);

			INTmin = 2.0f;
			INTmax = 7.0f;
			INTval = Random.Range(INTmin, INTmax);

			HPmin = 50.0f;
			HPmax = 75.0f;
			HPval = Random.Range(HPmin, HPmax);

			DEFmin = 5.0f;
			DEFmax = 10.0f;
			DEFval = Random.Range(DEFmin, DEFmax);
		}

		if (selectedClass == "Ranger") {
			STRmin = 2.0f;
			STRmax = 7.0f;
			STRval = Random.Range(STRmin, STRmax);
			
			DEXmin = 7.0f;
			DEXmax = 10.0f;
			DEXval = Random.Range(DEXmin, DEXmax);
			
			INTmin = 2.0f;
			INTmax = 7.0f;
			INTval = Random.Range(INTmin, INTmax);

			HPmin = 30.0f;
			HPmax = 55.0f;
			HPval = Random.Range(HPmin, HPmax);
			
			DEFmin = 2.0f;
			DEFmax = 7.0f;
			DEFval = Random.Range(DEFmin, DEFmax);
		}

		if (selectedClass == "Alchemist") {
			STRmin = 2.0f;
			STRmax = 7.0f;
			STRval = Random.Range(STRmin, STRmax);
			
			DEXmin = 2.0f;
			DEXmax = 7.0f;
			DEXval = Random.Range(DEXmin, DEXmax);
			
			INTmin = 7.0f;
			INTmax = 10.0f;
			INTval = Random.Range(INTmin, INTmax);

			HPmin = 40.0f;
			HPmax = 60.0f;
			HPval = Random.Range(HPmin, HPmax);

			DEFmin = 1.0f;
			DEFmax = 5.0f;
			DEFval = Random.Range(DEFmin, DEFmax);
		}
	}

	void SetStats() {
		pickedClass = GameObject.Find (pickedClassName);
		pickedClass.GetComponent<Stats> ().SetStats (STRval, DEXval, INTval, HPval, DEFval);
	}
}

