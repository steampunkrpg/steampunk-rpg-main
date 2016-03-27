using UnityEngine;
using System.Collections;

public class WorldMapButtonManager : MonoBehaviour
{

    int level;

    void Start()
    {
        DisplayButtons(GameManager.instance.level);
    }

    void DisplayButtons(int level)
    {
        var buttonList = GameObject.FindGameObjectsWithTag("MapButton");
        foreach (var button in buttonList)
        {
            if (checkName(button))
            {
                button.SetActive(true);
            }
            else
            {
                button.SetActive(false);
            }
        }
    }

    public bool checkName(GameObject button)
    {
        if ((level == 0 && button.name.Equals("Grassland Button"))
            || (level == 1 && button.name.Equals("Desert Button"))
            || (level == 2 && button.name.Equals("Forrest Button"))
            || (level == 3 && button.name.Equals("Snow Button"))
            || (level == 4 && button.name.Equals("Shadow Ridge Button")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
