using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour, IUnit
{
    public String unitName, className;
    public GridCell hex;

    private const float PLAYER_HEIGHT = 0.5f;

    public GridCell CurrentCell
    {
        get
        {
            return hex;
        }

        set
        {
            hex = value;
        }
    }

    public Unit()
    {
        
    }

    public void Update()
    { }

    public void SetUp()
    {
        transform.name = "I'm a unit!";
        //transform.parent = GameObject.Find("Unit Manager").transform;
        hex = GameObject.Find("Hex 0, 0").GetComponent<GridCell>();
        UpdatePosition();
    }


    public void UpdatePosition()
    {
        Vector3 position = transform.position;
        position = CurrentCell.GetComponent<Renderer>().bounds.center;
        position.y = PLAYER_HEIGHT;
        transform.position = position;
    }

    public void MoveNE()
    {
        CurrentCell = CurrentCell.GetNeighbor(0);
        UpdatePosition();
    }

    public void MoveN()
    {
        CurrentCell = CurrentCell.GetNeighbor(1);
        UpdatePosition();
    }

    public void MoveNW()
    {
        CurrentCell = CurrentCell.GetNeighbor(2);
        UpdatePosition();
    }

    public void MoveSW()
    {
        CurrentCell = CurrentCell.GetNeighbor(3);
        UpdatePosition();
    }

    public void MoveS()
    {
        CurrentCell = CurrentCell.GetNeighbor(4);
        UpdatePosition();
    }

    public void MoveSE()
    {
        CurrentCell = CurrentCell.GetNeighbor(5);
        UpdatePosition();
    }
}
