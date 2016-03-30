using UnityEngine;
using System.Collections;

public class GrasslandManager : MonoBehaviour {

    public int numOfBushes;
    public GameObject bush1;
    public GameObject bush2;
    public GameObject bush3;
    public GameObject bush4;

    public Terrain terrain;

    // Use this for initialization
    void Start()
    {
        if (terrain.isActiveAndEnabled)
        {
            InstantiateVegetation(bush1, (numOfBushes / 4));
            InstantiateVegetation(bush2, (numOfBushes / 4));
            InstantiateVegetation(bush3, (numOfBushes / 4));
            InstantiateVegetation(bush4, (numOfBushes / 4));

        }
    }

    Vector3 RandomPosGenerator(GameObject go)
    {
        float randX = Random.Range(5f, 95f);
        float randZ = Random.Range(5f, 95f);
        float randY = terrain.SampleHeight(new Vector3(randX, 0f, randZ)) + go.transform.lossyScale.y - 1.5f;

       
        while (DistributeGrass(randY) == false)
        {
            randX = Random.Range(3f, 98f);
            randZ = Random.Range(3f, 75f);
            randY = terrain.SampleHeight(new Vector3(randX, 0f, randZ)) + go.transform.lossyScale.y - 1.5f;
        }
        

        Vector3 randPos = new Vector3(randX, randY, randZ);
        return randPos;
    }

    bool DistributeGrass(float y)
    {
        if (y > 1.5f)
        {
            return true;
        }
        return false;
    }

    bool DistributeBush(float y)
    {
        if (y < 1.2f)
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
            GameObject bush = Instantiate(go, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
            bush.name = "grass";
            
            bush.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            
        }
    }


}
