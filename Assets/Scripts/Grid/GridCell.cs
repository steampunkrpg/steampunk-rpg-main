using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour {

    public int col, row;


    public void SetGridProperties(string name, int col, int row)
    {
        transform.name = name;
        this.col = col;
        this.row = row;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
