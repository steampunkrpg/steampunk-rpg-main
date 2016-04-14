using UnityEngine;
using System.Collections;

public class ForestManager : MonoBehaviour {

    //TEST
    public int numOfTrees;
    public GameObject tree1;
    public GameObject tree2;
    public Terrain terrain;

	// Use this for initialization
	void Start () {
        if (terrain.isActiveAndEnabled)
        {
            InstantiateVegetation(tree1, (numOfTrees / 2));
            InstantiateVegetation(tree2, (numOfTrees / 2));
        }	
	}

    Vector3 RandomPosGenerator(GameObject go)
    {
        float randX = Random.Range(60f, 140f);
        float randZ = Random.Range(60f, 140f);
        while (Distribute(randX, randZ) == false)
        {
            randX = Random.Range(60f, 140f);
            randZ = Random.Range(60f, 140f);
        }
        float randY = terrain.SampleHeight(new Vector3(randX, 0f, randZ)) + go.transform.lossyScale.y - 0.5f;
        Vector3 randPos = new Vector3(randX, randY, randZ);
        return randPos;
    }

    bool Distribute(float x, float y)
    {
        if (((x < 75) || (x>130)) || ((y < 75) || (y > 130)))
        {
            return true;
        }
        return false;
    }

    float Decider(float x, float y)
    {
        int pick = Random.Range(0, 2);
        if (pick == 0) { return x; }
        else { return y; }
    }

    void InstantiateVegetation(GameObject go, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = RandomPosGenerator(go);
            GameObject tree = Instantiate(go, pos, Quaternion.Euler(0,0,0)) as GameObject;
            tree.transform.parent = this.transform;
            tree.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            tree.name = "tree";
        }
    }
	
	
}
