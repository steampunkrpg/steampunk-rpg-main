using System;
using UnityEngine;

public class MoveUpCommand : ICommand {

    Unit unit;

    public MoveUpCommand(GameObject gameObject)
    {
        this.unit = gameObject.GetComponent("Unit") as Unit;
    }

    void ICommand.Execute()
    {
        // if grid cell x is even do blah
        unit.MoveNP();
        // else do blah 2
        unit.MoveZP();
    }
}
