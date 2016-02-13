using System;
using UnityEngine;

public class MoveNWCommand : ICommand
{
    void ICommand.Execute(Unit unit)
    {
        unit.MoveNW();
    }
}
