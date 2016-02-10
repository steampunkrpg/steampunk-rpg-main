using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class StatBuilder : MonoBehaviour {

	private string selectedClass;
	private List<Stat> statsAndValues;
	private int STRmin, STRmax, DEXmin, DEXmax, INTmin, INTmax, HPmin, HPmax, 
		DEFmin, DEFmax, STRval, DEXval, INTval, HPval, DEFval;
	private Dictionary<string, List<Stat>> characterStats;
	
	private ClassSelectManager csm;
	private string pickedClass;
	private bool classFetched;

	// Use this for initialization
	void Start () {
		statsAndValues = new List<Stat> ();
		characterStats = new Dictionary<string, List<Stat>> ();

		csm = ClassSelectManager.FindObjectOfType<ClassSelectManager> ();
		classFetched = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (csm.classIsPicked == true && classFetched == false) {
			selectedClass = GetSelectedClass ();
			classFetched = true;
			RandomizeStats();
		}
	}
	
	string GetSelectedClass () {
		pickedClass = csm.className;
		return pickedClass;
	}

	//hit this with a software engineering hammer when you get a chance, duplicate code is gross
	void RandomizeStats () {
		if (selectedClass == "Vanguard") {
			STRmin = 7;
			STRmax = 10;
			STRval = Random.Range(STRmin, STRmax);
			statsAndValues[0].statName = "STR";
			statsAndValues[0].value = STRval;

			DEXmin = 2;
			DEXmax = 7;
			DEXval = Random.Range(DEXmin, DEXmax);
			statsAndValues[1].statName = "DEX";
			statsAndValues[1].value = DEXval;

			INTmin = 2;
			INTmax = 7;
			INTval = Random.Range(INTmin, INTmax);
			statsAndValues[2].statName = "INT";
			statsAndValues[2].value = INTval;

			HPmin = 50;
			HPmax = 75;
			HPval = Random.Range(HPmin, HPmax);
			statsAndValues[3].statName = "HP";
			statsAndValues[3].value = HPval;

			DEFmin = 5;
			DEFmax = 10;
			DEFval = Random.Range(DEFmin, DEFmax);
			statsAndValues[4].statName = "DEF";
			statsAndValues[4].value = DEFval;
		}

		if (selectedClass == "Ranger") {
			STRmin = 2;
			STRmax = 7;
			STRval = Random.Range(STRmin, STRmax);
			statsAndValues[0].statName = "STR";
			statsAndValues[0].value = STRval;
			
			DEXmin = 7;
			DEXmax = 10;
			DEXval = Random.Range(DEXmin, DEXmax);
			statsAndValues[1].statName = "DEX";
			statsAndValues[1].value = DEXval;
			
			INTmin = 2;
			INTmax = 7;
			INTval = Random.Range(INTmin, INTmax);
			statsAndValues[2].statName = "INT";
			statsAndValues[2].value = INTval;
			
			HPmin = 30;
			HPmax = 55;
			HPval = Random.Range(HPmin, HPmax);
			statsAndValues[3].statName = "HP";
			statsAndValues[3].value = HPval;
			
			DEFmin = 2;
			DEFmax = 7;
			DEFval = Random.Range(DEFmin, DEFmax);
			statsAndValues[4].statName = "DEF";
			statsAndValues[4].value = DEFval;
		}

		if (selectedClass == "Alchemist") {
			STRmin = 2;
			STRmax = 7;
			STRval = Random.Range(STRmin, STRmax);
			statsAndValues[0].statName = "STR";
			statsAndValues[0].value = STRval;
			
			DEXmin = 2;
			DEXmax = 7;
			DEXval = Random.Range(DEXmin, DEXmax);
			statsAndValues[1].statName = "DEX";
			statsAndValues[1].value = DEXval;
			
			INTmin = 7;
			INTmax = 10;
			INTval = Random.Range(INTmin, INTmax);
			statsAndValues[2].statName = "INT";
			statsAndValues[2].value = INTval;
			
			HPmin = 40;
			HPmax = 60;
			HPval = Random.Range(HPmin, HPmax);
			statsAndValues[3].statName = "HP";
			statsAndValues[3].value = HPval;
			
			DEFmin = 1;
			DEFmax = 5;
			DEFval = Random.Range(DEFmin, DEFmax);
			statsAndValues[4].statName = "DEF";
			statsAndValues[4].value = DEFval;
		}

		characterStats.Add(selectedClass, statsAndValues);
		Debug.Log (characterStats);
	}
}
