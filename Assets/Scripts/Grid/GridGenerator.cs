using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour {

    // Variable that "stores" hexagon
    public GameObject Hexagon;
    public static GameObject cube;  

    // Set grid width / height
    public int gridWidth = 5;
    public int gridHeight = 5;

    // hexagon tile width / height
    private float hexWidth;
    private float hexHeight;

    // set height and width for tiles
    void setSizes()
    {
        hexWidth = Hexagon.GetComponent<Renderer>().bounds.size.x;
        hexHeight = Hexagon.GetComponent<Renderer>().bounds.size.y;
    }

    // Center hex grid (put first hexagon at (0, 0, 0))
    Vector3 calcStartPos()
    {
        Vector3 startPos;
        //start in top left corner
        startPos = new Vector3(-hexWidth * gridWidth / 2f + hexWidth / 2, 0,
            gridHeight / 2f * hexHeight - hexHeight / 2);
        return startPos;
    }

    // Change hex grid coordinates to game coordinates
    public Vector3 worldCoordinate(Vector2 gridPos)
    {
        // Position of first hexagon
        Vector3 startPos = calcStartPos();
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
    void createGrid()
    {
        // Game object that acts as "parent" to all hexagon tiles
        GameObject hexGridParent = new GameObject("HexGrid");
        int i = 1;
        // create da grid
        for (float x = 0; x < gridHeight; x++)
        {
            for (float y = 0; y < gridWidth; y++)
            {
                // clone hexagon              
                GameObject hexagon = (GameObject)Instantiate(Hexagon);
                // position in grid
                Vector2 gridPos = new Vector2(x, y);
                hexagon.transform.position = worldCoordinate(gridPos);
                hexagon.transform.parent = hexGridParent.transform;
                hexagon.transform.name = "Hex " + x + ", " + y;
                i++;
            }
        }
    }

	// Use this for initialization
	void Start () {
        setSizes();
        createGrid();
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3((-.055f * (((float)gridWidth) - .5f)), 0, .254f);
        cube.transform.localScale = new Vector3(.015f, .015f, .015f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
