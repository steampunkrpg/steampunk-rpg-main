using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour {


    // Variable that "stores" hexagon
    public GameObject Hexagon;

    // Set grid width / height
    public int gridWidth, gridHeight;

    // hexagon tile width / height
    private float hexWidth, hexHeight;

    // set height and width for tiles
    private void SetSizes()
    {
        hexWidth = Hexagon.GetComponent<Renderer>().bounds.size.x;
        hexHeight = Hexagon.GetComponent<Renderer>().bounds.size.z;
    }

    // Center hex grid (put first hexagon at (0, 0, 0))
    Vector3 CalcStartPos()
    {
        //start in top left corner
        return new Vector3(-hexWidth * gridWidth / 2f + hexWidth / 2, 0,
            gridHeight / 2f * hexHeight - hexHeight / 2);
    }

    // Change hex grid coordinates to game coordinates
    public Vector3 WorldCoordinate(Vector2 gridPos)
    {
        // Position of first hexagon
        Vector3 startPos = CalcStartPos();
        
        //Every other row offset by half width of tile
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = hexWidth / 2;
        }
        float x = startPos.x + offset + gridPos.x * hexWidth;
        // Every new line is offset in z by .75 of hexagon height
        float z = startPos.z - gridPos.y * hexHeight * .75f;
        return new Vector3(x, 0, z);
    }

    // create all the hexagon tiles
    private void CreateGrid()
    {
        // Game object that acts as "parent" to all hexagon tiles
        GameObject hexGridParent = new GameObject("HexGrid");

        // create da grid
        for (float x = 0; x < gridHeight; x++)
        {
            for (float y = 0; y < gridWidth; y++)
            {
                // clone hexagon              
                GameObject hexagon = (GameObject)Instantiate(Hexagon);
                // position in grid
                Vector2 gridPos = new Vector2(x, y);

                var hexScript = hexagon.GetComponent<GridCell>();

                string name = "Hex " + x + ", " + y;

                hexScript.SetGridProperties(name, (int)x, (int)y);
                hexagon.transform.position = WorldCoordinate(gridPos);
                hexagon.transform.parent = hexGridParent.transform;
            }
        }
    }

	// Use this for initialization
	void Start () {
        SetSizes();
        CreateGrid();
    }	
}
