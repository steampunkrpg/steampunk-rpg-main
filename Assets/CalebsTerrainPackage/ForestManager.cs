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
        float randX = Random.Range(25f, 75f);
        float randZ = Random.Range(5f, 45f);
        while (Distribute(randX, randZ) == false)
        {
            randX = Random.Range(25f, 75f);
            randZ = Random.Range(5f, 45f);
        }
        float randY = terrain.SampleHeight(new Vector3(randX, 0f, randZ)) + go.transform.lossyScale.y -2;
        Vector3 randPos = new Vector3(randX, randY, randZ);
        return randPos;
    }

    bool Distribute(float x, float y)
    {
        if (((x < 35) || (x>65)) || ((y < 4) || (y > 35)))
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
            GameObject tree = Instantiate(go, pos, Quaternion.Euler(-90,0,0)) as GameObject;
            tree.name = "tree";
        }
    }
	
	
}
