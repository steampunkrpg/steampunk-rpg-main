using UnityEngine;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour, IUnit
{
    public String unitName, className;

    public Unit()
    {
        unitName = transform.name;
    }

    public abstract void Update();

    public virtual void MoveUp()
    {
        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
    }
}
