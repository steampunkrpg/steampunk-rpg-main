using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour, IUnit
{
    public String unitName, className;
    public GridCell currentCell;

    private const float PLAYER_HEIGHT = 0.5f;

    public GridCell CurrentCell
    {
        get
        {
            return currentCell;
        }

        set
        {
            currentCell = value;
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
        currentCell = GameObject.Find("Hex 0, 0").GetComponent<GridCell>();
        UpdatePosition();
    }

    public void MoveZN()
    {
        CurrentCell.col--;
        UpdatePosition();
    }

    public void MovePZ()
    {
        CurrentCell.row++;
        UpdatePosition();
    }

    public void MovePP()
    {
        CurrentCell.row++;
        CurrentCell.col++;
        UpdatePosition();
    }

    public void MoveZP()
    {
        CurrentCell.col++;
        UpdatePosition();
    }

    public void MoveNP()
    {
        CurrentCell.col++;
        UpdatePosition();
    }

    public void MoveNZ()
    {
        CurrentCell.col--;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector3 position = transform.position;
        position = CurrentCell.GetComponent<Renderer>().bounds.center;
        position.y = PLAYER_HEIGHT;
        transform.position = position;
    }
}
