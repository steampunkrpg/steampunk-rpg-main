using UnityEngine;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour, IUnit
{
    public String unitName;

    public Unit()
    {
        unitName = transform.name;
    }

    public abstract void MoveUp();
}
