using System;
using UnityEngine;

public class MoveUpCommand : ICommand {

    IUnit iUnit;

    public MoveUpCommand(GameObject gameObject)
    {
        this.iUnit = gameObject.GetComponent("IUnit") as IUnit;
    }

    void ICommand.Execute()
    {
        iUnit.MoveUp();
    }
}
