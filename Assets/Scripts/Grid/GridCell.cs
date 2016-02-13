using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour {

    public int col, row;

    Vector2[][] directions;


    //


    void Start()
    {
        directions = new Vector2[][] {
            // directions           0,               1,                  2,                    3,                4,                    5
            //                      NE,              N,                  NW,                   SW,               S,                    SE
            new Vector2[] { new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(0, 1) },
            new Vector2[] { new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 1) }
       };
    }

    void Update()
    {

    }
    
    public GridCell(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetGridProperties(string name, int col, int row)
    {
        transform.name = name;
        this.col = col;
        this.row = row;
    }

    public GridCell GetNeighbor(int direction)
    {
        var parity = row & 1;
        Debug.Log("Parity: " + parity);

        Debug.Log(directions.ToString());
        //Debug.Log("Dir: " + directions[parity][0]);
        Vector2 dir = directions[parity][direction];
        GridCell gridCell = GameObject.Find("Hex " + (int)(row + dir.x) + ", " + (int)(row + dir.y)).GetComponent<GridCell>() as GridCell;
        return gridCell;
    }
}
