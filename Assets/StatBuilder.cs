using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class StatsBuilder : MonoBehaviour {

	private string selectedClass;
	private Stat[] statsAndValues;
	private int STRmin, STRmax, DEXmin, DEXmax, INTmin, INTmax, HPmin, HPmax, DEFmin, DEFmax;
	private Dictionary<string, Stat[]> characterStats = new Dictionary<string, Stat[]> ();
	
	private ClassSelectManager csm;
	private string pickedClass;
	private bool classFetched;

	// Use this for initialization
	void Start () {
		csm = ClassSelectManager.FindObjectOfType<ClassSelectManager> ();
		classFetched = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (csm.classIsPicked == true && classFetched == false) {
			selectedClass = GetSelectedClass ();
			classFetched = true;
		}
	}
	
	string GetSelectedClass () {
		pickedClass = csm.className;
		return pickedClass;
	}

	void RandomizeStats () {
		if (selectedClass == "Vanguard") {
			STRmin = 5;
			STRmax = 10;

			DEXmin = 2;
			DEXmax = 8;

			INTmin = 2;
			INTmax = 8;

			HPmin = 50;
			HPmax = 75;

			DEFmin = 5;
			DEFmax = 10;
		}
		if (selectedClass == "Ranger") {
		}
		if (selectedClass == "Alchemist") {
		}
	}
}
